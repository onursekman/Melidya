using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Helpers;
using log4net;
using Melidya.Admin.Panel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using MimeKit;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;

namespace Melidya.Admin.Panel.Controllers
{
    public class ProductController : Controller
    {

        private readonly IFileProvider fileProvider;
        private readonly IConfiguration Configuration;

        private readonly IHostingEnvironment hostingEnvironment;
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProductController(IFileProvider fileprovider, IHostingEnvironment env, IConfiguration configuration)
        {
            fileProvider = fileprovider;
            hostingEnvironment = env;
            Configuration = configuration;

        }




        public IActionResult ProductDetail(int id)
        {
            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
            if (ModelState.IsValid)
            {
                if (user != null)
                {

                    ProductModel productModel = HttpHelper.GetMethod<ProductModel>("https://melidyacikolata.com/api/", "Productt/GetProductDetail", Method.GET, id);
                    var webPath = Configuration.GetSection("Image:path");
                    var path = Path.Combine("", webPath.Value + productModel.Image);


                    productModel.Image = path;
                    log.Info("ProductDetail Çalışıyor");
                    return View(productModel);

                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                log.Error("ProductDetail ModelState.IsValid=false");
                return RedirectToAction("Index", "Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel product, IFormFile file)
        {

            if (file != null || file.Length != 0)
            {
                // Create a File Info 
                FileInfo fi = new FileInfo(file.FileName);


                var newFilename = "_" + String.Format("{0:d}",
                                  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                var webPath = Configuration.GetSection("Image:path");


                var path = Path.Combine("", webPath.Value + newFilename);
                // This stream the physical file to the allocate wwwroot/ImageFiles folder
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }

                // This save the path to the record
                product.Image = path;

                HttpHelper.SendRequestModel<ProductModel>("https://melidyacikolata.com/api/", "Productt/ProductUpdate", product, Method.POST);
                log.Info("ProductUpdate Çalışıyor");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                product.ResultCode = 500;
                product.ResultMessage = "Hata oluştu Güncellenme Yapılamadı";
                product.Success = false;
                log.Error("ProductUpdate ModelState.IsValid=false");
                return View(product);
            }

        }
        public IActionResult ProductInsert()
        {
            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
            if (user != null)
            {
                List<CategoryModel> categories = HttpHelper.GetListMethod<List<CategoryModel>>("https://melidyacikolata.com/api/", "Category/GetCategories", Method.GET);
                CategoryModel model = new CategoryModel();
                var groupList = new List<SelectListItem>();
                foreach (var item in categories)
                {
                    groupList.Add(new SelectListItem(item.CategoryName, item.CategoryID.ToString()));

                }

                model.CategorySelectList = groupList;
                return View(model);

            }
            else
            {
                return RedirectToAction("Index", "Login");

            }
        }

        [HttpPost]
        public async Task<IActionResult> ProductInsert(ProductModel product, CategoryModel category, IFormFile file)
        {
            if (file != null || file.Length != 0)
            {
                // Create a File Info 
                FileInfo fi = new FileInfo(file.FileName);


                var newFilename = "_" + String.Format("{0:d}",
                                  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                var webPath = Configuration.GetSection("Image:path");

                //TODO: bu path değeri configurasyon dosyasından alınması gerek. Çünkü burası linuxte farklı adres olacak, windowsta farklı adres olacak. 
                // Koddan değiştirerek olmaz. Configurasyon dosyasından değişmemiz gerek.  @"c:/ImageFiles/" kısmından bahsediyorum. 
                var path = Path.Combine("", webPath.Value + newFilename);


                var pathToSave = @"/ImageFiles/" + newFilename;


                //This stream the physical file to the allocate wwwroot / ImageFiles folder
                using (var stream = new FileStream(path, FileMode.CreateNew))
                {
                    await file.CopyToAsync(stream);

                }


                product.Image = pathToSave;

            }

            product.CategoryID = category.CategoryID;

            List<ProductModel> productModels = HttpHelper.GetMethod<List<ProductModel>>("https://melidyacikolata.com/api/", "Category/GetCategories", Method.GET, category.CategoryID);
            if (productModels.Count < 20)
            {

                product.PageNumber = 1;
            }
            else if (productModels.Count < 40)
            {
                product.PageNumber = 2;

            }
            else if (productModels.Count < 60)
            {
                product.PageNumber = 3;
            }
            else if (productModels.Count < 80)
            {
                product.PageNumber = 4;
            }
            else if (productModels.Count < 100)
            {
                product.PageNumber = 5;
            }
            else
            {
                product.PageNumber = 6;
            }
            HttpHelper.SendRequestModel<ProductModel>("https://melidyacikolata.com/api/", "Productt/ProductInsert", product, Method.POST);
            return RedirectToAction("/Admin/Index", "Home");

        }
        public IActionResult ProductDelete(int id)
        {
            var i = HttpHelper.GetMethod<ProductModel>("https://melidyacikolata.com/api/", "Productt/ProductDelete", Method.DELETE, id);

            return RedirectToAction("Index", "Home");
        }
    }
}