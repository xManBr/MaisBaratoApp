using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace MaisBaratoServerApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Complaint/[action]")]
    public class ComplaintController : Controller
    {
        private IConfiguration _configuration;

        public ComplaintController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Authorize("Bearer")]       
        [ActionName("Anonymous")]
        public List<ComplaintType> GetList(String statusId)
        {
            String userCode = HttpContext.User.Identity.Name;
            int userAgentId = Convert.ToInt32(userCode);

            List<ComplaintType> list = new List<ComplaintType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ComplaintSelect"; // Stored Procedure Name  
                var p = new DynamicParameters();
                p.Add(" @IN_StatusId", statusId);
                using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                {
                    list = multi.Read<ComplaintType>().ToList();
                }
            }
            return list;

        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Anonymous")]
        public void Anonymous(String latLng, int productId, int productPriceId, int storeId, String sourceIP, int interestId)
        {

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {
                var sql = "business.ComplaintInsert";
                var p = new DynamicParameters();
                p.Add("@IN_LatLng", latLng);
                p.Add("@IN_ProductId", productId);
                p.Add("@IN_ProductPriceId", productPriceId);
                p.Add("@IN_StoreId", storeId);
                p.Add("@IN_Note", String.Empty);
                p.Add("@IN_SourceIP", sourceIP);
                p.Add("@IN_interestId", interestId);

                /*
                 * 
                p.Add("@IN_LatLng", complaintType.LatLng);
                p.Add("@IN_ProductId", complaintType.ProductId);
                p.Add("@IN_ProductPriceId", complaintType.ProductPriceId);
                p.Add("@IN_StoreId", complaintType.StoreId);
                p.Add("@IN_Note", complaintType.Note);
                p.Add("@IN_SourceIP", complaintType.SourceIP);
                p.Add("@IN_interestId", complaintType.InterestId);
                 * */

                conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
            }
        }
    }
}