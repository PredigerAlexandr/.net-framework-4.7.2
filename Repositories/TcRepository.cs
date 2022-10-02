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

        public void CreateTc(Tc tc)
        {
            using (db = new DatabaseContext())
            {
                db.Tcs.Add(tc);
                db.SaveChanges();
            }
        }

        public void DeleteTc(int id)
        {
            using (db = new DatabaseContext())
            {
                Tc tc = db.Tcs.Find(id);
                if (tc != null)
                    db.Tcs.Remove(tc);
                db.SaveChanges();
            }
        }

        public Tc GetTc(int id)
        {
            using (db = new DatabaseContext())
            {
                Tc tc = db.Tcs.FirstOrDefault(t => t.Id == id);
                return tc;
            }
        }

        public List<Tc> GetTcs()
        {
            using (db = new DatabaseContext())
            {
                List<Tc> tcs = db.Tcs.ToList();
                return tcs;
            }

        }

        public void UpdateTc(int id, Tc tc)
        {
            using (db = new DatabaseContext())
            {
                Tc ChangeTc = db.Tcs.FirstOrDefault(t => t.Id == id);
                ChangeTc = tc;
                db.SaveChanges();
            }
        }
    }
}
