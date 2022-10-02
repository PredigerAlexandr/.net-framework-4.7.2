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
using System.Diagnostics;
using System.Data.Entity;
using System.IO;
using ClosedXML.Excel;

namespace TestTask1_.net_framework_4._7._2_.Controllers
{
    public class TransportCompanyController : Controller
    {
        OrderRepository _repoOrder = new OrderRepository();
        TcRepository _repoTc = new TcRepository();
        private DatabaseContext db;
        public ActionResult CreateOrder()
        {
            return View(new ViewModelOrders());
        }


        [HttpPost]
        public async Task<ActionResult> CreateOrder(ViewModelOrders model)
        {
            List<double> FirstCoords = await Coords.GetCoords(model.FirstPlace);
            List<double> LastCoords = await Coords.GetCoords(model.LastPlace);
            if (FirstCoords.Count == 0)
            {
                ModelState.AddModelError(nameof(model.FirstPlace), "Не можем найти пункт назначения");
            }
            if (LastCoords.Count == 0)
            {
                ModelState.AddModelError(nameof(model.LastPlace), "Не можем найти пункт назначения");
            }
            var query_params = new Distance(FirstCoords.Concat(LastCoords).ToList());
            double distance = Distance.GetDistance(query_params);
            if (distance == 0)
            {
                ModelState.AddModelError(nameof(model.LastPlace), "Нет дорог между населёнными пунктами");
            }

            if (ModelState.IsValid)
            {
                var companies = _repoTc.GetTcs();

                var orders = new List<ViewModelOrderItem>();
                foreach (var company in companies)
                {
                    double price = 0;
                    switch (company.Name)
                    {
                        case ("СДЭК"):
                            SDEKTc sdekTc = new SDEKTc();
                            price = sdekTc.CalculateCost(distance, model.Weight, model.Size);
                            break;
                        case ("ПЭК"):
                            PEKTc pekTc = new PEKTc();
                            price = pekTc.CalculateCost(distance, model.Weight, model.Size);
                            break;
                        case ("Энергия"):
                            EnergyTc energyTc = new EnergyTc();
                            price = energyTc.CalculateCost(distance, model.Weight, model.Size);
                            break;
                    }

                    orders.Add(new ViewModelOrderItem()
                    {
                        FirstName = model.FirstName,
                        SurName = model.SurName,
                        Phone = model.Phone,
                        FirstPlace = model.FirstPlace,
                        LastPlace = model.LastPlace,
                        Weight = model.Weight,
                        Size = model.Size,
                        Price = price,
                        Distance = Convert.ToInt32(distance),
                        TcId = company.Id,
                        TcName = company.Name,
                    });
                }

                model.Orders = orders;

                return View(model);

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddOrder(ViewModelOrderItem model)
        {
            var order = new Order()
            {
                FirstName = model.FirstName,
                SurName = model.SurName,
                Phone = model.Phone,
                FirstPlace = model.FirstPlace,
                LastPlace = model.LastPlace,
                Weight = model.Weight,
                Size = model.Size,
                Price = model.Price,
                Distance = model.Distance,
                TcId = model.TcId,
                Date = DateTime.Now
            };
            _repoOrder.CreateOrder(order);

            return RedirectToAction("ShowTc", new { model.TcId });
        }

        [HttpGet]
        public async Task<ActionResult> ShowTc(int tcId)
        {
            using (db = new DatabaseContext())
            {
                ViewModelTc viewModelTc = new ViewModelTc()
                {
                    Tc = _repoTc.GetTc(tcId),
                    Orders = db.Orders.Where(o => o.TcId == tcId).OrderBy(o => o.Date).ToList()
                };
                return View(viewModelTc);
            }
        }

        [HttpGet]
        public async Task<ActionResult> ExportExcel(int tcId)
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Заказы");

                worksheet.Cell(1, 1).Value = "Имя";
                worksheet.Cell(1, 2).Value = "Фамилия";
                worksheet.Cell(1, 3).Value = "Откуда";
                worksheet.Cell(1, 4).Value = "Куда";
                worksheet.Cell(1, 5).Value = "Вес";
                worksheet.Cell(1, 6).Value = "Размер";
                worksheet.Cell(1, 7).Value = "Расстояние";
                worksheet.Cell(1, 8).Value = "Цена";
                worksheet.Cell(1, 9).Value = "ТК";
                worksheet.Cell(1, 10).Value = "Дата";
                worksheet.Row(1).Style.Font.Bold = true;

                using(db = new DatabaseContext())
                {
                    var orders = await db.Orders.Where(o => o.Tc.Id == tcId).OrderBy(o => o.Date).ToListAsync();
                    int iter = 1;
                    foreach (Order item in orders)
                    {
                        iter++;
                        worksheet.Cell(iter, 1).Value = item.FirstName;
                        worksheet.Cell(iter, 2).Value = item.SurName;
                        worksheet.Cell(iter, 3).Value = item.FirstPlace;
                        worksheet.Cell(iter, 4).Value = item.LastPlace;
                        worksheet.Cell(iter, 5).Value = item.Weight;
                        worksheet.Cell(iter, 6).Value = item.Size;
                        worksheet.Cell(iter, 7).Value = item.Distance;
                        worksheet.Cell(iter, 8).Value = item.Price;
                        worksheet.Cell(iter, 9).Value = _repoTc.GetTc(tcId).Name;
                        worksheet.Cell(iter, 10).Value = item.Date;
                    }
                }
                
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "orders.xlsx"
                    };
                }
            }

        }

        public ActionResult TransportCompanies()
        {
            ViewModelTcs tcs = new ViewModelTcs()
            {
                Tcs = _repoTc.GetTcs()
            };
            return View(tcs);
        }
    }

}