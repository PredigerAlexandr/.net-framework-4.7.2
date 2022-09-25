using Core;
using Energy;
using Logic;
using PEK;
using SDEK;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestTask1_.net_framework_4._7._2_.Models.ViewModels;
using Data;

namespace TestTask1_.net_framework_4._7._2_.Controllers
{
    public class TransportCompanyController : Controller
    {
        OrderRepository _repoOrder = new OrderRepository();
        TcRepository _repoTc = new TcRepository();
        DatabaseContext db = new DatabaseContext();
        public ActionResult CreateOrder()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateOrder(ViewModelOrder order)
        {
            List<double> FirstCoords = await Coords.GetCoords(order.FirstPlace);
            List<double> LastCoords = await Coords.GetCoords(order.LastPlace);
            if (FirstCoords.Count == 0)
            {
                ModelState.AddModelError(nameof(order.FirstPlace), "Не можем найти пункт назначения");
            }
            if (LastCoords.Count == 0)
            {
                ModelState.AddModelError(nameof(order.LastPlace), "Не можем найти пункт назначения");
            }
            var query_params = new Distance(FirstCoords.Concat(LastCoords).ToList());
            double distance = Distance.GetDistance(query_params);
            if (distance == 0)
            {
                ModelState.AddModelError(nameof(order.LastPlace), "Нет дорог между населёнными пунктами");
            }

            List<Order> orders = new List<Order>();
            if (ModelState.IsValid)
            {
                var companies = _repoTc.GetTcs();
                foreach (var company in companies)
                {
                    double price = 0;
                    switch (company.Name)
                    {
                        case ("СДЭК"):
                            SDEKTc sdekTc = new SDEKTc();
                            price = sdekTc.CalculateCost(distance, order.Weight, order.Size);
                            break;
                        case ("ПЭК"):
                            PEKTc pekTc = new PEKTc();
                            price = pekTc.CalculateCost(distance, order.Weight, order.Size);
                            break;
                        case ("Энергия"):
                            EnergyTc energyTc = new EnergyTc();
                            price = energyTc.CalculateCost(distance, order.Weight, order.Size);
                            break;
                    }

                    orders.Add(new Order()
                    {
                        FirstName = order.FirstName,
                        SurName = order.SurName,
                        Phone = order.Phone,
                        FirstPlace = order.FirstPlace,
                        LastPlace = order.LastPlace,
                        Weight = order.Weight,
                        Size = order.Size,
                        Date = DateTime.Now,
                        Price = price,
                        Distance = Convert.ToInt32(distance),
                        Tc = company
                    });
                }
                ViewBag.Orders = orders;
                return View(order);
            }
            
                return View(order);
        }

        [HttpPost]
        public ActionResult ShowTc(string FirstName, string SurName, string Phone, string FirstPlace, string LastPlace, string Weight, string Size, string Distance, string Company, string Price, string tcName = "")
        {
            if (tcName == "")
            {
                Order order = new Order()
                {
                    FirstName = FirstName,
                    SurName = SurName,
                    Phone = Phone,
                    FirstPlace = FirstPlace,
                    LastPlace = LastPlace,
                    Weight = Convert.ToDouble(Weight),
                    Size = Convert.ToDouble(Size),
                    Date = DateTime.Now,
                    Distance = Convert.ToInt32(Distance),
                    Price = Convert.ToDouble(Price),
                    Tc = db.Transport_companies.FirstOrDefault(p => p.Id == Convert.ToInt32(Company))
                };
                db.Orders.Add(order);
                db.SaveChanges();

                return Redirect("AllCalculator");
            }
            else
            {
                db.Orders.ToList();
                var Tc = db.Transport_companies.FirstOrDefault(t => t.Name == tcName);
                IOrderedQueryable orders = db.Orders.Where(o => o.Tc.Id == Tc.Id).OrderBy(o => o.Date);
                ViewBag.Orders = orders;
                return View(Tc);
            }
            return View();
        }
    }
}