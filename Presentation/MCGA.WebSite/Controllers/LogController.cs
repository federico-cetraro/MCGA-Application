using MCGA.Data;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MCGA.WebSite.Controllers
{
    public class LogController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: Log
        [Authorize]
        public ActionResult LogView()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult DescargarLogPDF() => new ViewAsPdf(@"LogView_PDF", db)
        {
            FileName = "Descarga-Log-" + DateTime.Today + ".pdf",
            PageSize = Rotativa.Options.Size.A4,
            PageOrientation = Rotativa.Options.Orientation.Landscape,
            CustomSwitches = "--footer-center \"  Creado: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Pagina: [page]/[toPage]\"" +
          " --footer-line --footer-font-size \"12\" --footer-spacing 1 --footer-font-name \"Segoe UI\"",
            PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 }
            
        };
        
    }
}