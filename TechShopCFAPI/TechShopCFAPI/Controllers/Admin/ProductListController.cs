using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repositories.AdminModule;

namespace TechShopCFAPI.Controllers.Admin
{
    [RoutePrefix("api/products")]
    public class ProductListController : ApiController
    {
        ProductRepository productRepository = new ProductRepository();

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(productRepository.GetAll());
        }

        [Route("productName")]
        public IHttpActionResult Get(string productName)
        {
            if (productName != "")
            {
                return Ok(productRepository.GetByName(productName));
            }
            return Ok(StatusCode(HttpStatusCode.NotFound));
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Product product = productRepository.Get(id);
            if (product == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(product);
        }

        [Route("")]
        public IHttpActionResult PostProduct(Product product)
        {
            product.DateAdded = DateTime.Now;
            product.LastUpdated = DateTime.Now;
            productRepository.Insert(product);
            return Ok();
        }

        [Route("{id}")]
        public IHttpActionResult PutProduct([FromBody]Product product, [FromUri]int id)
        {
            product.Id = id;
            product.LastUpdated = DateTime.Now;
            productRepository.Update(product);
            return Ok();
        }

        [Route("add/{id}")]
        public IHttpActionResult PutAddQuantity([FromBody]Product product, [FromUri]int id)
        {
            
            product.Id = id;
            product.Quantity += product.NewQuantity;
            product.LastUpdated = DateTime.Now;
            productRepository.Update(product);
            return Ok();
        }


    }
}
