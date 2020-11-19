using System;
using System.Collections.Generic;
using System.Linq;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    [Route("api/[controller]")]
    [Route("api/ProductNoSign/[action]")]
    public class ProductNoSignController : Controller
    {
        private IConfiguration _configuration;

        public ProductNoSignController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [ActionName("GetPrices")]
        public List<ProductPriceType> Get(string sku, string latLng)
        {// Leitura de preços sem usuário logado - todos vem como preços nao editavel - pois o acesso eh anomino
            
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


            List<ProductPriceType> list = new List<ProductPriceType>();
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("MaisbaratoAppEntities")))
            {

                var sql = "business.ProductPriceSkuNoSignSelect"; // [TODO] PRECISA ATUALIZAR APROCEDURE DARA RADIUS NEX
                var p = new DynamicParameters();
                p.Add("@IN_Sku", sku);
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

    }
}
