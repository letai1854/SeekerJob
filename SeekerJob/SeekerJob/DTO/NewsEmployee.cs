using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeekerJob.Models;
namespace SeekerJob.DTO
{
    public class NewsEmployee
    {
        public InforEmployer inforEmployer {  get; set; }
        public News newsinfo { get; set; }
    }
}