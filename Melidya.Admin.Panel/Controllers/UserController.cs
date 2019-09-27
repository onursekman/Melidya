using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using Melidya.Admin.Panel.Models;

using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Melidya.Admin.Panel.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
            if (user!=null)
            {

            return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult AddUser()
        {

            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
            if (user != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public JsonResult AddUser([FromBody]UserModel model)
        {
            if (ModelState.IsValid)
            {
               int i= HttpHelper.SendRequestModel<int>("https://melidyacikolata.com/api/", "User/AddUser", model, Method.POST);
                if (i==1)
                {
                    model.ResultCode = 200;
                    model.ResultMessage = "Güncelleme Başarılı";
                    return Json(model);
                }
                else
                {
                    model.ResultCode = 500;
                    model.ResultMessage = "Güncelleme Gerçekleşmedi";

                    return Json(model);
                }

               
            }
            else
            {
                model.ResultCode = 500;
                model.ResultMessage = "Güncelleme Gerçekleşmedi";

                return Json(model);
            }

        }

        public IActionResult UpdateUser()
        {
            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
            if (user != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public JsonResult UpdateUser([FromBody]UserModel model)
        {
            try
            {
              int i=  HttpHelper.SendRequestModel<int>("https://melidyacikolata.com/api/", "User/UpdateUser", model, Method.POST);
                if (i==1)
                {
                    model.ResultCode = 200;
                    model.ResultMessage = "Güncelleme Başarılı";
                    return Json(model);
                }
                else
                {
                    model.ResultCode = 500;
                    model.ResultMessage = "Güncelleme Gerçekleşmedi";
                    return Json(model);
                }
               
            }
            catch (Exception)
            {
                model.ResultCode = 500;
                model.ResultMessage = "Güncelleme Gerçekleşmedi";
                return Json(model);
            }
           
        }

        public IActionResult UserProfile()
        {
            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
            if (user != null)
            {

                UserModel model = HttpContext.Session.GetObjectFromJson<UserModel>("User");
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

     

    }
}