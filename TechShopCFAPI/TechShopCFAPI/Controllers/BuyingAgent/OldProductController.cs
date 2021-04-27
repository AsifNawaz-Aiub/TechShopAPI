using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;

namespace TechShopCFAPI.Controllers.BuyingAgent
{
    [RoutePrefix("api/old_product")]
    public class OldProductController : ApiController
    {
        OldProductRepository oldProductRepository = new OldProductRepository();

        [Route("{id}", Name = "GetOldProductById")]
        public IHttpActionResult Get([FromUri] int id)
        {
            var oldProduct = oldProductRepository.Get(id);

            if (oldProduct == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                var insertUrl = mainUrl.Remove(mainUrl.Length - 2, 2);
                oldProduct.Links.Add(new Link() { Url = mainUrl, Method = "GET", Relation = "Get an existing Old Product" });
                oldProduct.Links.Add(new Link() { Url = mainUrl, Method = "PUT", Relation = "Update an existing Old Product" });
                oldProduct.Links.Add(new Link() { Url = mainUrl, Method = "DELETE", Relation = "Delete an existing Old Product" });
                return Ok(oldProduct);
            }
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var oldProducts = oldProductRepository.GetAll().ToList();

            if (oldProducts.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                foreach (var oldProduct in oldProducts)
                {
                    oldProduct.Links.Add(new Link() { Url = mainUrl + "/" + oldProduct.Id, Method = "GET", Relation = "Get an existing Old Product" });
                    oldProduct.Links.Add(new Link() { Url = mainUrl + "/" + oldProduct.Id, Method = "PUT", Relation = "Update an existing Old Product" });
                    oldProduct.Links.Add(new Link() { Url = mainUrl + "/" + oldProduct.Id, Method = "DELETE", Relation = "Delete an existing Old Product" });
                }
                return Ok(oldProducts);
            }
        }

        [Route("")]
        public IHttpActionResult Post(Models.OldProduct oldProduct)
        {
            oldProductRepository.Insert(oldProduct);
            string url = Url.Link("GetOldProductById", new { id = oldProduct.Id });
            return Created(url, oldProduct);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromBody] Models.OldProduct oldProduct, [FromUri] int id)
        {
            var updatedOldProduct = oldProductRepository.Get(id);
            if (updatedOldProduct == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                updatedOldProduct.CustomerId = oldProduct.CustomerId;
                updatedOldProduct.ProductName = oldProduct.ProductName;
                updatedOldProduct.ProductDescription = oldProduct.ProductDescription;
                updatedOldProduct.Status = oldProduct.Status;
                updatedOldProduct.BuyingPrice = oldProduct.BuyingPrice;
                updatedOldProduct.SellingPrice = oldProduct.SellingPrice;
                updatedOldProduct.Category = oldProduct.Category;
                updatedOldProduct.Brand = oldProduct.Brand;
                updatedOldProduct.Features = oldProduct.Features;
                updatedOldProduct.Quantity = oldProduct.Quantity;
                updatedOldProduct.Images = oldProduct.Images;
                updatedOldProduct.Discount = oldProduct.Discount;
                updatedOldProduct.Tax = oldProduct.Tax;
                oldProductRepository.Update(updatedOldProduct);
                return Ok(updatedOldProduct);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var oldProduct = oldProductRepository.Get(id);

            if (oldProduct == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                oldProductRepository.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
