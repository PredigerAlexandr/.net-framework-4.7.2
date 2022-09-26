using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestTask1_.net_framework_4._7._2_.Models.ViewModels
{
    public class ViewModelOrderItem
    {
        [DisplayName("Имя")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        [StringLength(20)]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        [StringLength(20)]
        public string SurName { get; set; }

        [DisplayName("Номер Телефона")]
        [Phone(ErrorMessage = "Ввведите корректные данные")]
        public string Phone { get; set; }

        [DisplayName("Откуда забрать")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        [StringLength(50)]
        public string FirstPlace { get; set; }

        [DisplayName("Куда доставить")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        [StringLength(50)]
        public string LastPlace { get; set; }

        [DisplayName("Вес(кг)")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        public double Weight { get; set; }

        [DisplayName("Объём(куб.см)")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        public double Size { get; set; }
        public double Price { get; set; }

        public int Distance { get; set; }

        public int TcId { get; set; }

        public string TcName { get; set; }

    }
}