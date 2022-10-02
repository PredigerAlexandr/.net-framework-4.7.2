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
    public class OrderRepository:IOrderRepository
    {
        private DatabaseContext db;


        public void CreateOrder(Order order)
        {
            using(db=new DatabaseContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public void DeleteOrder(int id)
        {
            using(db = new DatabaseContext())
            {
                Order order = db.Orders.FirstOrDefault(o => o.Id == id);
                db.Orders.Remove(order);
                db.SaveChanges();
            }
        }

        public List<Order> GetOders()
        {
            using (db = new DatabaseContext())
            {
                List<Order> orders = db.Orders.ToList();
                return orders;
            }
        }

        public Order GetOrder(int id)
        {
            using (db = new DatabaseContext())
            {
                Order order = db.Orders.FirstOrDefault(o => o.Id == id);
                return order;
            }
        }

        public void UpdateOrder(int id, Order order)
        {
            using (db = new DatabaseContext())
            {
                Order ChangeOrder = db.Orders.FirstOrDefault(o => o.Id == id);
                ChangeOrder = order;
                db.SaveChanges();
            }
        }

    }
}
