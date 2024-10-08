using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekerJob.DTO
{
    public class ComeNewsLikeNews
    {
        public long Id { get; set; }               // news.id
        public string Username { get; set; }        // news.username
        public string Title { get; set; }           // news.title
        public string Image { get; set; }           // news.image
        public string Meta { get; set; }            // news.meta
        public DateTime? DayPost { get; set; }       // news.daypost
        public string ShortBref { get; set; }       // news.shortbref
        public string Description { get; set; }     // news.description
        public int LikeCount { get; set; }
    }
}