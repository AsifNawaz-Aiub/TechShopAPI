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
    [RoutePrefix("api/admins")]
    public class AdminListController : ApiController
    {
        AdminRepository adminRepository = new AdminRepository();
        CredentialRepository credentialRepository = new CredentialRepository();

        public CredentialRepository CredentialRepository { get => credentialRepository; set => credentialRepository = value; }

        //[Route(""),BasicAuthentication]
        [Route("")]
        public IHttpActionResult GetActive()
        {
            return Ok(adminRepository.GetActive());
        }

        [Route("name")]
        public IHttpActionResult GetByName(string name)
        {
            return Ok(adminRepository.GetByName(name));
        }

        [Route("restricted")]
        public IHttpActionResult GetRestricted()
        {
            return Ok(adminRepository.GetRestricted());
        }

        [Route("{id}", Name = "GetByAdminId")]
        public IHttpActionResult Get(int id)
        {
            Models.Admin admin = adminRepository.Get(id);
            if (admin == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            admin.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "POST", Relation = "Create new Admin" });
            admin.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "PUT", Relation = "Modify Admin" });
            return Ok(admin);
        }

        [Route("")]
        public IHttpActionResult Post(Models.Admin admin)
        {
            admin.ProfilePic = "default.jpg";
            admin.Status = 1;
            admin.JoiningDate = DateTime.Now;
            admin.LastUpdated = DateTime.Now;
            adminRepository.Insert(admin);

            Credential cred = new Credential();
            cred.Email = admin.Email;
            cred.UserName = admin.UserName;
            cred.Password = admin.UserName + "1458";
            cred.Status = 1;
            cred.Type = 1;
            CredentialRepository.Insert(cred);

            return Created("api/admins/" + admin.Id, admin);
        }

        [Route("BlockAdmin/{id}")]
        public IHttpActionResult PutBlockAdmin(int id)
        {
            var admin = adminRepository.Get(id);
            adminRepository.Restrict(admin.Email);

            CredentialRepository.Restrict(admin.Email);
            return Ok();
        }
    }
}
