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
            Mucluong
        }
        public enum Banner
        {
            imgbanner,
            imgperson, 
            
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