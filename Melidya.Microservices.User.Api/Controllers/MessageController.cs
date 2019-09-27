using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MelidyaEntity.DataBase;
using MelidyaEntity.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Melidya.MicroservicesUser.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        Repository<Message> repo = new Repository<Message>();
        Repository<SenderMessage> srepo = new Repository<SenderMessage>();
        [HttpGet]
        public List<Message> ListMessage()
        {
            return repo.List();
        }
        public Message ReadMessage(int id)
        {
            return repo.Find(x => x.ID == id);
        }

        public void ReplyMessage(SenderMessage message)
        {
            srepo.Insert(message);
        }
        [HttpPost]
        public int SendMessage(Message message)
        {
            int i= repo.Insert(message);
            return i;
        }
    }
}