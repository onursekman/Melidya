using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Melidya.Admin.Panel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using RestSharp;

namespace Melidya.Admin.Panel.Controllers
{
    public class CategoryController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFileProvider fileProvider;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration Configuration;

        public CategoryController(IFileProvider fileprovider, IHostingEnvironment env, IConfiguration configuration)
        {
            fileProvider = fileprovider;
            hostingEnvironment = env;
            Configuration = configuration;
        }
        public IActionResult GetCategory()
        {
            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
            if (ModelState.IsValid)
            {
                if (user != null)
                {

                    try
                    {
                        List<CategoryModel> category = HttpHelper.GetListMethod<List<CategoryModel>>("https://melidyacikolata.com/api/", "Category/GetCategories", Method.GET);
                        log.Info("GetCategory Çaloşıyor");
                        return View(category);

                    }
                    catch (Exception ex)
                    {
                        log.Error("GetCategory Hata!! " + ex.Message);
                        return RedirectToAction("Index", "Error");

                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");

                }
            }
            else
            {
                log.Error("GetCategory ModelState.IsValid=false");
                return RedirectToAction("Index", "Error");
            }
        }
        public IActionResult CategoryInsert()
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
        public async Task<IActionResult> CategoryInsert(CategoryModel categoryModel, IFormFile file)
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




                categoryModel.Image = path;
                log.Info("CategoryInsert Başarılı ");

                int i = HttpHelper.SendRequestModel<int>("https://melidyacikolata.com/api/", "Category/CategoryInsert", categoryModel, Method.POST);

                return View(categoryModel);


            }
            else
            {
                log.Error("CategoryInsert Hata!!");
                return View(categoryModel);
            }


        }
        public IActionResult CategoryUpdate(int id)
        {
            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    CategoryModel category = HttpHelper.GetMethod<CategoryModel>("https://melidyacikolata.com/api/", "Category/GetCategory", Method.GET, id);
                    log.Info("CategoryUpdate Başarılı");
                    return View(category);

                }
                else
                {
                    return RedirectToAction("Index", "Login");

                }
            }
            else
            {
                log.Error("CategoryUpdate ModelState.Isvalid=false");
                return RedirectToAction("Index", "Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CategoryUpdate(CategoryModel categoryModel, IFormFile file)
        {

            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
            if (user != null)
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




                    categoryModel.Image = path;
                }

                HttpHelper.SendRequestModel<CategoryModel>("https://melidyacikolata.com/api/", "Category/CategoryUpdate", categoryModel, Method.POST);
                log.Info("CategoryUpdate Başarılı");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        public IActionResult CategoryDelete(int id)
        {
            if (ModelState.IsValid)
            {
                UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
                if (user != null)
                {

                    HttpHelper.GetMethod<CategoryModel>("https://melidyacikolata.com/api/", "Category/CategoryDelete", Method.DELETE, id);

                    log.Info("CategoryDelete Başarılı");
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                log.Error("CategoryDelete ModelState.Isvalid=false");
                return RedirectToAction("Index", "Error");
            }

        }
    }
}