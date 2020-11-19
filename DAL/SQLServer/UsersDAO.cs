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
    public class UsersDAO
    {
        private IConfiguration _configuration;

        public UsersDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserAgentType Find(String code)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                return conexao.QueryFirstOrDefault<UserAgentType>(
                    " SELECT [UserAgentId]" +
                    ",[Code] " +
                    ",[Password]" +
                    ",[Name]" +
                    ",[Email]" +
                    ",[StatusId]" +
                    " FROM[common].[UserAgent] (NoLock) " +
                    "WHERE Code = @Code  or Email = @Code", new { Code = code });
            }
        }
    }
}