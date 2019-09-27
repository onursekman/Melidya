using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace MelidyaUI.Controllers
{
    public class AboutController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {

            if (ModelState.IsValid)
            {
                try
                {

                    log.Error("Hakkımızda sayfası çalışıyor");

                    return View();
                }
                catch (Exception ex)
                {

                    log.Error("Hakkımızda sayfası çalışmıyor" + " " + ex.Message);


                    return View("Index", "Error");
                }

            }
            else
            {
                log.Info("Hakkımızda sayfası çalışmıyor Model.State.IsValid==False");
                return View("Index", "Error");
            }

        }
    }
}