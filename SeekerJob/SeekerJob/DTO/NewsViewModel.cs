using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace SeekerJob.DTO
{
    public class NewsViewModel
    {
        public IPagedList<NewsWrapper> PagedList { get; set; }
    }
}