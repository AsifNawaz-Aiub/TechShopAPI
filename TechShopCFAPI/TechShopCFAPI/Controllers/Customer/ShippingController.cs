using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Repository;

namespace TechShopCFAPI.Controllers.Customer
{
    [RoutePrefix("Api/Shipping")]
    public class ShippingController : ApiController
    {
        protected ShippingRepository shippingRepository = new ShippingRepository();
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var shippingData=shippingRepository.Get(id);
            if (shippingData!=null)
            {
                return Ok(shippingData);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route("{id}"), HttpPut]
        public IHttpActionResult Edit([FromBody]Models.ShippingData shippingData, [FromUri]int id)
        {
            shippingData.Id= id;
            if (ModelState.IsValid)
            {
                shippingRepository.Update(shippingData);
                return Ok(shippingRepository);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }
    }
}
