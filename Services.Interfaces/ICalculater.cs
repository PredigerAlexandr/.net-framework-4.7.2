using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICalculater
    {
        double CalculateCost(double distance, double weight, double size);
    }
}
