using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeekerJob.Models;
namespace SeekerJob.DTO
{
    public class SaveJobViewModal
    {

   
            public Job Job { get; set; }
            public InforEmployer InforEmployer { get; set; }
            public SaveJob saveJob { get; set; }

    }
}
