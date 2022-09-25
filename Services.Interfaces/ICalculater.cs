using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    internal interface ICalculater
    {
        double CalculateCost(Order order);
    }
}
