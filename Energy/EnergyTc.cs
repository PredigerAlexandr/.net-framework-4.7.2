using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy
{
    public class EnergyTc:Tc, ICalculater
    {
        private static DatabaseContext db = new DatabaseContext();
        private static Tc tc = db.TransportCompanies.FirstOrDefault(t => t.Name == "Энергия");
        public double CalculateCost(Order order)
        {
            return order.Distance * tc.CoefficientOfKilometer + order.Size * tc.CoefficientOfSize + order.Weight * tc.CoefficientOfKilogram;
        }
    }
}
