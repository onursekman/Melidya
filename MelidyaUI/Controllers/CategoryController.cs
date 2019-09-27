using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using MelidyaUI.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace MelidyaUI.Controllers
{
    public class CategoryController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    log.Info("Ürün Sayfası çalışıyor");
                    List<CategoryModel> Categories = HttpHelper.GetListMethod<List<CategoryModel>>("https://localhost:44391/api/", "Category/GetCategories", Method.GET);

                    return View(Categories);
                }
                catch (Exception ex)
                {
                    log.Error("Ürün Sayfası çalışmıyor" + " " + ex.Message);

                    return RedirectToAction("Index", "Error");

                }

            }
            else
            {
                log.Info("Ürün Sayfası çalışmıyor Model.State.IsValid==False");
                return RedirectToAction("Index", "Error");
            }

        }
    }
}