using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestTask1_.net_framework_4._7._2_.Models.ViewModels
{
    public class ViewModelOrders : ViewModelOrderItem
    {
        public List<ViewModelOrderItem> Orders { get; set; }
    }
}