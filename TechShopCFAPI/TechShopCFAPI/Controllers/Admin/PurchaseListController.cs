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
    public class PurchaseListController : ApiController
    {
        TechShopDbContext context = new TechShopDbContext();
        PurchaseRepository purRepo = new PurchaseRepository();

        [Route("api/purchaselogs")]
        public IHttpActionResult Get(string startDate, string endDate)
        {
            if (startDate == null && endDate == null)
            {
                return Ok(purRepo.GetAll());
            }
            else if (startDate != null && endDate != null)
            {
                return Ok(purRepo.Get(startDate, endDate));
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }
    }
}
