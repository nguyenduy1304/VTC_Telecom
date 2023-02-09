using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSO.Controllers
{
    public class AbstractControllerClass : Controller
    {
        public string CheckLogged()
        {
            var cookie = Request.Cookies["TokenCookies"] != null;
            if (cookie == true)
            {
            //    string cookievalue = Request.Cookies["TokenCookies"].Value;
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(cookievalue);
            //    String permission = securityToken.Claims.FirstOrDefault(c => c.Type == "permission")?.Value;
                return "";
            }
            else
            {
                return null;
            }
        }



    }
}