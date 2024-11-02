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
        public void Save()
        {
            myDb.SaveChanges();
        }
    }
}