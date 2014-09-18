using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Model
{
    public class Pembelian
    {
        public int KodePembelian { get; set; }
        public DateTime Tanggal { get; set; }
        public DateTime Jam { get; set; }
        public decimal TotalHargaBeli { get; set; }
        public Supplier Supplier { get; set; }
        public Pengguna Pengguna { get; set; }
        public List<PembelianDetail> PembelianDetails { get; set; }
    }
}
