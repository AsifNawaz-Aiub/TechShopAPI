using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;
using TechShopCFAPI.BuyingAgentAuthorizations;
using System.Threading;
using System.Web;

namespace TechShopCFAPI.Controllers.BuyingAgent
{
    [RoutePrefix("api/buying_agent")]
    public class BuyingAgentController : ApiController
    {
        BuyingAgentRepository buyingAgentRepository = new BuyingAgentRepository();

        [Route("{id}", Name = "GetBuyingAgentById")]
        public IHttpActionResult Get([FromUri] int id)
        {
            var buyingAgnet = buyingAgentRepository.Get(id);

            if (buyingAgnet == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                var insertUrl = mainUrl.Remove(mainUrl.Length - 2, 2);
                buyingAgnet.Links.Add(new Link() { Url = mainUrl, Method = "GET", Relation = "Get an existing Buying Agent" });
                buyingAgnet.Links.Add(new Link() { Url = insertUrl, Method = "POST", Relation = "Create a new Buying Agent" });
                buyingAgnet.Links.Add(new Link() { Url = mainUrl, Method = "PUT", Relation = "Update an existing Buying Agent" });
                buyingAgnet.Links.Add(new Link() { Url = mainUrl, Method = "DELETE", Relation = "Delete an existing Buying Agent" });
                return Ok(buyingAgnet);
            }
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var buyingAgnets = buyingAgentRepository.GetAll().ToList();

            if(buyingAgnets.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                foreach(var buyingAgent in buyingAgnets)
                {
                    buyingAgent.Links.Add(new Link() { Url = mainUrl + "/"+buyingAgent.Id, Method = "GET", Relation = "Get an existing Buying Agent" });
                    buyingAgent.Links.Add(new Link() { Url = mainUrl, Method = "POST", Relation = "Create a new Buying Agent" });
                    buyingAgent.Links.Add(new Link() { Url = mainUrl + "/" + buyingAgent.Id, Method = "PUT", Relation = "Update an existing Buying Agent" });
                    buyingAgent.Links.Add(new Link() { Url = mainUrl + "/" + buyingAgent.Id, Method = "DELETE", Relation = "Delete an existing Buying Agent" });
                }
                return Ok(buyingAgnets);
            }
        }

        [Route("")]
        public IHttpActionResult Post(Models.BuyingAgent buyingAgent)
        {
            buyingAgentRepository.Insert(buyingAgent);
            string url = Url.Link("GetBuyingAgentById", new { id = buyingAgent.Id });
            return Created(url, buyingAgent);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromBody] Models.BuyingAgent buyingAgent, [FromUri] int id)
        {
            var updatedBuyingAgent = buyingAgentRepository.Get(id);
            if(updatedBuyingAgent == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                updatedBuyingAgent.FullName = buyingAgent.FullName;
                updatedBuyingAgent.UserName = buyingAgent.UserName;
                updatedBuyingAgent.ProfilePic = buyingAgent.ProfilePic;
                updatedBuyingAgent.Password = buyingAgent.Password;
                updatedBuyingAgent.Email = buyingAgent.Email;
                updatedBuyingAgent.Phone = buyingAgent.Phone;
                updatedBuyingAgent.Salary = buyingAgent.Salary;
                updatedBuyingAgent.Address = buyingAgent.Address;
                updatedBuyingAgent.JoiningDate = buyingAgent.JoiningDate;
                updatedBuyingAgent.LastUpdated = buyingAgent.LastUpdated;
                buyingAgentRepository.Update(updatedBuyingAgent);
                return Ok(updatedBuyingAgent);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var buyingAgnet = buyingAgentRepository.Get(id);

            if (buyingAgnet == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                buyingAgentRepository.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("get_validate_buying_agent"), LoginBuyingAgentAuthorization]
        public IHttpActionResult GetvalidateBuyingAgent()
        {
            var buyingAgent = buyingAgentRepository.GetBuyingAgentByEmail(Thread.CurrentPrincipal.Identity.Name);

            if(buyingAgent == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(buyingAgent);
            }
        }
    }
}
