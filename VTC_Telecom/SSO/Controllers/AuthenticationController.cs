using SSO.Application.Services;
using SSO.Contract.Constant;
using SSO.Contract.Requests;
using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SSO.Controllers
{
    public class AuthenticationController : Controller
    {
        #region Authen
        private string link = "https://localhost:44350/";
        //private string link = "http://vtcsso.com/";
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public ActionResult SignIn(LoginRequest loginRequest)
        {
            var domainSSO = Request.UrlReferrer.ToString();
            var checkSSO = link;
            if (loginRequest.UserName != null && loginRequest.PassWord != null)
            {
                if (domainSSO == checkSSO)
                {
                    UserService userService = new UserService();
                    loginRequest.AppId = "VTCSSO";
                    var login = userService.LogIn(loginRequest);
                    if (login != null)
                    {
                        HttpCookie TokenCookies = new HttpCookie("SSO_Cookies");
                        TokenCookies.Value = login;
                        TokenCookies.Expires = DateTime.Now.AddHours(24);
                        TokenCookies.Domain = Request.Url.Host.ToString();
                        Response.Cookies.Add(TokenCookies);

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(login);
                        Session["Username"] = securityToken.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
                        String permission = securityToken.Claims.FirstOrDefault(c => c.Type == "permission")?.Value;
                        if (permission.Contains("update") == true)
                        {
                            return RedirectToAction("users", "authentication");
                        }
                        else
                        {
                            return RedirectToAction("Unauthorized", "Error");
                        }
                    }
                }
                else
                {
                    var prevPage = Request.UrlReferrer.ToString().Split('=');
                    string appid = prevPage[1].ToUpper();
                    AppService appService = new AppService();
                    VTC_Apps vTC_Apps = appService.GetAppByAppId(appid);
                    if (vTC_Apps == null)
                    {
                        ViewBag.ERROR = string.Format(MessageError.NotEmptyApp, appid);
                        return View();
                    }
                    loginRequest.AppId = vTC_Apps.AppId;
                    string redirectURL = vTC_Apps.Domain;
                    UserService userService = new UserService();
                    var login = userService.LogIn(loginRequest);
                    if (login != null)
                    {
                        HttpCookie TokenCookies = new HttpCookie("SSO_Cookies");
                        TokenCookies.Value = login;
                        TokenCookies.Expires = DateTime.Now.AddHours(24);
                        TokenCookies.Domain = Request.Url.Host.ToString();
                        Response.Cookies.Add(TokenCookies);
                        byte[] bytes = Encoding.UTF8.GetBytes("B68A4717A0BE21353585823D2532E6AD");
                        string encryptedText = AESEncryption.Encrypt(login, bytes);

                        return Redirect(redirectURL + "?token=" + encryptedText);
                    }
                    return View();
                }
            }
            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

          
            //    else
            //    {
            //        var prevPage = Request.UrlReferrer.ToString().Split('=');
            //        string appid = prevPage[1];
            //        AppService appService = new AppService();
            //        VTC_Apps vTC_Apps = appService.GetAppByAppId(appid);
            //        if (vTC_Apps == null)
            //        {
            //            ViewBag.ERROR = string.Format(MessageError.NotEmptyApp, appid);
            //            return View();
            //        }
            //        else
            //        {
            //            loginRequest.Domain = vTC_Apps.Domain;
            //            string redirectURL = vTC_Apps.Domain;
            //            UserService userService = new UserService();
            //            var login = userService.LogIn(loginRequest);
            //            if (login != null)
            //            {
            //                HttpCookie TokenCookies = new HttpCookie("TokenCookies");
            //                TokenCookies.Value = login;
            //                TokenCookies.Expires = DateTime.Now.AddHours(24);
            //                TokenCookies.Domain = Request.Url.Host.ToString();
            //                Response.Cookies.Add(TokenCookies);
            //                return Redirect(redirectURL + "?token=" + login);
            //            }
            //            else
            //            {
            //                ViewBag.ERROR = "Sai tên đăng nhập hoặc tài khoản";
            //                return View();
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    ViewBag.ERROR = string.Format(MessageError.NotEmpty, "Username, Password");
            //    return View();
            //}
            //return RedirectToAction("users", "authentication");

        



        #endregion















        #region Management
        public ActionResult Users()
        {
            UserService userService = new UserService();
            return View(userService.GetUsers());
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost, ActionName("Register")]
        public ActionResult AddAccount(RegisterRequest registerRequest)
        {
            UserService userService = new UserService();
            var register = userService.Register(registerRequest);
            if (register == "successful")
            {
                return RedirectToAction("Index", "Authentication");
            }
            else if (register == "ER_Username")
            {
                ViewBag.ERROR = "Tài khoản đã tồn tại";
                return View();
            }
            else
            {
                ViewBag.ERROR = "Tạo tài khoản thất bại, vui lòng kiểm tra lại";
                return View();
            }
        }
        public ActionResult UpdateUser(String id)
        {

            UserService userService = new UserService();
            GroupService groupService = new GroupService();
            var groupUser = userService.GetGroupUserByUserId(id);
            var groups = groupService.GetGroup();
            ViewBag.GroupUser = groupUser;
            ViewBag.Groups = groups;
            return View(userService.GetUsersById(id));

        }

        [HttpPost]
        public JsonResult UpdateUserPost(FormCollection collection)
        {
            JsonResult jr = new JsonResult();
            EditUserRequest editUserRequest = new EditUserRequest();
            editUserRequest.Id = collection["id"];
            editUserRequest.FullName = collection["fullname"];
            editUserRequest.Email = collection["email"];
            editUserRequest.Phone = collection["phone"];
            var birth = collection["Birthday"];
            if (birth.Length == 0)
            {
                editUserRequest.Birthday = null;
            }
            else
            {
                editUserRequest.Birthday = DateTime.Parse(birth);
            }
            editUserRequest.Address = collection["Address"];
            editUserRequest.GroupId = collection["groupid"];
            UserService userService = new UserService();
            userService.UpdateUser(editUserRequest);
            jr.Data = new
            {
                status = "OK",
                message = "Cập nhật thành công"
            };
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteUser(String id)
        {
            UserService userService = new UserService();
            return View(userService.GetUsersById(id));
        } 
        [HttpPost]
        public ActionResult DeleteUserPost(String id)
        {
            UserService userService = new UserService();
            userService.DeleteUser(id);
            return RedirectToAction("users", "authentication");
        }
        #endregion
    }
}