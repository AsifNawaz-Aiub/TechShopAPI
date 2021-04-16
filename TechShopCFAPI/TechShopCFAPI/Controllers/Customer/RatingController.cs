using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;

namespace TechShopCFAPI.Controllers.Customer
{
    [RoutePrefix("Api/Rating")]
    public class RatingController : ApiController
    {
        protected RatingRepository ratingRepository = new RatingRepository();
        [Route("{id}", Name ="GetByRatingId")]
        public IHttpActionResult Get(int id)
        {
            var rating = ratingRepository.Get(id);
            if (rating==null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                return Ok(rating);
            }
        }
        [Route("")]
        public IHttpActionResult Post(Rating rating)
        {
            var newRating = ratingRepository.GetAll().Where(x => x.CustomerId == rating.CustomerId && x.ProductId == rating.ProductId).FirstOrDefault();
            if (newRating==null)
            {
                ratingRepository.Insert(rating);
                string url = Url.Link("GetByRatingId", new { id = rating.RatingId });
                return Created(url, rating);
            }
            else
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
        }
    }
}
