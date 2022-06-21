﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSISolution.Models
{
    public class SystemRoles
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string RecordedBy { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
