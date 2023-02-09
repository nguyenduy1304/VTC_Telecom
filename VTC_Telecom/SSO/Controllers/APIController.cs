using SSO.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSO.Controllers
{
    public class APIController : Controller
    {
        public JsonResult Token(String id)
        {
            UserService userService = new UserService();
            var token = userService.GetTokenByUser(id);
            var js = new
            {
                status = "OK",
                id = token.Id,
                userid = token.UserId,
                token = token.Token,
                loginDate = String.Format("{0:d/M/yyyy HH:mm:ss}", token.LoginDate),
                expired = String.Format("{0:d/M/yyyy HH:mm:ss}", token.Expired),
            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}