using SSO.Application.Services;
using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSO.Controllers
{
    public class RolesController : Controller
    {
        public ActionResult Index()
        {
            RoleService roleService = new RoleService();
            return View(roleService.GetRole());
        }
        //=================================================================
        public ActionResult AddRole()
        {
            AppService appService = new AppService();
            return View(appService.GetApp());
        }
        [HttpPost]
        public JsonResult AddRolePost(VTC_Roles roles)
        {
            JsonResult jr = new JsonResult();
            RoleService roleService = new RoleService();
            var rs = roleService.AddRole(roles);
            if (rs == "successful")
            {
                jr.Data = new
                {
                    status = "OK",
                };
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                jr.Data = new
                {
                    status = "ER",
                };
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
        }
        //=================================================================
        public ActionResult UpdateRole(String id)
        {
            RoleService roleService = new RoleService();
            AppService appService = new AppService();
            ViewBag.App = appService.GetApp();
            return View(roleService.GetRoleByID(id));

        }
        [HttpPost]
        public JsonResult UpdateRolePost(VTC_Roles roles)
        {
            JsonResult jr = new JsonResult();
            RoleService roleService = new RoleService();
            String add = roleService.UpdateRole(roles);
            if (add == "successful")
            {
                jr.Data = new
                {
                    status = "OK",
                };
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                jr.Data = new
                {
                    status = "ER",
                };
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
        }
        //=================================================================
        public JsonResult DeleteApp(FormCollection collection)
        {
            JsonResult jr = new JsonResult();
            RoleService roleService = new RoleService();
            roleService.DeleteRole(collection["id"]);
            jr.Data = new
            {
                status = "OK",
            };
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}