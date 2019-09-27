using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using MailKit.Net.Smtp;
using MailKit.Security;
using Melidya.Admin.Panel.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using RestSharp;

namespace Melidya.Admin.Panel.Controllers
{
    public class MessagesController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
                if (user!=null)
                {

                List<MessageModel> messageModels = HttpHelper.GetListMethod<List<MessageModel>>("https://melidyacikolata.com/api/", "Message/ListMessage", Method.GET);
                log.Info("Message Index Çalışıyor");
                return View(messageModels);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                log.Error("Message ModelState.IsValid=false");
                return RedirectToAction("Index", "Error");
            }
        }

        public IActionResult ReadMessage(int id)
        {
            if (ModelState.IsValid)
            {
                UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("user");
                if (user!=null)
                {

                MessageModel messageModel = HttpHelper.GetMethod<MessageModel>("https://melidyacikolata.com/api/", "Message/ReadMessage", Method.GET, id);
                log.Info("ReadMessage çalışıyor");
                return View(messageModel);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                log.Error("ReadMessage ModelState.IsValid=false");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public IActionResult ReplyMessage(int id)
        {
            MessageModel model = HttpHelper.GetMethod<MessageModel>("https://melidyacikolata.com/api/", "Message/ReadMessage", Method.GET, id);
            SenderMessageModel smodel = new SenderMessageModel();
            smodel.MessageID = model.ID;
            smodel.Email = model.Email;
            return View(smodel);
        }
        [HttpPost]
        public IActionResult ReplyMessage(SenderMessageModel model)
        {
            UserModel user = HttpContext.Session.GetObjectFromJson<UserModel>("User");
            model.SenderID = user.ID;
            var message = new MimeMessage();
            //Setting the To e-mail address
            message.To.Add(new MailboxAddress(model.Email));
            //Setting the From e-mail address
            message.From.Add(new MailboxAddress(user.Email));
            //E-mail subject 
            message.Subject = model.MessageTitle;
            //E-mail message body
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = model.MessageDetail  + " E-mail: " + model.Email
            };

            //Configure the e-mail
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                emailClient.Authenticate("melidyacikolata@gmail.com", "melidya2018");
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
            HttpHelper.SendRequestModel<SenderMessageModel>("https://melidyacikolata.com/api/", "Message/ReplyMessage", model, Method.POST);
            return RedirectToAction("Index","Home");
        }


    }
}
