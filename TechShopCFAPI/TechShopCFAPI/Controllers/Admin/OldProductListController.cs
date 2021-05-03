using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Repositories.AdminModule;

namespace TechShopCFAPI.Controllers.Admin
{
    [RoutePrefix("api/oldproducts")]
    public class OldProductListController : ApiController
    {

        OldProductRepository oldProductRepository = new OldProductRepository();

        [Route("")]
        public IHttpActionResult Get(string category)
        {
            if (category == "All")
            {
                return Ok(oldProductRepository.GetInStock());
            }
            else
                return Ok(oldProductRepository.GetInStockByCategory(category));
        }

        [Route("a")]
        public IHttpActionResult Gete()
        {

            return Ok(oldProductRepository.GetInStock());

        }
    }
}
