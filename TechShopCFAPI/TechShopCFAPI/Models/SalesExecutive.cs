using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class SalesExecutive
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string ProfilePic { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(20)]
        public string Address { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public System.DateTime JoiningDate { get; set; }

        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}