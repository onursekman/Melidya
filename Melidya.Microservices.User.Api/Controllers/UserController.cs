using MelidyaEntity.Core;
using MelidyaEntity.DataBase;
using MelidyaEntity.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Melidya.MicroservicesUser.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        Repository<User> repo = new Repository<User>();
        
        [HttpPost]
        public User GetUser(User user)
        {
            return repo.Find(x => x.Email == user.Email);
        }
        [HttpPost]
        public void AddUser(User user)
        {
          
            user.Password = user.Password.MD5Hash();
            repo.Insert(user);
            //TODO KONTROL KONULUCAK İİŞLEM BAŞARILIMI DEĞİL Mİ?
        }

        [HttpPost]
        public int UpdateUser(User user)
        {
            User u = repo.Find(x => x.ID == user.ID);
            u.Name = user.Name;
            u.Surname = user.Surname;
            u.Password = user.Password;
            u.Email = user.Email;
           int i= repo.Update(u);
            return i;
        }

     
  

    }
}