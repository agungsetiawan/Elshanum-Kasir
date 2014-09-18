using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Model
{
    public class Pengguna
    {
        public int KodePengguna { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nama { get; set; }
        public Role Role { get; set; }
    }
}
