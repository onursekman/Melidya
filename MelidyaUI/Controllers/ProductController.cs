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
    public class ProductController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index(int id)
        {
            log.Info("Ürün Sayfası çalışıyor");
     

            HttpContext.Session.SetObjectAsJson("CategoryID", id.ToString());
            List<ProductModel> productModels = HttpHelper.GetMethod<List<ProductModel>>("https://localhost:44391/api/", "Productt/GetProducts", Method.GET, id);

            return View(productModels);
        }

        public IActionResult ProductDetail(int id)
        {
            log.Info("Ürün Detayı istendi");


            ProductModel productModel = HttpHelper.GetMethod<ProductModel>("https://localhost:44391/api/", "Productt/GetProductDetail", Method.GET, id);
            if (productModel != null)
            {

                return View(productModel);
            }
            else
            {
                log.Error("ürün detayı boş geldi");
                return RedirectToAction("Index", "Error");
            }

        }

        public IActionResult ProductPagedlist(int id)
        {
            ProductModel model = new ProductModel();
            model.CategoryID = HttpContext.Session.GetObjectFromJson<int>("CategoryID");
            model.PageNumber = id;
            List<ProductModel> productModels = HttpHelper.SendRequestModel<List<ProductModel>>("https://localhost:44391/api/", "Productt/GetProductsPage", model, Method.POST);
            ViewBag.sayfa = productModels.Count();
            return View("Index", productModels);
        }
    }
}