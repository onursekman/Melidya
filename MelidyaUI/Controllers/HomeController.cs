using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MelidyaUI.Models;

using log4net;
using RestSharp;

namespace MelidyaUI.Controllers
{
    public class HomeController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    log.Info("Anasayfa Çalışıyor");
                    HomeModel model = new HomeModel();
                    model.ProductModel = HttpHelper.SendRequest<List<ProductModel>>("https://localhost:44391/api/", "Productt/GetListProduct", Method.GET);
                    model.CategoryModel = HttpHelper.SendRequest<List<CategoryModel>>("https://localhost:44391/api/", "Category/GetCategories", Method.GET);
                    return View(model);

                }
                catch (Exception ex)
                {
                    log.Error("Anasayfa'da Hata!!" + ex.Message);
                    return RedirectToAction("Index", "Error");

                }

            }
            else
            {
                log.Error("Anasayfa'da Hata!! ModelState.IsValid=false");
                return RedirectToAction("Index", "Error");

            }

        }
    }
}
