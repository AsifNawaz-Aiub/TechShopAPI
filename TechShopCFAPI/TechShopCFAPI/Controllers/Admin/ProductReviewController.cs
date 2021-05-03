using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Repositories.AdminModule;

namespace TechShopCFAPI.Controllers.Admin
{
    public class ProductReviewController : ApiController
    {
        ReviewRepository reviewRepo = new ReviewRepository();

        [Route("api/reviews/product/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(reviewRepo.GetReview(id));
        }
    }
}