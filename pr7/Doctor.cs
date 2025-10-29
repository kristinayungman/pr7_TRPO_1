using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr7
{
    public class Doctor
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Specialisation { get; set; }
        public int? Password { get; set; }

        public int? Password2 { get; set; }
    }
}
