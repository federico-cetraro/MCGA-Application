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
        //metodos post para chequear que vuelva a funcionar
        public const string PostRegister = ControllerName.Home + "PostRegister";
        public const string PostLogin = ControllerName.Home + "PostLogin";
    }
}