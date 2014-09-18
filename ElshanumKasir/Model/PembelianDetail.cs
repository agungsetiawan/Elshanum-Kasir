using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElshanumKasir.Model
{
    public class PembelianDetail
    {
        public int KodePembelianDetail { get; set; }
        public Pembelian Pembelian { get; set; }
        public Barang Barang { get; set; }
        public decimal Harga { get; set; }
        public int Jumlah { get; set; }
        public decimal SubTotal { get; set; }
        public float Diskon { get; set; }
    }
}
