using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repository
{
    public class BuyingAgentRepository : Repository<BuyingAgent>
    {
        TechShopDbContext context = new TechShopDbContext();

        public bool BuyingAgnetLoginValidation(string email, string password)
        {
            return context.BuyingAgents.Any(x => x.Email.Equals(email) && x.Password.Equals(password));   
        }

        public BuyingAgent GetBuyingAgentByEmail(string email)
        {
            return GetAll().Where(x => x.Email.Equals(email)).FirstOrDefault();
        }
    }
}