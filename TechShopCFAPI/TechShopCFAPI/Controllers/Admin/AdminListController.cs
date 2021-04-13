using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Controllers.Admin
{
    public class AdminListController : ApiController
    { 
        TechShopDbContext context = new TechShopDbContext();

        [Route("api/admins")]
        public IHttpActionResult Get()
        {
            return Ok(context.Admins.ToList());
        }
    }
}
