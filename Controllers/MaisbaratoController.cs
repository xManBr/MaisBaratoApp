using System;
using System.Collections.Generic;
using System.Linq;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Mercoplano.Maisbarato.Server.RESTful.Util;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Http.Extensions;

using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Http;

using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    public class MaisbaratoController : Controller
    {
        private IConfiguration _configuration;
        private IHttpContextAccessor contexto;

        private readonly IHostingEnvironment _appEnvironment;

        public MaisbaratoController(IConfiguration configuration, IHttpContextAccessor contexto, IHostingEnvironment appEnvironment)
        {
            _configuration = configuration;
            this.contexto = contexto;
            _appEnvironment = appEnvironment;
        }

        private void ComplaintParams()
        {
            ViewBag.UrlCompaint = Config.UrlComplaint;
            ViewBag.IP = this.contexto.HttpContext.Connection.RemoteIpAddress.ToString();

        }

        public IActionResult PictureDisplay(String sku, String pathLen)
        {
            List<ProductType> listp = new List<ProductType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ProductBySkuSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_Sku", sku);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    listp = multi.Read<ProductType>().ToList();
                }
            }

            if (listp.Count > 0)
            {
                int width;
                int height;
                if (pathLen == Config.SMALL_LABEL)
                {
                    ViewBag.PathPicture = this.PathLength(Config.SMALL_LABEL, listp[0].VirtualPath);
                    width = 80;
                    height = 80;
                }
                else if (pathLen == Config.BIG_LABEL)
                {
                    ViewBag.PathPicture = this.PathLength(Config.BIG_LABEL, listp[0].VirtualPath);
                    width = 240;
                    height = 240;
                }
                else
                {
                    ViewBag.PathPicture = this.PathLength(Config.BIG_LABEL, listp[0].VirtualPath);
                    width = 160;
                    height = 160;
                }
                ViewBag.ProductPicture = ImageUtility.ResizeImage2(listp[0].Picture, ref width, ref height);
                ViewBag.Name = listp[0].Name;
                ViewBag.Sku = listp[0].Sku;
                ViewBag.height = height;
                ViewBag.width = width;
            }

            return View();
        }

        public IActionResult InterestLocal(String latLng, String s)
        {
            if (s != null)
            {
                ViewBag.Subscript = s;
            }
            else
            {
                ViewBag.Subscript = String.Empty;
            }

            if (latLng != null)
            {
                var latLngs = latLng.Split(',');
                ViewBag.Lat = latLngs[0];
                ViewBag.Lng = latLngs[1];
            }
            else
            {
                ViewBag.Lat = 0;
                ViewBag.Lng = 0;
            }

            return View();
        }

        public IActionResult Interest()
        {
            ComplaintParams();

            List<InterestType> list = new List<InterestType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.InteresseSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<InterestType>().ToList();
                }
            }
            return View(list);

        }

        public IActionResult SupplyChain(String code, String latitude, String longitude)
        {
            ViewBag.PathRoot = string.Empty;// _appEnvironment.WebRootPath;

            ComplaintParams();

            ViewBag.latitude = latitude != null ? latitude : "0";
            ViewBag.longitude = longitude != null ? longitude : "0";

            SupplyChainType suply;// = new SupplyChainType();

            if ((code != null) && (code != String.Empty))
            {
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    String sql = @"
                        SELECT SupplyChainId
                              ,Code
                              ,Name
                              ,ObjectTypeId
                              ,StatusId
                              ,Category
                          FROM business.SupplyChain
                         WHERE Code = '{0}'";
                    sql = String.Format(sql, code);

                    suply = conexao.QueryFirstOrDefault<SupplyChainType>(sql);
                    if (suply != null)
                    {
                        ViewBag.SupplyChainName = suply.Name;
                    }
                    else
                    {
                        ViewBag.SupplyChainName = "Diversa";
                    }
                }

            }
            else
            {
                ViewBag.SupplyChainName = "Diversa";
            }

            List<ProductType> list = new List<ProductType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.SupplyProductSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_code", code);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<ProductType>().ToList();
                }
            }

            return View(list);
        }

        public IActionResult News()
        {


            /*
            //if (this.HttpContext.Request..Url.GetLeftPart(UriPartial.Authority).Contains("receitafederal.com.br"))
            if (Request.GetDisplayUrl().Contains("receitafederal.com.br"))
            {
                return RedirectToAction("Cpf_Cnpj_IRPF_IRPJ_Comex_Receita_Federal_Brasil", "RFB");
            }
            else
            {
                return View();
            }
            */

            return View();
        }

        public IActionResult Index()
        {
            //[TODO] COMO RECUPERAR AS COORDENADAS (latitude, longitude) iniciais...
            return SupplyChain(string.Empty, "0", "0");
            /*
            List<ProductType> list = new List<ProductType>();
            return View(list);
            */
        }

        public IActionResult Search(String searchProduct, String latitude, String longitude)
        {// Lista produtos procurados
         //[TODO] Script para produrar as ofertas proximas ao visitante

            ViewBag.PathRoot = string.Empty;// _appEnvironment.WebRootPath;

            ComplaintParams();

            ViewBag.latitude = latitude != null ? latitude : "0";
            ViewBag.longitude = longitude != null ? longitude : "0";

            string sessionID = String.Empty;//HttpContext.Session.SessionID;
            string url = Request.GetDisplayUrl();

            string latLng = string.Empty;
            if ((latitude != null) && (latitude != String.Empty))
            {
                latLng = latitude + "," + longitude;
            }

            string urlAnetrior;
            try
            {
                urlAnetrior = Request.GetDisplayUrl();//this.HttpContext.Request.UrlReferrer.ToString();
            }
            catch (Exception ex)
            {
                urlAnetrior = "indefinifo";
            }

            if ((searchProduct != null) && (searchProduct != String.Empty))
            {
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "business.InteresseInsert"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_Interesse", searchProduct);
                    p.Add("@IN_ur", url);
                    p.Add("@IN_urlanterior", url);
                    p.Add("@IN_SessionID", urlAnetrior);
                    p.Add("@IN_LatLng", sessionID);
                    p.Add("@IN_LatLng", latLng);

                    conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                }
            }

            // var list = (from x in db.ProductSelect(null, searchProduct) select new ProductType { Sku = (String)x.Sku, Name = (String)x.Name, Source = (String)x.Source, ObjectTypeId = (String)x.ObjectTypeId, StatusId = (String)x.StatusId, Details = (String)x.Details, Picture = (Byte[])x.Picture, ProductId = (int)x.ProductId }).ToList();
            List<ProductType> list = new List<ProductType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ProductSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_Sku", null);
                p.Add("@IN_Name", searchProduct);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<ProductType>().ToList();
                }
            }

            return View("Index", list);
        }

        private string PathLength(string lengthTag, string str)
        {
            string strOut = string.Empty;
            if (str != string.Empty)
            {
                String[] strs = str.Split('.');
                if (strs.Length > 1)
                {
                    strs[strs.Length - 2] = strs[strs.Length - 2] + "_" + lengthTag + ".";
                    for (int i = 0; i < strs.Length; i++)
                    {
                        strOut = strOut + strs[i];
                    }
                }
            }

            return strOut;
        }

        public IActionResult PriceList(String sku, String latitude, String longitude)
        {
            ViewBag.PathRoot = string.Empty;// _appEnvironment.WebRootPath;
            ViewBag.Sku = sku;
            ComplaintParams();

            char a = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            latitude = latitude.Replace('.', a);
            longitude = longitude.Replace('.', a);
            decimal lat = 0;
            decimal lng = 0;
            lat = Convert.ToDecimal(latitude);
            lng = Convert.ToDecimal(longitude);

            List<ProductType> listp = new List<ProductType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ProductBySkuSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_Sku", sku);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    listp = multi.Read<ProductType>().ToList();
                }
            }

            if (listp.Count > 0)
            {
                ViewBag.PathSmall = this.PathLength(Config.SMALL_LABEL, listp[0].VirtualPath);
                int width = 80;
                int height = 80;
                ViewBag.ProductPicture = ImageUtility.ResizeImage2(listp[0].Picture, ref width, ref height);
                ViewBag.Name = listp[0].Name;
                ViewBag.Sku = listp[0].Sku;
                ViewBag.height = height;
                ViewBag.width = width;

                ViewBag.PathBig = this.PathLength(Config.BIG_LABEL, listp[0].VirtualPath);
                width = 240;
                height = 240;
                ViewBag.ProductPictureBig = ImageUtility.ResizeImage2(listp[0].Picture, ref width, ref height);
                ViewBag.heightBig = height;
                ViewBag.widthBig = width;

                ViewBag.ProductId = listp[0].ProductId;
                ViewBag.Complained = listp[0].Complained;

                List<ProductPriceType> list = new List<ProductPriceType>();
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "business.ProductPriceByDaysAgoSelect"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_Sku", sku);
                    p.Add("@IN_DaysAgo", Config.DAYS_AGO);
                    p.Add("@IN_Lat", lat);
                    p.Add("@IN_Lng", lng);
                    p.Add("@IN_Radius", Config.Radius);

                    using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                    {
                        list = multi.Read<ProductPriceType>().ToList();
                    }
                }

                listp = null;
                return View(list);
            }
            else
            {
                ViewBag.Name = String.Empty;
                ViewBag.Sku = String.Empty;
                ViewBag.height = 0;
                ViewBag.width = 0;
                List<ProductPriceType> list = new List<ProductPriceType>();

                return RedirectToAction("Index", "Maisbarato");

            }
        }

        public IActionResult Localizacao(int storeId)
        {

            ComplaintParams();

            String latLng = null;
            List<StoreType> list = new List<StoreType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "[business].[StoreByIdSelect]"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_StoreId", storeId);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<StoreType>().ToList();
                }
                if (list.Count > 0)
                {
                    ViewBag.Store = list[0].Name;
                    ViewBag.DDD = list[0].DDD;
                    ViewBag.Phone = list[0].Phone;
                    ViewBag.Address1 = list[0].Address1;
                    ViewBag.Address2 = list[0].Address2;
                    ViewBag.Number = list[0].Number;
                    ViewBag.City = list[0].City;
                    ViewBag.StateName = list[0].StateName;
                    ViewBag.ZipCode = list[0].ZipCode;
                    latLng = list[0].LatLng;
                    ViewBag.StoreId = list[0].StoreId;
                    ViewBag.Complained = list[0].Complained;

                    if (latLng != null)
                    {
                        var latLngs = latLng.Split(',');
                        if (latLngs.Length > 1)
                        {
                            ViewBag.Lat = latLngs[0];
                            ViewBag.Lng = latLngs[1];
                        }
                        else
                        {
                            ViewBag.Lat = 0;
                            ViewBag.Lng = 0;
                        }
                    }
                    else
                    {
                        ViewBag.Lat = 0;
                        ViewBag.Lng = 0;
                    }
                    return View();
                }
            }

            return RedirectToAction("Index", "Maisbarato");
        }
    }
}