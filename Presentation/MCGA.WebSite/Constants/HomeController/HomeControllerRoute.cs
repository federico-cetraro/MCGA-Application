using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Constants.HomeController
{
    public static class HomeControllerRoute
    {
        public const string GetAbout = ControllerName.Home + "GetAbout";
        public const string GetContact = ControllerName.Home + "GetContact";       
        public const string GetIndex = ControllerName.Home + "GetIndex";
        public const string GetLogin = ControllerName.Home + "GetLogin";
        public const string GetRegister = ControllerName.Home + "GetRegister";
    }
}