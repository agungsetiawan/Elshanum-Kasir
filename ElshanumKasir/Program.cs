using ElshanumKasir.Model;
using ElshanumKasir.Repository;
using ElshanumKasir.Service;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElshanumKasir
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=elshanum;Uid=root;Pwd=;");

            BarangService service = new BarangService(connection);
            KategoriService kategoriService = new KategoriService(connection);

            Kategori kategori = new Kategori()
            {
                KodeKategori=2,
                NamaKategori="Kategori Two",
                Keterangan="Kategori Two"
            };

            Barang barang = new Barang()
            {
                //KodeBarang=5,
                NamaBarang="Barang One BARUU",
                HargaJual=100000,
                HargaBeli=80000,
                Stok=5,
                Keterangan="Barang Bagus BARUUU",
                Kategori=kategori
            };

            //Barang b = service.Save(barang);
            //service.Update(barang);
            //service.Delete(5);
            //var result = service.Find();
            //var result = service.FindOne(2);
            //var result2 = service.FindWithKategori();
            //var result2 = service.FindOneWithKategori(2);
            //var kategoriResult = kategoriService.Save(kategori);
            //var result = kategoriService.Update(kategori);
            //var result = kategoriService.Find();
            //kategoriService.Delete(2);
            var result = kategoriService.FindOne(1);
        }
    }
}
