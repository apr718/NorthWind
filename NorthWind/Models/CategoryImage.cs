using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NorthWind.Models
{
    public class CategoryImage
    {
        public IFormFile AvatarImage { get; set; }
    }
}
