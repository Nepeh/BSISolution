using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSISolution.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public int ParVolume { get; set; }
        [Required]
        public int UnitPerBox { get; set; }
        public DateTime ExpiratioDate { get; set; }
        [Required]
        public int AvailibleBalance { get; set; }
        public string RecordedBy { get; set; }
        public DateTime DateRecorded { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Categories Categories { get; set; }
    }
}
