using System;
using System.Collections.Generic;
using System.Linq;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Mercoplano.Maisbarato.Server.RESTful.Util;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Http;
using Mercoplano.Maisbarato.Server.RESTful.Business;
using System.IO;
using System.Drawing;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    public class PalmoliveController : Controller
    {
        private IConfiguration _configuration;
        private IHttpContextAccessor contexto;

        private readonly IHostingEnvironment _appEnvironment;

        //public PalmoliveController(IHostingEnvironment appEnvironment)
        //{
        //    _appEnvironment = appEnvironment;
        //}

        public PalmoliveController(IConfiguration configuration, IHttpContextAccessor contexto, IHostingEnvironment appEnvironment)
        {
            _configuration = configuration;
            this.contexto = contexto;
            _appEnvironment = appEnvironment;
        }

        #region Copy Image to Path

        public IActionResult S_878676()
        {
            List<ProductFileType> list = new List<ProductFileType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ProductNoFileSelect";
                var p = new DynamicParameters();
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<ProductFileType>().ToList();
                }

                if (list.Count > 0)
                {

                    try
                    {
                        foreach (var product in list)
                        {
                            string path_Root = _appEnvironment.WebRootPath;
                            MemoryStream fileStream = new MemoryStream(product.Picture);
                            //fileStream.Write(product.Picture, 0, product.Picture.Length);

                            Bitmap startBitmap = new Bitmap(fileStream);
                          
                            string imageFromat = this.GetImageFormat(startBitmap).ToString();

                            string path_to_Images = path_Root + "\\" + Config.IMAGE_PATH_1 + "\\" + product.Sku + "." + imageFromat;
                            //Normal
                            using ( var stream = new FileStream(path_to_Images, FileMode.Create))
                            {
                                
                                startBitmap.Save(stream, startBitmap.RawFormat);
                                //fileStream.CopyToAsync((stream);// await 
                            }


                            int width = 80;
                            int height = 80;
                            MemoryStream fileStreamBig = new MemoryStream(ImageUtility.ResizeImage2(product.Picture, ref width, ref height));
                            Bitmap startBitmapBig = new Bitmap(fileStreamBig);
                          
                            path_to_Images = path_Root + "\\" + Config.IMAGE_PATH_1 + "\\" + product.Sku + "_" +  Config.BIG_LABEL + "." + imageFromat;
                            //Big
                            using (var stream = new FileStream(path_to_Images, FileMode.Create))
                            {

                                startBitmap.Save(stream, startBitmapBig.RawFormat);
                                //fileStream.CopyToAsync((stream);// await 
                            }

                            width = 240;
                            height = 240;
                            MemoryStream fileStreamSmall= new MemoryStream(ImageUtility.ResizeImage2(product.Picture, ref width, ref height));
                            Bitmap startBitmapSmall= new Bitmap(fileStreamSmall);
                          
                            path_to_Images = path_Root + "\\" + Config.IMAGE_PATH_1 + "\\" + product.Sku + "_" + Config.SMALL_LABEL + "."  + imageFromat;
                            //Small
                            using (var stream = new FileStream(path_to_Images, FileMode.Create))
                            {

                                startBitmap.Save(stream, startBitmapSmall.RawFormat);
                                //fileStream.CopyToAsync((stream);// await 
                            }


                            sql = "business.ProductFileInsertOrUpdate";
                            p = new DynamicParameters();
                            p.Add("@IN_ProductId", product.ProductId);
                            p.Add("@IN_VirtualPath", "/" + Config.IMAGE_PATH_1 + "/" + product.Sku + "." + imageFromat);
                            conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.Msg = e.Message;
                        return View();
                    }   
                }

            }
            ViewBag.Msg = "OK";
            return View();
        }


        public System.Drawing.Imaging.ImageFormat GetImageFormat(Bitmap img)
        {
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                return System.Drawing.Imaging.ImageFormat.Bmp;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                return System.Drawing.Imaging.ImageFormat.Png;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf))
                return System.Drawing.Imaging.ImageFormat.Emf;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif))
                return System.Drawing.Imaging.ImageFormat.Exif;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                return System.Drawing.Imaging.ImageFormat.Gif;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                return System.Drawing.Imaging.ImageFormat.Icon;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
                return System.Drawing.Imaging.ImageFormat.MemoryBmp;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
                return System.Drawing.Imaging.ImageFormat.Tiff;
            else
                return System.Drawing.Imaging.ImageFormat.Wmf;
        }
        #endregion

        public IActionResult S_878675()
        {

            List<UserEmailTokenType> list = new List<UserEmailTokenType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                //[TOEKN]
                var sql = "common.TokenNotSentSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {// Lista de token pendentes de envio.....
                    list = multi.Read<UserEmailTokenType>().ToList();
                }

                if (list.Count > 0)
                {

                    int lim = list.Count > 100 ? 100 : list.Count; // Enviar ate 100 email com token por vez em que o serviço por executado....
                    for (int i = 0; i < lim; i++)
                    {
                        var myToken = list[i];
                        try
                        {
                            EmailTO emailTO = new EmailTO();
                            emailTO.Body = "Token: " + myToken.Token;
                            emailTO.Subject = "Token para validação de usuário";
                            emailTO.FromEmail = "donotreply@maisbarato.app";
                            emailTO.ToEmail = myToken.Email;
                            Email.Send(emailTO);

                            sql = "common.UserEmailTokenUpdate";
                            p = new DynamicParameters();
                            p.Add("@IN_Id", myToken.Id);
                            p.Add("@IN_StatusId", Config.ACT);
                            conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);//[TODO] Verificar se não deu erro no update
                        }
                        catch
                        {
                            return View();
                        }
                    }
                }

                //[EmailTO]
                sql = "common.EmailSelect"; // Stored Procedure Name  
                List<EmailTO> emailTOs = new List<EmailTO>();
                var pp = new DynamicParameters();
                pp.Add("@IN_StatusId", Config.ACT);
                using (var multi = conexao.QueryMultiple(sql, pp, null, null, System.Data.CommandType.StoredProcedure))
                {// Lista de token pendentes de envio.....
                    emailTOs = multi.Read<EmailTO>().ToList();
                }

                foreach (var emailTO in emailTOs)
                {

                    bool ret = Email.Send(emailTO);

                    if (ret)
                    {
                        sql = "common.EmailStatusUpdate"; // Stored Procedure Name  
                        p = new DynamicParameters();
                        p.Add("@IN_EmailId", emailTO.EmailId);
                        p.Add("@IN_StatusId", Config.SENT);
                        conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);//[TODO] Verificar se não deu erro no update
                    }

                }
            }
            return View();
        }


    }
}