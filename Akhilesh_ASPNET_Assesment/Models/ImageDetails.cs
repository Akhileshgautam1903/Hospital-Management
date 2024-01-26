using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akhilesh_ASPNET_Assesment.Models
{
    public class ImageDetails
    {
        public string Name { get; set; }
        public byte[] RetriveData { get; set; }
        public IFormFile Pic { get; set; }
    }
}
