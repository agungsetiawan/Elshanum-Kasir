using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Model
{
    public class Pelanggan
    {
        public int KodePelanggan { get; set; }
        public string NamaPelanggan { get; set; }
        public string Alamat { get; set; }
        public string NoTelepon { get; set; }
        public Member Member { get; set; }
    }
}
