using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ado_net.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public char Gender { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
    }
}
