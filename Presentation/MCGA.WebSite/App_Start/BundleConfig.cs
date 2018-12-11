using System.Web;
using System.Web.Optimization;

namespace MCGA.WebSite
{
    [Compress]
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/lib/jquery-ui.min.js",
                         "~/Scripts/lib/moment.min.js",
                        "~/Scripts/stacktable.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/fullcalendar/fullcalendar.min.js",
                        "~/Scripts/locale-all.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-materia.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/fullcalendar.min.css",
                      "~/Scripts/lib/cupertino/jquery-ui.min.css",
                      "~/Content/site.css"));
        }
    }
}
