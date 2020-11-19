using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Mercoplano.Maisbarato.Server.RESTful.DAL.SQLServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    [Route("api/[controller]")]
    [Route("api/ProductUser/[action]")]
    public class ProductUserController : Controller
    {
        private IConfiguration _configuration;

        public ProductUserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("GetFull")]
        public List<ProductType> Get()
        {
            String userCode = HttpContext.User.Identity.Name;
            int userAgentId = Convert.ToInt32(userCode);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                /*
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                var UserCode = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName).ToString();
                */
            }

            List<ProductType> list = new List<ProductType>();
            // using (SqlConnection conexao = new SqlConnection( Config.CONNECTION_STRING))
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ProductByUserSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_UserAgentId", userAgentId);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<ProductType>().ToList();
                }
                foreach( var  item in list)
                {
                    item.PictureBase64 = Convert.ToBase64String(item.Picture);
                    item.Picture = Convert.FromBase64String(String.Empty);
                }
                return list;
            }
        }

        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("NoPicture")]
        public List<ProductType> NoPicture()
        {
            String userCode = HttpContext.User.Identity.Name;
            int userAgentId = Convert.ToInt32(userCode);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                /*
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                var UserCode = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName).ToString();
                */
            }

            List<ProductType> list = new List<ProductType>();
            // using (SqlConnection conexao = new SqlConnection( Config.CONNECTION_STRING))
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ProductByUserSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_UserAgentId", userAgentId);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<ProductType>().ToList();
                }
                foreach (var item in list)
                {
                    item.Picture = Convert.FromBase64String(String.Empty);
                }
                return list;
            }
        }
    }
}