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


namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{

    [Route("api/[controller]")]
    [Route("api/Score/[action]")]
    public class ScoreController : Controller
    {
        private IConfiguration _configuration;

        public ScoreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("Get")]
        public ScoreType Get()
        {
            String userCode = HttpContext.User.Identity.Name;
            int userAgentId = Convert.ToInt32(userCode);

            List<ScoreType> list = new List<ScoreType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ScoreSelect";
                var p = new DynamicParameters();
                p.Add("@IN_UserAgentId", userAgentId);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<ScoreType>().ToList();
                }
            }

            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }

    }
}