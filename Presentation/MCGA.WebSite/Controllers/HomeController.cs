using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Constants.HomeController;

namespace MCGA.WebSite.Controllers
{
    
    
    public class HomeController : Controller
    {
        [RequireHttps]
        [Compress]
        [Route("", Name = HomeControllerRoute.GetIndex)]
        public ActionResult Index()
        {
            return this.View(HomeControllerAction.Index);
        }

        [RequireHttps]
        [Compress]
        [Route("Nosotros", Name = HomeControllerRoute.GetAbout)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [RequireHttps]
        [Compress]
        [Route("Contactenos", Name = HomeControllerRoute.GetContact)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

  
    }
}