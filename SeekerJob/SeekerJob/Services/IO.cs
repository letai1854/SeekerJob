using SeekerJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekerJob.Services
{
    public class IO
    {
        testdbs2425Entities myDb = new testdbs2425Entities();
        public void AddObject<T>(T obj)
        {
            myDb.Set(obj.GetType()).Add(obj);
        }
        public News GetNews(int id)
        {
            return myDb.News.Where(m =>m.id==id).FirstOrDefault();
        }
        public bool DeleteNews(int id)
        {

            var itemNew = myDb.News.FirstOrDefault(n => n.id == id);
            if (itemNew != null)
            {
                myDb.News.Remove(itemNew);
                return true;
            }
            return false;
        }
        public void Save()
        {
            myDb.SaveChanges();
        }
    }
}