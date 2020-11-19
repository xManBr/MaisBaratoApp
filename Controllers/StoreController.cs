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
using System.Threading;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{

    [Route("api/Store/[action]")]
    public class StoreController : Controller
    {

        private IConfiguration _configuration;

        public StoreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [ActionName("Insert")]
        public bool Insert([FromBody] Object json)
        {
            var storeType = Newtonsoft.Json.JsonConvert.DeserializeObject<StoreType>(json.ToString());

            String userCode = HttpContext.User.Identity.Name;
            storeType.UserAgentId = Convert.ToInt32(userCode);

            String[] coords = storeType.LatLng.Split(',');
            char a = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            coords[0] = coords[0].Replace('.', a);
            coords[1] = coords[1].Replace('.', a);
            decimal lat = 0;
            decimal lng = 0;
            if (coords.Length > 1)
            {
                lat = Convert.ToDecimal(coords[0]);
                lng = Convert.ToDecimal(coords[1]);
            }
            
            try
            {
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "business.StoreInsertOrUpdate"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_StoreId", storeType.StoreId);
                    p.Add("@IN_FullName", storeType.FullName);
                    p.Add("@IN_Name", storeType.Name);                   
                    p.Add("@IN_Source", storeType.Source);
                    p.Add("@IN_ObjectTypeId", "DEF");
                    p.Add("@IN_StatusId", "ACT");
                    p.Add("@IN_UserAgentId", storeType.UserAgentId);
                    p.Add("@IN_DDI", storeType.DDI);
                    p.Add("@IN_DDD", storeType.DDD);
                    p.Add("@IN_Phone", storeType.Phone);
                    p.Add("@IN_WebSite", storeType.WebSite);
                    p.Add("@IN_Address1", storeType.Address1);
                    p.Add("@IN_Number", storeType.Number);
                    p.Add("@IN_Address2", storeType.Address2);
                    p.Add("@IN_ZipCode", storeType.ZipCode);
                    p.Add("@IN_City", storeType.City);
                    p.Add("@IN_Country", storeType.Country);
                    p.Add("@IN_StateName", storeType.StateName);
                    p.Add("@IN_ActivityKey", storeType.ActivityKey);
                    p.Add("@IN_Lat", lat);
                    p.Add("@IN_Lng", lng);

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
        [ActionName("Get")]
        public List<StoreType> Get(string LatLng)
        {
            List<StoreType> list = new List<StoreType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.StoreSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_LatLng", LatLng);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<StoreType>().ToList();
                }
            }
            return list;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("UserStore")]
        public List<StoreType> UserStore()
        {
            String userCode = HttpContext.User.Identity.Name;
            int userAgentId = Convert.ToInt32(userCode);

            List<StoreType> list = new List<StoreType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.StoreByUserSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_UserAgentId", userAgentId);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<StoreType>().ToList();
                }
            }
            return list;
        }
        
        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("ThisStore")]
        public List<StoreType> ThisStore(int storeId)
        {
            String userCode = HttpContext.User.Identity.Name;
            int userAgentId = Convert.ToInt32(userCode);

            List<StoreType> list = new List<StoreType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.StoreByIdSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add("@IN_StoreId", storeId);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<StoreType>().ToList();
                }
            }
            return list;
        }

    }
}