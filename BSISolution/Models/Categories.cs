using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSISolution.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RecordedBy { get; set; }
        public DateTime DateTime { get; set; }
    }
}
