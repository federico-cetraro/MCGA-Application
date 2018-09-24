using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Constants
{
    public class Application
    {
        public const string Name = "Clinica Embarazos de Riesgo Dr. Erick Rosenkratsz";
        public const string ShortName = "Clinica Dr. Erick Rosenkratsz";
        public const int SeoDescriptionLength = 260;
        public const string Email = "clinicarosenkratsz@sitio.com.ar";
        public const string Phone = "(+54-11) 5678-3211";

        public const string StreetAddress = "Ruta 67";
        public const string Locality = "CABA";
        public const string PostalCode = "5678";
        public const string Country = "Argentina";


        public const double Latitude = -69.667308627965596;
        public const double Longitude = -34.49549933992794;

       public static string Url => HttpContext.Current.Request.Url.AbsoluteUri;
    }
}