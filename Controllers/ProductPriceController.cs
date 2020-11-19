using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Threading;


namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    [Route("api/[controller]")]
    [Route("api/ProductPrice/[action]")]
    public class ProductPriceController : Controller
    {
        // private MaisbaratoAppEntities db = new MaisbaratoAppEntities();

        private IConfiguration _configuration;

        public ProductPriceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("GetPriceStore")]
        public ProductPriceStoreType Get(int productPriceId)
        {
            String userCode = HttpContext.User.Identity.Name;
            if (userCode != String.Empty)
            {
                int userAgentId = Convert.ToInt32(userCode);

                List<ProductPriceStoreType> list = new List<ProductPriceStoreType>();
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {

                    var sql = "business.ProductPriceStoreSelect"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_ProductPriceId", productPriceId);
                    p.Add("@IN_userAgentId", userAgentId);
                    using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                    {
                        list = multi.Read<ProductPriceStoreType>().ToList();
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
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Authorize("Bearer")]
        [ActionName("GetPrices")]
        public List<ProductPriceType> Get(string sku, String latLng)
        {
            String[] coords = latLng.Split(',');
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

            String userCode = HttpContext.User.Identity.Name;
            if (userCode != String.Empty)
            {
                int userAgentId = Convert.ToInt32(userCode);

                List<ProductPriceType> list = new List<ProductPriceType>();
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {
                    var sql = "business.ProductPriceBySkuSelect"; // Stored Procedure Name  
                    var p = new DynamicParameters();
                    p.Add("@IN_Sku", sku);
                    p.Add("@IN_DaysAgo", Config.DAYS_AGO);
                    p.Add("@IN_userAgentId", userAgentId);
                    p.Add("@IN_Lat", lat);
                    p.Add("@IN_Lng", lng);
                    p.Add("@IN_Radius", Config.Radius);
                    using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                    {
                        list = multi.Read<ProductPriceType>().ToList();
                    }

                }

                return list;
            }
            else
            {
                return null;
            }
        }


        //public List<ProductPriceType> Post([FromBody] Object json)
        [HttpPost]
        [Authorize("Bearer")]
        [ActionName("PostPriceInsert")]
        public int Post([FromBody] Object json)
        {
            //dynamic item = System.Web.Helpers.Json.Decode(json.ToString());
            var productPriceType = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductPriceType>(json.ToString());

            productPriceType.Price = productPriceType.Price / 100;

            String userCode = HttpContext.User.Identity.Name;
            productPriceType.UserAgentId = Convert.ToInt32(userCode);
            int errorCode = 999;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
                {

                    var p = new DynamicParameters();
                    int storeId = 0;
                    var sql = String.Empty;
                    if (productPriceType.StoreId == 0)
                    {
                        String[] coords = productPriceType.LatLng.Split(',');
                        char a = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                        //char a = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
                        coords[0] = coords[0].Replace('.', a);
                        coords[1] = coords[1].Replace('.', a);
                        decimal lat = 0;
                        decimal lng = 0;
                        if (coords.Length > 1)
                        {   // [OBS] LOCALIZAÇÃO NAO ATUALIZADA POR ESTA ROTINA, APENAS INSERE...
                            lat = Convert.ToDecimal(coords[0]);
                            lng = Convert.ToDecimal(coords[1]);
                        }

                        try
                        {
                            sql = "business.StoreInsert"; // Stored Procedure Name  
                            p = new DynamicParameters();

                            p.Add("@IN_StoreId", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                            p.Add("@IN_Name", productPriceType.Store);
                            p.Add("@IN_UserAgentId", productPriceType.UserAgentId);
                            p.Add("@IN_Source", "USER");
                            p.Add("@IN_ObjectTypeId", "DEF");
                            p.Add("@IN_StatusId", "ACT");
                            p.Add("@IN_Lat", lat);
                            p.Add("@IN_Lng", lng);

                            conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);

                            storeId = p.Get<int>("@IN_StoreId");
                        }
                        catch (Exception ex)
                        {
                            string XX = ex.Message;
                            errorCode = 997;
                            return errorCode;
                        }

                    }
                    else
                    {
                        storeId = productPriceType.StoreId;
                    }

                    try
                    {
                        sql = "business.ProductPriceInsert";
                        p = new DynamicParameters();

                        p.Add("@IN_UserAgentId", productPriceType.UserAgentId);
                        p.Add("@IN_Sku", productPriceType.Sku);
                        p.Add("@IN_LatLng", productPriceType.LatLng);
                        p.Add("@IN_Price", productPriceType.Price);
                        p.Add("@IN_Amount", productPriceType.Amount);
                        p.Add("@IN_Measurement", productPriceType.Measurement);
                        p.Add("@IN_StoreId", storeId);
                        p.Add("@IN_ShelfLife", productPriceType.ShelfLifeView);
                        p.Add("@IN_Deadline", productPriceType.DeadlineView);
                        p.Add("@IN_IsShoppingList", productPriceType.IsShoppingList);
                        p.Add("@IN_TargetName", productPriceType.TargetName);
                        p.Add("@IN_ActivityKey", productPriceType.ActivityKey);
                        p.Add("@IN_ERROR", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

                        conexao.Execute(sql, p, null, null, System.Data.CommandType.StoredProcedure);
                        errorCode = p.Get<int>("@IN_ERROR");
                    }
                    catch (Exception ex)
                    {
                        string XX = ex.Message;
                        errorCode = 998;
                        return errorCode;
                    }

                    /*
                    var list = new List<ProductPriceType>();
                    p = new DynamicParameters();
                    p.Add("@IN_Sku", productPriceType.Sku);
                    sql = "business.ProductPriceBySkuSelect";
                    using (var multi = conexao.QueryMultiple(sql, p, null, null, System.Data.CommandType.StoredProcedure))
                    {
                        list = multi.Read<ProductPriceType>().ToList();
                    }
                    return list;
                    */
                }

            }
            return errorCode;
        }
    }

}