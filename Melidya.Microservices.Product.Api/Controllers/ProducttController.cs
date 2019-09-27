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
    public class ProducttController : ControllerBase
    {
        Repository<Product> repo = new Repository<Product>();

        [HttpGet]
        public List<Product> GetListProduct()
        {
            return repo.List();
        }
        [HttpGet]
        public List<Product> GetProducts(int id)
        {
            List<Product> pro= repo.List(x => x.CategoryID == id);
            return pro;
        }
        [HttpPost]
        public List<Product> GetProductsPage(Product product)
        {
            List<Product> pro = repo.List(x => x.CategoryID == product.CategoryID && x.PageNumber == product.PageNumber);
            return pro;
        }
        [HttpGet]
        public Product GetProductDetail(int id)
        {
            return repo.Find(x => x.ID == id);
        }
        [HttpPost]
        public int ProductUpdate(Product product)
        {
            Product p = repo.Find(x => x.ID == product.ID);


            p.Image = product.Image;
            p.ProductDetail = product.ProductDetail;
            p.ProductName = product.ProductName;
          int i=  repo.Update(p);
            return i;
        }
        public int ProductInsert(Product product)
        {
           int i= repo.Insert(product);
            return i;
        }
        [HttpDelete]
        public void ProductDelete(int id)
        {
            Product product = repo.Find(x => x.ID == id);

            repo.Delete(product);
        }

        [HttpGet]
        public List<Product> GetPage(int page)
        {
            return repo.List(x => x.PageNumber == page);
        }
    }
}