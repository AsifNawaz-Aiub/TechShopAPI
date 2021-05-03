using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repositories.AdminModule;

namespace TechShopCFAPI.Controllers.Admin
{
    [RoutePrefix("api/customers")]
    public class CustomerListController : ApiController
    {
        CustomerRepository custRepo = new CustomerRepository();
        CredentialRepository credentialRepository = new CredentialRepository();

        [Route("active")]
        public IHttpActionResult Get()
        {
            return Ok(custRepo.GetActiveCustomers());
        }

        [Route("active/name")]
        public IHttpActionResult GetActiveByName(string name)
        {
            return Ok(custRepo.GetActiveByName(name));
        }

        [Route("restricted/name")]
        public IHttpActionResult GetRestrictedByName(string name)
        {
            return Ok(custRepo.GetRestrictedByName(name));
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Customer cust = custRepo.Get(id);
            if (cust == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(cust);
        }

        [Route("Block/{id}")]
        public IHttpActionResult PutBlockCustomer(int customerId)
        {
            var cust = custRepo.Get(customerId);
            custRepo.BlockCustomer(cust.Email);

            credentialRepository.Restrict(cust.Email);
            return Ok();

        }

        [Route("restricted")]
        public IHttpActionResult GetRestricted()
        {
            return Ok(custRepo.GetRestrictedCustomers());
        }

        [Route("history/{id}")]
        public IHttpActionResult GetHistory(int id)
        {
            return Ok(custRepo.History(id));
        }


        [Route("ReactiveCustomer/{id}")]
        public IHttpActionResult PutReactivateCustomer(int customerId)
        {
            var cust = custRepo.GetCust(customerId);
            custRepo.ReactivateCustomer(cust.Email);
            credentialRepository.Reactive(cust.Email);
            return Ok();
        }

        [Route("Review/{id}")]
        public IHttpActionResult GetReview(int id)
        {
            return Ok(custRepo.CustomerReview(id));
        }
    }
}