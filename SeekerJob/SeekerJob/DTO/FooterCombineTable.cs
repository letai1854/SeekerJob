using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeekerJob.Models;
namespace SeekerJob.DTO
{
    public class FooterCombineTable
    {

        public Dictionary<tablefooter,List<tablefooterpart>> footercombine {  get; set; }
    }
}