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
    public class SupplyChainController : Controller
    {
        private IConfiguration _configuration;

        public SupplyChainController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [Authorize("Bearer")]
        public List<SupplyChainType> Get()
        {
            List<SupplyChainType> list = new List<SupplyChainType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.SupplyChainSelect"; // Stored Procedure Name  
                using (var multi = conexao.QueryMultiple(sql, null, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<SupplyChainType>().ToList();
                }
            }
            return list;
        }

    }
}

