using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Mercoplano.Maisbarato.Server.RESTful.DAL.SQLServer;
using Microsoft.AspNetCore.Authorization;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mercoplano.Maisbarato.Server.RESTful.Business;

using Mercoplano.Maisbarato.Server.RESTful;


namespace MaisBaratoServerApp.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingList/[action]")]
    public class ShoppingListController : Controller
    {

        private IConfiguration _configuration;

        public ShoppingListController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Authorize("Bearer")]
        [ActionName("Delitem")]
        public int Delitem( int listId)
        {
            String userCode = HttpContext.User.Identity.Name;
            int  userAgentId = Convert.ToInt32(userCode);

            int errorCode = 999;// Erro Genérico
            try
            {
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "business.ShoppingListDelete"; 
                    var p = new DynamicParameters();
                    p.Add("@IN_ListId", listId);
                    p.Add("@IN_UserAgentId", userAgentId);
                    p.Add("@IN_ERROR", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                    errorCode = p.Get<int>("@IN_ERROR");
                    return errorCode;
                }
            }
            catch (Exception ex)
            {
                String e = ex.Message;
                return errorCode;
            }
        }

        [HttpPost]
        [Authorize("Bearer")]
        [ActionName("Insert")]
        public bool Insert([FromBody] Object json)
        {
            var shoppingListType = Newtonsoft.Json.JsonConvert.DeserializeObject<ShoppingListType>(json.ToString());

            String userCode = HttpContext.User.Identity.Name;
            shoppingListType.UserAgentId = Convert.ToInt32(userCode);

            try
            {
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "business.ShoppingListIorU"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_UserAgentId", shoppingListType.UserAgentId);
                    p.Add("IN_TargetName", shoppingListType.TargetName);
                    p.Add("@IN_Sku", shoppingListType.Sku);
                    p.Add("@IN_Amount", shoppingListType.Amount);
                    p.Add("@IN_ObjectTypeId", Config.DEF);
                    p.Add("@IN_StatusId", Config.ACT);
                    conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                String e = ex.Message;
                return false;
            }
        }

        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("GetShoppingList")]
        public List<ShoppingListType> Get()
        {
            String userCode = HttpContext.User.Identity.Name;
            if (userCode != String.Empty)
            {
                int userAgentId = Convert.ToInt32(userCode);

                List<ShoppingListType> list = new List<ShoppingListType>();
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "business.ShoppingListSelect"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_userAgentId", userAgentId);
                    using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                    {
                        list = multi.Read<ShoppingListType>().ToList();
                    }
                }
                return list;
            }
            return null;
        }
    }
}
