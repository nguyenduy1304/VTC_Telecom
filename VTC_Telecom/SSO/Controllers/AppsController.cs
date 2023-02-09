using SSO.Application.Services;
using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSO.Controllers
{
    public class AppsController : Controller
    {
        public ActionResult Index()
        {
            AppService appService = new AppService();
            return View(appService.GetApp());
        }

        [HttpPost, ActionName("Index")]
        public ActionResult AddApp(VTC_Apps app)
        {
            AppService appService = new AppService();
            var rs = appService.AddApp(app);
            if (rs == "successful")
            {
                return RedirectToAction("Index", "Apps");
            }
            else if(rs== "ER_AppId")
            {
                ViewBag.ERROR = "ID ứng dụng đã tồn tại.";
                return View();
            }
            ViewBag.ERROR = "Thêm không thành công vui lòng kiểm tra lại.";
            return View();
        }
        //=================================================================
        public ActionResult UpdateApp(String id)
        {
            AppService appService = new AppService();
            return View(appService.GetAppByID(id));

        }
        [HttpPost]
        public JsonResult UpdateAppPost(VTC_Apps apps)
        {
            JsonResult jr = new JsonResult();
            AppService appService = new AppService();
            String add = appService.UpdateApp(apps);
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
            var id = collection["id"];
            AppService appService = new AppService();
            appService.DeleteApp(id);
            jr.Data = new
            {
                status = "OK",
            };
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}