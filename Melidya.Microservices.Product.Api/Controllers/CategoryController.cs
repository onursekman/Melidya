using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MelidyaEntity.DataBase;
using MelidyaEntity.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Melidya.MicroservicesProductt.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        Repository<Category> repo = new Repository<Category>();
        [HttpGet]
        public List<Category> GetCategories()
        {
            List<Category> categories = repo.List();
            return categories;
        }
        [HttpPost]
        public int CategoryInsert(Category category)
        {
            Category cat = new Category
            {
                CategoryName = category.CategoryName,
                Image = category.Image
            };
            int i = repo.Insert(cat);
            return i;

        }
        [HttpGet]
        public Category GetCategory(int id)
        {
            return repo.Find(x => x.CategoryID == id);
        }
        [HttpPost]
        public void CategoryUpdate(Category category)
        {

            Category cat = repo.Find(x => x.CategoryID == category.CategoryID);
            cat.CategoryName = category.CategoryName;
            cat.Image = category.Image;
            repo.Update(cat);


        }
        [HttpDelete]
        public void CategoryDelete(int id)
        {
            Category category = repo.Find(x => x.CategoryID == id);
            repo.Delete(category);
        }
    }
}