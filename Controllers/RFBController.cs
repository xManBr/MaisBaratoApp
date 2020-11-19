using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    public class RFBController : Controller
    {
        private IConfiguration _configuration;

        public RFBController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //
        // GET: /RFB/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cpf_Serasa_Spc_Receita_Federal_Leilao()
        {
            return View();
        }

        public ActionResult Cpf_Cnpj_IRPF_IRPJ_Comex_Receita_Federal_Brasil()
        {
            return View();
        }
        public ActionResult News()
        {
            return View();
        }
	}
}