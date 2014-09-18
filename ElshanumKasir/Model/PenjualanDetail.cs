using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Model
{
    public class PenjualanDetail
    {
        public int KodePenjualanDetail { get; set; }
        public Penjualan Penjualan { get; set; }
        public Barang Barang { get; set; }
        public decimal Harga { get; set; }
        public int Jumlah { get; set; }
        public decimal SubTotal { get; set; }
        public float Diskon { get; set; }
    }
}
