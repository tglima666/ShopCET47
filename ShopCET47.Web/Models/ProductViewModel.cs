
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ShopCET47.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.Web.Models
{
    public class ProductViewModel : Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}