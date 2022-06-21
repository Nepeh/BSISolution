using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSISolution.Models
{
    public class UserSystemRoles
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RoleID { get; set; }
        public string RecordedBy { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
