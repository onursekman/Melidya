using log4net;
using Melidya.Admin.Panel.Models;

using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Melidya.Admin.Panel.Controllers
{
    public class LoginController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Login(LoginModel loginmodel)
        {
            if (ModelState.IsValid)
            {

            UserModel users = HttpHelper.SendRequestModel<UserModel>("https://melidyacikolata.com/api/", "User/GetUser", loginmodel, Method.POST);
            if (loginmodel.Password==users.Password)
            {
                HttpContext.Session.SetObjectAsJson("user", users);
                    log.Info("Login Başarılı");
                return RedirectToAction("Index", "Home");

            }
            else
            {
                    log.Error("Login şifre Tekrarı");
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                log.Error("Login ModelState.IsValid=false");
                return RedirectToAction("Index", "Login");

            }
            
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("User");
            return RedirectToAction("Index", "Login");
        }
    }
}