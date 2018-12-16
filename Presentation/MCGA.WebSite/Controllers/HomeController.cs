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
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [RequireHttps]
        [Compress]
        [Route("", Name = HomeControllerRoute.GetIndex)]
        public ActionResult Index()
        {
            logger.Debug("Iniciando Index en Home Controller");
            return this.View(HomeControllerAction.Index);
        }

        [RequireHttps]
        [Compress]
        [Route("Nosotros", Name = HomeControllerRoute.GetAbout)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            logger.Debug("Pasamos por apartado NOSOTROS en  Home Controller");
            return View();
        }

        [RequireHttps]
        [Compress]
        [Route("Contactenos", Name = HomeControllerRoute.GetContact)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            logger.Debug("Pasamos por apartado CONTACTENOS en  Home Controller");
            return View();
        }

  
    }
}