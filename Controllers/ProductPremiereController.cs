using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Mercoplano.Maisbarato.Server.RESTful.DAL.SQLServer;
using Mercoplano.Maisbarato.Server.RESTful.Security;
using Dapper;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Mercoplano.Maisbarato.Server.RESTful.Util;
using Mercoplano.Maisbarato.Server.RESTful.Security;
using System.Linq;


namespace MaisBaratoServerApp.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductPremiere/[action]")]
    public class ProductPremiereController : Controller
    {
        private IConfiguration _configuration;

        public ProductPremiereController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
       /* [Authorize("Bearer")] */
        [ActionName("Get")]
        public List<ProductPremiereType> Get(string code)
        {
            List<ProductPremiereType> list = new List<ProductPremiereType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "[business].[ProductPremiereSelect]"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_Code", code);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<ProductPremiereType>().ToList();
                }
            }

            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<ProductPremiereType>();
            }
        }
    }
}