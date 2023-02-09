using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSO.Application.Services;
using System.Web.Mvc;
using SSO.Contract.Requests;

namespace SSO.Controllers
{
    public class GroupsController : Controller
    {
        public ActionResult Index()
        {
            GroupService groupService = new GroupService();
            return View(groupService.GetGroup());
        }
        //=================================================================
        public ActionResult AddGroup()
        {
            RoleService roleService = new RoleService();
            return View(roleService.GetRole());
        }
        [HttpPost]
        public JsonResult AddGroupPost(AddGroupRequest addGroupRequest)
        {
            JsonResult jr = new JsonResult();
            GroupService groupService = new GroupService();
            var rs = groupService.AddGroup(addGroupRequest);
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
        public ActionResult UpdateGroup(String id)
        {
            RoleService roleService = new RoleService();
            GroupService groupService = new GroupService();
            ViewBag.Role = roleService.GetRole();
            ViewBag.GroupRole = roleService.GetGroupRoleByGroupId(id);
            return View(groupService.GetGroupByID(id));

        }
        [HttpPost]
        public JsonResult UpdateGroupPost(UpdateGroupRequest updateGroupRequest)
        {
            JsonResult jr = new JsonResult();
            GroupService groupService = new GroupService();
            String update = groupService.UpdateGroup(updateGroupRequest);
            if (update == "successful")
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
        public JsonResult DeleteGroup(FormCollection collection)
        {
            JsonResult jr = new JsonResult();
            var id = collection["id"];
            GroupService groupService = new GroupService();
            groupService.DeleteGroup(id);
            jr.Data = new
            {
                status = "OK",
            };
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}