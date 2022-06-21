using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSISolutions.Models
{
    public class ProductViewModel
    {
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public double ProductPrice { get; set; }
        public int ParVolume { get; set; }
        public int UnitPerBox { get; set; }
        public DateTime ExpiratioDate { get; set; }
        public int AvailibleBalance { get; set; }
    }
}
