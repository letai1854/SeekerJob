using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace SeekerJob.DTO
{
    public class JobApplicationViewModel
    {
        public string Title { get; set; }
        public double Offer { get; set; }
        public DateTime? StartDay { get; set; }
        public string JobCategory { get; set; }
        public string Address { get; set; }
        public int IdJob { get; set; }
        public int CandidateCount { get; set; }
        public string meta { get; set; }
    }

}