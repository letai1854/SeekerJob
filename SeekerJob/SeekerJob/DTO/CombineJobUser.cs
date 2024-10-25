using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeekerJob.Models;
namespace SeekerJob.DTO
{
    public class CombineJobUser
    {
        public InforEmployer employer { get;set;}
        public  Job job {get;set;}
    }
}