using Core;
using Data;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy
{
    public class EnergyTc : Tc, ICalculater
    {
        private DatabaseContext db;

        public double CalculateCost(double distance, double weight, double size)
        {   
            using(db = new DatabaseContext())
            {
                Tc tc = db.Tcs.FirstOrDefault(t => t.Name == "Энергия");
                return distance * tc.CoefficientOfKilometer + size * tc.CoefficientOfSize + weight * tc.CoefficientOfKilogram;
            }
        }
    }
}
