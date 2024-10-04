using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekerJob
{
    public class EnumType
    {
        public enum Type
        {
            Danhsachvieclam,
            searchTitleJob,
            Diadiem,
            Loaicongviec,
            Ngaydang,
            Mucluong,
            titlepagePostJob,
            list,
            titlepageSavedJob,
            listCandidate,
        }
        public enum Banner
        {
            imgbanner,
            imgperson
        }
        public enum jobCategory
        {
            Fulltime,
            Internship,
            Parttime,
            Temporary,
            Volunteer,
            Freelance,

        }
        public enum TypeTitleHome
        {
            titlepagehome1
                , titlepagehome2
        }
        public enum Adertise
        {
            advertisehome1

        }
    }
}