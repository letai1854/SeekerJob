using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace SeekerJob.Controllers
{
    public class ViewListNewsController : Controller
    {
        MYDB db = new MYDB();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewListNew(int? page, int? pageSize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 8;
            }
            var newslike = (from n in db.News
                            join ln in db.likenews on n.id equals ln.idnews
                            group n by new
                            {
                                n.id,
                                n.username,
                                n.title,
                                n.image,
                                n.meta,
                                n.daypost,
                                n.shortbref,
                                n.description
                            } into grouped
                            select new ComeNewsLikeNews
                            {
                                Id = grouped.Key.id,
                                Username = grouped.Key.username,
                                Title = grouped.Key.title,
                                Image = grouped.Key.image,
                                Meta = grouped.Key.meta,
                                DayPost = grouped.Key.daypost,
                                ShortBref = grouped.Key.shortbref,
                                Description = grouped.Key.description,
                                LikeCount = grouped.Count()
                            }).OrderBy(t => t.LikeCount).Take(5).ToList();



            var newsall = db.News.OrderBy(t => t.daypost).ToList();
            var infoemployeelist = new List<NewsEmployee>();
            var infocandidatelist = new List<NewsCandidate>();


            foreach (var item in newsall)
            {
                var inforEmployer = db.InforEmployers
                                      .Where(t => t.username == item.username)
                                      .FirstOrDefault();

                if (inforEmployer != null)
                {
                    infoemployeelist.Add(new NewsEmployee
                    {
                        inforEmployer = inforEmployer,
                        newsinfo = item
                    });
                }

                var inforcandidate = db.InforCandidates
                                       .Where(t => t.username == item.username)
                                       .FirstOrDefault();

                if (inforcandidate != null)
                {
                    infocandidatelist.Add(new NewsCandidate
                    {
                        inforCandidate = inforcandidate,
                        newsinfo = item
                    });
                }
            }
            var combinedList = new List<NewsWrapper>();

            foreach (var candidate in infocandidatelist)
            {
                combinedList.Add(new NewsWrapper { NewsCandidate = candidate });
            }

            foreach (var employer in infoemployeelist)
            {
                combinedList.Add(new NewsWrapper { NewsEmployee = employer });
            }


            // Return as IPagedList<NewsWrapper>
            IPagedList<NewsWrapper> pagedList = combinedList.ToPagedList((int)page, (int)pageSize);
            ViewBag.metacontroller = "Chi-tiet-tin-tuc";
            ViewBag.infocandidatelist = infocandidatelist;
            ViewBag.infoemployeelist = infoemployeelist;
            ViewBag.newslike = newslike;
            ViewBag.pagedList = pagedList;  // <-- Pass the paged list directly

            return View();
        }
    }
}