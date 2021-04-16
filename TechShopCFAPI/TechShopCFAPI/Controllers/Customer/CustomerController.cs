using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Authentications;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;

namespace TechShopCFAPI.Controllers.Customer
{
    [RoutePrefix("Api/Customers")]
    public class CustomerController : ApiController
    {
        CustomerRepository customerRepository = new CustomerRepository();
        ShippingRepository shippingRepository = new ShippingRepository();
        [Route("")]
        public IHttpActionResult Get()
        {
            var activeCustomerList = customerRepository.GetAll().Where(x=>x.Status=="Active").ToList();
            if (activeCustomerList!=null && activeCustomerList.Count!=0)
            {
                foreach (var customer in activeCustomerList)
                {
                    var url = HttpContext.Current.Request.Url.AbsoluteUri;
                    customer.Links.Add( new Link() { Url=url , Method="POST", Relation="Create a new customer."});
                    customer.Links.Add(new Link() { Url =url+"/"+customer.CustomerId , Method = "GET", Relation = "Get an existing specific customer." });
                    customer.Links.Add(new Link() { Url = url + "/" + customer.CustomerId, Method = "PUT", Relation = "Edit an existing specific customer." });
                    customer.Links.Add(new Link() { Url = url + "/" + customer.CustomerId, Method = "DELETE", Relation = "Deletes an existing specific customer." });
                }
                return Ok(activeCustomerList);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
        [Route("{id}", Name ="GetById"), LoginAuthentication]
        public IHttpActionResult Get(int id)
        {
            var customer = customerRepository.Get(id);
            if (customer!=null)
            {
                var url = HttpContext.Current.Request.Url.AbsoluteUri;
                customer.Links.Add(new Link() { Url = url.Remove(url.Length-2,2), Method = "POST", Relation = "Create a new customer." });
                customer.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an existing specific customer." });
                customer.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit an existing specific customer." });
                customer.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Deletes an existing specific customer." });
                return Ok(customer);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route("")]
        public IHttpActionResult Post(Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Insert(customer);

                Models.ShippingData shippingData = new ShippingData();
                shippingData.CustomerId = customer.CustomerId;
                shippingData.CardNumber = null;
                shippingData.CardType = null;
                shippingData.ExpirationMonth = null;
                shippingData.ExpirationYear = null;
                shippingData.ShippingAddress = null;
                shippingData.ShippingMethod = null;
                shippingRepository.Insert(shippingData);

                string url = Url.Link("GetById", new { id = customer.CustomerId });
                return Created(url, customer);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        [Route("{id}"), HttpPut]
        public IHttpActionResult Edit([FromBody]Models.Customer customer, [FromUri]int id)
        {
            customer.CustomerId = id;
            if (ModelState.IsValid)
            {
                customerRepository.Update(customer);
                return Ok(customer);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
            //customerRepository.Update(customer);
            //return Ok(customer);
        }
        [Route("{id}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            customerRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("GetCustomerLoginValidation"), HttpGet, LoginAuthentication]
        public IHttpActionResult GetCustomerLoginValidation()
        {
            var customer = customerRepository.GetCustomerByEmailOrUserName(Thread.CurrentPrincipal.Identity.Name);
            if (customer!=null)
            {
                return Ok(customer);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route("GetUserNameValidation/{user}", Name = "ValidateByUserName")]
        public IHttpActionResult GetUserNameValidation(string user)
        {
            var customer = customerRepository.GetCustomerByEmailOrUserName(user);
            if (customer!=null)
            {
                return StatusCode(HttpStatusCode.Conflict);
            }
            else
            {
                return StatusCode(HttpStatusCode.OK);
            }
        }
        [Route("{customerId}/shipping")]
        public IHttpActionResult GetShippingDataByCustomerId(int customerId)
        {
            var shippingData = shippingRepository.GetShippingDataByCustomerId(customerId);
            if (shippingData!=null)
            {
                return Ok(shippingData);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
    }
}
