using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElshanumKasir.Model
{
    public class Role
    {
        public int KodeRole { get; set; }
        public string NamaRole { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
