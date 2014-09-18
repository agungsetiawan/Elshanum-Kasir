using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElshanumKasir.Model
{
    public class Kategori
    {
        public int KodeKategori { get; set; }
        public string NamaKategori { get; set; }
        public string Keterangan { get; set; }
        public List<Barang> Barangs { get; set; }
    }
}
