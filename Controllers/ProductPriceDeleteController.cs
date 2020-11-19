using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

using Mercoplano.Maisbarato.Server.RESTful.Util;
using Microsoft.Extensions.Configuration;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    [Route("api/ProductPriceDelete/[action]")]
    public class ProductPriceDeleteController : Controller
    {
        private IConfiguration _configuration;

        public ProductPriceDeleteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("ById")]
        public bool Delete(int productPriceId)
        {
            bool ret;
            try
            {
                String userCode = HttpContext.User.Identity.Name;
                int userAgentId = Convert.ToInt32(userCode);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                            {
                                var sql = "business.ProductPriceDelete"; // Stored Procedure Name  
                                var p = new DynamicParameters();
                                p.Add("@IN_ProductPriceId", productPriceId);
                                p.Add("@IN_UserAgentId", userAgentId);

                                conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);

                            }
                }
                ret = true;
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("ByMultipleIds")]
        public bool Deletes(String productPriceIds)
        {
            bool ret;
            try
            {
                String userCode = HttpContext.User.Identity.Name;
                int userAgentId = Convert.ToInt32(userCode);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    if ((productPriceIds != null) && (productPriceIds != String.Empty))
                    {
                        String[] productPriceIdArrs = productPriceIds.Split(Config.DefaultSeparator);
                        if (productPriceIdArrs.Length == 1)
                        {// Este método tambem apaga a tabela de produtos se não sobrar mais nenhum preço associado ao mesmo!
                            int productPriceId = Convert.ToInt32(productPriceIdArrs[0]);

                            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                            {
                                var sql = "business.ProductPriceDelete"; // Stored Procedure Name  
                                var p = new DynamicParameters();
                                p.Add("@IN_ProductPriceId", productPriceId);
                                p.Add("@IN_UserAgentId", userAgentId);

                                conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);

                            }
                        }
                        else
                        {
                            String ProductPrice = XmlManager.BuildXmlStringId("ProductPrice", productPriceIdArrs);

                            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                            {
                                var sql = "business.ProductPriceDeletes"; // Stored Procedure Name  
                                var p = new DynamicParameters();
                                p.Add("@IN_ProductPrice", ProductPrice);
                                p.Add("@IN_UserAgentId", userAgentId);

                                conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);

                            }
                        }
                    }
                }
                ret = true;
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
    }
}
