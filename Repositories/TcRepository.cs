using Core;
using Data;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TcRepository:ITcRepository
    {
        private DatabaseContext db;
        public TcRepository()
        {
            db = new DatabaseContext();
        }
        public void CreateTc(Tc tc)
        {
            db.TransportCompanies.Add(tc);
            db.SaveChanges();
        }

        public void DeleteTc(int id)
        {
            Tc tc = db.TransportCompanies.Find(id);
            if (tc != null)
                db.TransportCompanies.Remove(tc);
            db.SaveChanges();
        }

        public Tc GetTc(int id)
        {
            Tc tc = db.TransportCompanies.FirstOrDefault(t => t.Id == id);
            return tc;
        }

        public List<Tc> GetTcs()
        {
            List<Tc> tcs = db.TransportCompanies.ToList();
            return tcs;

        }

        public void UpdateTc(int id, Tc tc)
        {
            Tc ChangeTc = db.TransportCompanies.FirstOrDefault(t => t.Id == id);
            ChangeTc = tc;
            db.SaveChanges();
        }
    }
}
