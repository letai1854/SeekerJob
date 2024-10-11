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
            titlepageManager,
            titlepagePostNews,
            listNews,
            titlepageManagerNews,
            titlepageProfileCompany,
            titlepageProfileCandidate,
            changepasswordcandidate,
            changepasswordcompany,
            adminnews,
            adminmange,
            adminpost,
            adminusers,
            adminadv,
            advproduct,
            advnews,
            advcompany
        }
        public enum Banner
        {
            imgbanner,
            imgperson, 
            
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
                , titlepagehomenews,
            titlepagehomecompany,
            headercompany
        }
        public enum Adertise
        {
            advertisehome1,
            advertisehome2

        }
        public enum footer
        {
            imgcompany,
            packgagesoftware,
            contactinfo,
            infofooter,
            copyright,
            connectdiff
        }
    }
}