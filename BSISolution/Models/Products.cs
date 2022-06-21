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
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public double ProductPrice { get; set; }
        public int ParVolume { get; set; }
        public int UnitPerBox { get; set; }
        public DateTime ExpiratioDate { get; set; }
        public int AvailibleBalance { get; set; }
        public string RecordedBy { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
