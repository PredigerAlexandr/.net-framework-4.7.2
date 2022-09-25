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
        private DatabaseContext db = new DatabaseContext();
        public TcRepository()
        {
            db = new DatabaseContext();
        }
        public void CreateTc(Tc tc)
        {
            db.Tcs.Add(tc);
            db.SaveChanges();
        }

        public void DeleteTc(int id)
        {
            Tc tc = db.Tcs.Find(id);
            if (tc != null)
                db.Tcs.Remove(tc);
            db.SaveChanges();
        }

        public Tc GetTc(int id)
        {
            Tc tc = db.Tcs.FirstOrDefault(t => t.Id == id);
            return tc;
        }

        public List<Tc> GetTcs()
        {
            List<Tc> tcs = db.Tcs.ToList();
            return tcs;

        }

        public void UpdateTc(int id, Tc tc)
        {
            Tc ChangeTc = db.Tcs.FirstOrDefault(t => t.Id == id);
            ChangeTc = tc;
            db.SaveChanges();
        }
    }
}
