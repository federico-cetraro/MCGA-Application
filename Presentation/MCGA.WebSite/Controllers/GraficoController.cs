using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using MCGA.WebSite.Models;


namespace MCGA.WebSite.Controllers
{
    [Authorize]
    
    public class GraficoController : Controller
    {
        // GET: Grafico
        public ActionResult Index()
        {

            List<DataPoint> dataPoints = new List<DataPoint>
            {

                new DataPoint(1, 4),
                new DataPoint(2, 3),
                new DataPoint(3, 1),
                new DataPoint(4, 2),
                new DataPoint(5, 3),
            };

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}

            

