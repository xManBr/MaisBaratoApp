using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Mercoplano.Maisbarato.Server.RESTful.DAL.SQLServer;
using Microsoft.AspNetCore.Authorization;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mercoplano.Maisbarato.Server.RESTful.Business;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
  
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public List<ProductType> Get(string sku)
        {
            // Para ler os produtos não precisar estar logado - isso somente eh necessario para inserir novo produto
            List<ProductType> list = new List<ProductType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ProductBySkuSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_Sku", sku);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<ProductType>().ToList();
                }
            }

            if (list != null)
            {
                foreach (var item in list)
                {
                    item.PictureBase64 = Convert.ToBase64String(item.Picture);
                    item.Picture = Convert.FromBase64String(String.Empty);
                }
                return list;
            }
            else
            {
                return new List<ProductType>();
            }
        }



        [HttpPost]
        [Authorize("Bearer")]
        public bool Post([FromBody] Object json)
        {
            bool ret = false;
            // Insert into ProductTemp table

            //var yyy = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductType>(json.ToString(), new Newtonsoft.Json.JsonSerializerSettings{TypeNameHandling = TypeNameHandling.Objects, SerializationBinder = knownTypesBinder });

            var productType = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductType>(json.ToString());

            //dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(json.ToString());
            //dynamic item = System.Web.Helpers.Json.Decode(json.ToString());

            /*
            string sku;
            try
            {
                sku = item[0].sku;
            }
            catch
            {
                sku = String.Empty;
            }
                        
            String name;            
            try
            {
                name = item[0].name;
            }
            catch
            {
                name = String.Empty;
            }
            
            String details;
            try
            {
                details = item[0].details;
            }
            catch
            {
                details = String.Empty;
            }
                        
            String source;
            try
            {
                source = item[0].source;
            }
            catch
            {
                source = String.Empty;
            }
            
            Byte[] picture;

            try
            {
                //picture = (Byte[]) item[0].picture;
                picture = Convert.FromBase64String(item[0].picture);
            }
            catch
            {
                picture = Convert.FromBase64String(String.Empty);
            }

            String supplyChainCode= String.Empty;
            try
            {
                supplyChainCode = item[0].supplyChainCode;
            }
            catch
            {
                supplyChainCode = String.Empty;
            }
            */

            if (!productType.Sku.Equals(String.Empty))
            {
                try
                {
                    //picture = (Byte[]) item[0].picture;
                    productType.Picture = Convert.FromBase64String(productType.PictureBase64);
                }
                catch
                {
                    productType.Picture = Convert.FromBase64String(String.Empty);
                }

                String userCode = HttpContext.User.Identity.Name;
                productType.UserAgentId = Convert.ToInt32(userCode);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                    {
                        var sql = "business.ProductTempInsert"; // Stored Procedure Name  
                        var p = new DynamicParameters();
                        p.Add("@IN_Sku", productType.Sku);
                        p.Add("@IN_Name", productType.Name);
                        p.Add("@IN_picture", productType.Picture);
                        p.Add("@IN_Details", productType.Details);
                        p.Add("@IN_supplyChainCode", String.Empty);// [TODO] INLUIR NO OBJETO 
                        p.Add("@IN_UserAgentId", productType.UserAgentId);
                        p.Add("@IN_Source", productType.Source);
                        p.Add("@IN_ObjectTypeId", "DEF");
                        p.Add("@IN_StatusId", "ACT");

                        conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                        ret = true;//[TODO] iMPLEMENTAR CONTROLE DE ERRO DENTRO DA PROCEDURE
                    }

                    EmailTO emailTO = new EmailTO();
                    emailTO.Body = "Novo Produto cadastrado em: " + DateTime.Now.ToString() + " sku ( " + productType.Sku + " ) " + productType.Name;
                    emailTO.ToEmail = "maisbaratocomapp@gmail.com";
                    emailTO.FromEmail = "donotreply@maisbarato.app";
                    emailTO.Subject = "[MaisBarato] Alerta de Inclusão de novo Produto";
                    emailTO.StatusId = Config.ACT;
                    emailTO.ObjectTypeId = Config.DEF;
                    Email.Insert(_configuration, emailTO);

                }
            }
            return ret;
        }
        
    }
}
