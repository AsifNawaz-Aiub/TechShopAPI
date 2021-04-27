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
    [RoutePrefix("api/purchase_log")]
    public class PurchaseLogController : ApiController
    {
        PruchaseLogRepository pruchaseLogRepository = new PruchaseLogRepository();

        [Route("{id}", Name ="GetPurchaseLogById")]
        public IHttpActionResult Get([FromUri] int id)
        {
            var purchaseLog = pruchaseLogRepository.Get(id);

            if (purchaseLog == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                var insertUrl = mainUrl.Remove(mainUrl.Length - 2, 2);
                purchaseLog.Links.Add(new Link() { Url = mainUrl, Method = "GET", Relation = "Get an existing Purchase Log" });
                purchaseLog.Links.Add(new Link() { Url = mainUrl, Method = "PUT", Relation = "Update an existing Purchase Log" });
                purchaseLog.Links.Add(new Link() { Url = mainUrl, Method = "DELETE", Relation = "Delete an existing Purchase Log" });
                return Ok(purchaseLog);
            }
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var purchaseLogs = pruchaseLogRepository.GetAll().ToList();

            if (purchaseLogs.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                foreach (var purchaseLog in purchaseLogs)
                {
                    purchaseLog.Links.Add(new Link() { Url = mainUrl + "/" + purchaseLog.Id, Method = "GET", Relation = "Get an existing Purchase Log" });
                    purchaseLog.Links.Add(new Link() { Url = mainUrl + "/" + purchaseLog.Id, Method = "PUT", Relation = "Update an existing Purchase Log" });
                    purchaseLog.Links.Add(new Link() { Url = mainUrl + "/" + purchaseLog.Id, Method = "DELETE", Relation = "Delete an existing Purchase Log" });
                }
                return Ok(purchaseLogs);
            }
        }

        [Route("")]
        public IHttpActionResult Post(Models.PurchaseLog purchaseLog)
        {
            pruchaseLogRepository.Insert(purchaseLog);
            string url = Url.Link("GetPurchaseLogById", new { id = purchaseLog.Id });
            return Created(url, purchaseLog);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromBody] Models.PurchaseLog purchaseLog, [FromUri] int id)
        {
            var updatedPurchaseLog = pruchaseLogRepository.Get(id);
            if (updatedPurchaseLog == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                updatedPurchaseLog.CustomerId = purchaseLog.CustomerId;
                updatedPurchaseLog.ProductName = purchaseLog.ProductName;
                updatedPurchaseLog.ProductDescription = purchaseLog.ProductDescription;
                updatedPurchaseLog.Status = purchaseLog.Status;
                updatedPurchaseLog.BuyingPrice = purchaseLog.BuyingPrice;
                updatedPurchaseLog.Category = purchaseLog.Category;
                updatedPurchaseLog.Brand = purchaseLog.Brand;
                updatedPurchaseLog.Features = purchaseLog.Features;
                updatedPurchaseLog.Quantity = purchaseLog.Quantity;
                updatedPurchaseLog.Images = purchaseLog.Images;
                updatedPurchaseLog.PurchasedDate = purchaseLog.PurchasedDate;
                pruchaseLogRepository.Update(updatedPurchaseLog);
                return Ok(updatedPurchaseLog);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var purchaseLog = pruchaseLogRepository.Get(id);

            if (purchaseLog == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                pruchaseLogRepository.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
