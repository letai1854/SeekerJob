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

        public Job GetInfoJob(int id)
        {
            return myDb.Jobs.Where(m => m.id == id).FirstOrDefault();
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

        public bool DeleteTablebannerpart(int id)
        {

            var itemNew = myDb.tablebannerparts.FirstOrDefault(n => n.id == id);
            if (itemNew != null)
            {
                myDb.tablebannerparts.Remove(itemNew);
                return true;
            }
            return false;
        }

        public bool deletesavejob(int id, string username)
        {
            var item = myDb.SaveJobs.FirstOrDefault(n => n.idjob==id && n.usernamecandidate == username);
            if (item != null)
            {
                myDb.SaveJobs.Remove(item);
                return true;
            }
            return false;
        }
        public bool deleteappliedjobcandidate(int id, string username)
        {
            var item = myDb.ListCandidates.FirstOrDefault(t=>t.usernamecandidate == username && t.idjob==id);
            if (item != null)
            {
                myDb.ListCandidates.Remove(item);
                return true;
            }
            return false;
        }
        public Login GetAccount(string username)
        {
            return myDb.Logins.Where(m=>m.username==username).FirstOrDefault();
        }
        public tablemenupart GetTablemenupart(int id)
        {
            return myDb.tablemenuparts.Where(m=> m.id==id).FirstOrDefault();
        }
        public tablemenu GetTablemenu(int id)
        {
            return myDb.tablemenus.Where(m => m.id == id).FirstOrDefault();
        }
        //public Login GetLogin(string user)
        //{
        //    return myDb.Logins.Where(t=>t.username==user).FirstOrDefault();
        //}
        public bool DeleteJobs(int id)
        {

            var itemJob = myDb.Jobs.FirstOrDefault(n => n.id == id);
            if (itemJob != null)
            {
                myDb.Jobs.Remove(itemJob);

                return true;
            }
            return false;
        }
        public bool DeleteSavedJob(int id)
        {

            var itemNew = myDb.SaveJobs.FirstOrDefault(n => n.idjob==id);
            if (itemNew != null)
            {
                myDb.SaveJobs.Remove(itemNew);
                return true;
            }
            return false;
        }


        public InforEmployer GetInfoCompany(string id)
        {
            return myDb.InforEmployers.Where(m => m.username == id).FirstOrDefault();
        }

        public InforCandidate GetInfoCandidte(string id)
        {
            return myDb.InforCandidates.Where(m => m.username == id).FirstOrDefault();
        }

        public Login GetLogin(string username)
        {
            return myDb.Logins.Where(t=>t.username== username).FirstOrDefault();
        }
        public InforCandidate GetInforCandidate(string email)
        {
            return myDb.InforCandidates.Where(t=>t.email ==email).FirstOrDefault();
        }
        public InforEmployer GetInforEmployer(string email)
        {
            return myDb.InforEmployers.Where(t => t.email == email).FirstOrDefault();
        }
        public void Save()
        {
            myDb.SaveChanges();
        }

    }
}