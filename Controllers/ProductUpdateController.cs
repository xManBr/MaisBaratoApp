using System;
using System.Collections.Generic;
using System.Linq;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using Mercoplano.Maisbarato.Server.RESTful.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    [Route("api/[controller]")]
    public class ProductUpdateController : Controller
    {
        private IConfiguration _configuration;

        public ProductUpdateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Authorize("Bearer")]
        public void Post([FromBody] Object json)
        {// Insert into ProductTemp table
            
            dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(json.ToString());

            String userCode = HttpContext.User.Identity.Name;
            int userAgentId = Convert.ToInt32(userCode);

            if (HttpContext.User.Identity.IsAuthenticated)
            {
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
            
                String supplyChainCodeXml;
                try
                {
                    String supplyChainCodes = item[0].supplyChainCode;
                    String[] chainCodes = supplyChainCodes.Split(Config.DefaultSeparator);
                    if (chainCodes.Length > 0)
                    {
                        supplyChainCodeXml = XmlManager.BuildXmlStringId("SupplyChain", chainCodes);
                    }
                    else
                    {
                        supplyChainCodeXml = String.Empty;
                    }
                }
                catch
                {
                    supplyChainCodeXml = String.Empty;
                }

                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "business.ProductUpdate"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_Sku", sku);
                    p.Add("@IN_Name", name);
                    p.Add("@IN_picture", picture);
                    p.Add("@IN_Details", details);
                    p.Add("@IN_SupplyChainCodeXml", supplyChainCodeXml);
                    p.Add("@IN_UserAgentId", userAgentId);

                    conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);

                }
            }
        }
    }
}
