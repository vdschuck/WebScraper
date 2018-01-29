using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebScraper.Enum;
using WebScraper.Services;

namespace WebScraper.Controllers
{
    public class WebScraperController : Controller
    {
        private readonly NuBankServices nuBankServices = new NuBankServices();

        // GET: WebScraper
        public ActionResult Index()
        {
            ViewBag.WebsiteList = WebScraperEnum.GetWebSites();

            return View();
        }

        // POST: WebScraper/Index
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string[]> data = new List<string[]>();

                    // Login method call 
                    if (collection["Website"].Equals(WebScraperEnum.WebSites.NuBank.ToString()))
                        data = nuBankServices.Login(collection["User"], collection["Password"]);

                    if (data.Count == 0)
                        ViewData["Erro"] = "Usuário ou senha inválidos! ";
                    else
                        ViewBag.List = data.ToList();                    
                }

                ViewBag.WebsiteList = WebScraperEnum.GetWebSites();
                return View("Index");
            }          
            catch (Exception ex)
            {
                ViewData["Erro"] = "Erro inesperado, tente novamente! ";
                Console.WriteLine(ex.ToString());
                ViewBag.WebsiteList = WebScraperEnum.GetWebSites();
                return View();
            }
        }
       
    }
}
