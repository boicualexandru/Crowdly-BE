using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Models.User
{
    public class UploadAvatarModel
    {
        public IFormFile FormFile { get; set; }
    }
}
