using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOders();

        Order GetOrder(int id);

        void CreateOrder(Order order);

        void UpdateOrder(int id, Order order);

        void DeleteOrder(int id);
    }
}
