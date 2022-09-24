using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Tc
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double CoefficientOfKilometer { get; set; }
        [Required]
        public double CoefficientOfKilogram { get; set; }
        [Required]
        public double CoefficientOfSize { get; set; }

        public List<Order> orders { get; set; } = new List<Order>();

    }
}
