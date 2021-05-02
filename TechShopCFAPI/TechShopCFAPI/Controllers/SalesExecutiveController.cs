using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Models.Sales.DataModels;
using TechShopCFAPI.Models.Sales.ViewModels;

namespace TechShopCFAPI.Controllers
{
    [RoutePrefix("api/SalesExecutive")]
    public class SalesExecutiveController : ApiController
    {
        List<CartViewModel> salesCart = new List<CartViewModel>();
        TechShopDbContext context = new TechShopDbContext();
        ProductsDataModel productsData = new ProductsDataModel();
        CartDataModel cartData = new CartDataModel();
        System.Web.HttpCookie sc = new HttpCookie("sc1");

        [Route("Profile", Name = "SalesExecutiveProfile")]
        public IHttpActionResult Get()
        {
           
            return Ok(context.SalesExecutives);
        }
        [Route("Products", Name = "SalesExecutiveProducts")]
        public IHttpActionResult GetProducts()
        {
            return Ok(context.Products);
        }
        [Route("AvailableProducts", Name = "SalesExecutiveAvailableProducts")]
        public IHttpActionResult GetAvailableProducts()
        {
            return Ok(productsData.GetAvailableProducts());
        }
        [Route("UpcomingProducts", Name = "SalesExecutiveUpcomingProducts")]
        public IHttpActionResult GetUpcomingProducts()
        {
            return Ok(productsData.GetUpComingProducts());
        }
        [Route("DiscountProducts", Name = "SalesExecutiveDiscountProducts")]
        public IHttpActionResult GetDiscountProducts()
        {
            return Ok(productsData.GetAllDiscountProducts());
        }

       [Route("AddProductToCart/{id}/{q}", Name = "cart"), HttpPost]
        public IHttpActionResult AddProductToCart([FromUri] int id, int q)
        {

          Models.Product  data = context.Products.Find(id);
            Models.Cart cart = new Cart();

            cart.ProductId = id;
            cart.ProductName = data.ProductName;
            cart.Quantity = q;
            cart.Category = data.Category;
            cart.TotalPrice = (int)data.SellingPrice * q;
            context.Carts.Add(cart);
            context.SaveChanges();

            return Ok(new { msg = "Product Added to the Cart!" });
            
        }

        [Route("Cart", Name = "cartview")]
        public IHttpActionResult GetCart()
        {
            return Ok(context.Carts);
        }

        [Route("sell", Name = "sells")]
        public IHttpActionResult GetSell()
        {
            return Ok(context.Carts);
        }

        [Route("Sold"), HttpPost]
        public IHttpActionResult Solddd()
        {
            //string cname, string address, string phone
            ProductsDataModel data = new ProductsDataModel();
            Models.Product SellingProduct = new Product();


            var CurrentQuantity = 0;
            var UpdatedQuantity = 0;
            var id = 0;
            foreach (var c in context.Carts)
            {

                id = c.ProductId;

                SellingProduct = data.GetProductById(id);

                CurrentQuantity = SellingProduct.Quantity;
                UpdatedQuantity = CurrentQuantity - c.Quantity;
                if (UpdatedQuantity == 0) { SellingProduct.Status = "Stock Out"; }
                SellingProduct.Quantity = UpdatedQuantity;
                data.UpdateProduct(SellingProduct);
                var sl = new Sales_Log()
                {
                    UserId = 2,
                    CustomerName = "adsas",
                    CustomerAddress= "adsas",
                    CustomerPhoneNo = "adsas",
                    ProductId = c.ProductId,
                    SalesExecutiveId = 1,
                    DateSold = DateTime.Today,
                    Quantity = c.Quantity,
                    Discount = SellingProduct.Discount,
                    Tax = SellingProduct.Tax,
                    TotalPrice = (decimal)c.TotalPrice,
                    Status = "Sold",
                    Profits = ((SellingProduct.SellingPrice) * c.Quantity) - ((SellingProduct.BuyingPrice) * c.Quantity)
                };

                data.insertSales(sl);
            }
            return Ok();
        }




    }
}
