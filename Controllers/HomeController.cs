using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mercoplano.Maisbarato.Server.RESTful.Models;
using Microsoft.AspNetCore.Http.Extensions;

namespace Mercoplano.Maisbarato.Server.RESTful.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Inicio";

            /*
            if (Request.GetDisplayUrl().Contains("receitafederal.com.br"))
            {
                return RedirectToAction("News", "Maisbarato");
                // return RedirectToAction("Cpf_Serasa_Spc_Receita_Federal_Leilao", "RFB");
            }
            else
            {
                return RedirectToAction("News", "Maisbarato");                
            }
            */

            return RedirectToAction("Index", "Maisbarato");
        }


        public IActionResult Privacypolicy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Página de contato.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
