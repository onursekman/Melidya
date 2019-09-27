using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Melidya.Admin.Panel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;

namespace Melidya.Admin.Panel.Controllers
{
    public class HomeController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
                if (user!=null)
                {

                HomeModel homeModel = new HomeModel();
                homeModel.productModels = HttpHelper.GetListMethod<List<ProductModel>>("https://melidyacikolata.com/api/", "Productt/GetListProduct", Method.GET);
                homeModel.categoryModels = HttpHelper.GetListMethod<List<CategoryModel>>("https://melidyacikolata.com/api/", "Category/GetCategories", Method.GET);
                homeModel.messageModels = HttpHelper.GetListMethod<List<MessageModel>>("https://melidyacikolata.com/api/", "Message/ListMessage", Method.GET);
                UserModel users = HttpHelper.SendRequestModel<UserModel>("https://melidyacikolata.com/api/", "User/GetUser", user, Method.POST);

                HttpContext.Session.SetObjectAsJson("User", users);
                int urunsayisi = 0;
                int categorysayisi = 0;
                foreach (ProductModel item in homeModel.productModels)
                {
                    urunsayisi++;
                }
                foreach (CategoryModel item in homeModel.categoryModels)
                {
                    categorysayisi++;

                }
                ViewBag.Name = users.Name.ToString() + users.Surname.ToString();
                ViewBag.urunsayisi = urunsayisi.ToString();
                ViewBag.categorysayisi = categorysayisi.ToString();
                log.Info("HomeController Çalışıyor");
                return View(homeModel);
                }
                else
                {
                    return RedirectToAction("Index","Login");

                }

            }
            else
            {
                log.Error("HomeController ModelState.IsValid=false");
                return RedirectToAction("Index", "Error");
            }
            
          
        }
    }
}