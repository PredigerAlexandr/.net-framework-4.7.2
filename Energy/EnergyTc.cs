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
        private static DatabaseContext db = new DatabaseContext();
        private static Tc tc = db.Tcs.FirstOrDefault(t => t.Name == "Энергия");

        public double CalculateCost(double distance, double weight, double size)
        {
            return distance * tc.CoefficientOfKilometer + size * tc.CoefficientOfSize + weight * tc.CoefficientOfKilogram;
        }
    }
}
