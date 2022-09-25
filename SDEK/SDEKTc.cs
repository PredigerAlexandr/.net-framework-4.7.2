using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDEK
{
    internal class SDEKTc: Tc, ICalculater
    {
        private static DatabaseContext db = new DatabaseContext();
        private static Tc tc = db.TransportCompanies.FirstOrDefault(t => t.Name == "ПЭК");
        public double CalculateCost(Order order)
        {
            return order.Distance * tc.CoefficientOfKilometer + order.Size * tc.CoefficientOfSize + order.Weight * tc.CoefficientOfKilogram;
        }
    }
}
