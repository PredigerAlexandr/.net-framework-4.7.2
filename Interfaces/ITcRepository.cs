using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITcRepository
    {
        List<Tc> GetTcs();

        Tc GetTc(int id);

        void CreateTc(Tc tc);

        void UpdateTc(int id, Tc tc);

        void DeleteTc(int id);
    }
}
