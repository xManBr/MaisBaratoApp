using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Mercoplano.Maisbarato.Server.RESTful.Models;

namespace Mercoplano.Maisbarato.Server.RESTful.DAL.SQLServer
{
    public class EmailDAO
    {
        public static void Insert(IConfiguration _configuration, String emailXml)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "common.p_EmailXml_i"; // Stored Procedure Name  
                emailXml = emailXml.Replace("&key", "?key");
                var p = new DynamicParameters();
                p.Add("@IN_EmailXml", emailXml);

                conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
