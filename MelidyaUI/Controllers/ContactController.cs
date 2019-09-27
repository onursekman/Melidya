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
    public class ContactController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {

                try
                {
                    log.Info("İletişim sayfası çalışıyor");
                    return View();
                }
                catch (Exception ex)
                {
                    log.Error("İletişim sayfası ÇALIŞMIYOR" + ex.Message);
                    return RedirectToAction("Index", "Error");

                }
            }
            else
            {
                log.Error("İletişim sayfası ÇALIŞMIYOR ModelState.IsValid=false");
                return RedirectToAction("Index", "Error");
            }

        }
        [HttpPost]
        public IActionResult Index(MessageModell message)
        {
            MessageModell model = new MessageModell();
            if (ModelState.IsValid)
            {
                try
                {
                    log.Info("İletişim sayfasına girildi");
                    message.Date = DateTime.Now;
                    int i = HttpHelper.SendRequestModel<int>("https://localhost:44383/api/", "Message/SendMessage", message, Method.POST);
                    if (i == 1)
                    {
                        model.Success = true;
                        model.ResultCode = 200;
                        model.ResultMessage = "Mesaj Gönderimi Başarılı";
                        ViewBag.Message = model.ResultCode = 200;



                    }
                    else
                    {
                        model.Success = false;
                        model.ResultCode = 500;
                        model.ResultMessage = "Mesaj Gönderimi Gerçekleşmedi!!";
                        ViewBag.Message = model.ResultCode = 500;
                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    log.Error("İletişim sayfasında hata oluştu" + ex.Message);
                    model.Success = false;
                    model.ResultCode = 500;
                    model.ResultMessage = "Mesaj Gönderimi Gerçekleşmedi!! Beklenmeyen Bir Hata ile Karşılaşıldı Site Yöneticisi İle İletişime Geçin";
                    ViewBag.Message = model.ResultCode = 500;
                    return View(model);
                }


            }
            else
            {
                log.Error("Contact Sayfasında hata oluştu ModelState.IsValid=false");
                model.Success = false;
                model.ResultCode = 500;
                model.ResultMessage = "Mesaj Gönderimi Gerçekleşmedi!! Boş Alanları Doldurun";
                ViewBag.Message = model.ResultCode = 500;
                return View(model);
            }

        }
    }
}