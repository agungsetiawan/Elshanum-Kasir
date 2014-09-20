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
            PelangganService pelangganService=new PelangganService(connection);
            MemberService memberService=new MemberService(connection);
            SupplierService supplierService=new SupplierService(connection);
            PenggunaService penggunaService = new PenggunaService(connection);

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
            //var result = kategoriService.FindOne(1);

            Member member = new Member()
            {
                KodeMember=1,
                NamaMember="reseller",
                Diskon=10.5f
            };

            Pelanggan pelanggan = new Pelanggan()
            {
                KodePelanggan=1,
                NamaPelanggan="Yan Yin",
                Alamat="Kedungwuni",
                NoTelepon="085688889999",
                Member=member
            };

            //Pelanggan result = pelangganService.Save(pelanggan);
            //pelangganService.Update(pelanggan);
            //var result = pelangganService.FindWithMember();
            //var result = pelangganService.FindWithMemberByNama("Yan");
            //var result = memberService.Save(member);
            //memberService.Update(member);
            //var result = memberService.Find();
            //var result = memberService.FindOne(2);
            //pelangganService.Delete(1);
            //memberService.Delete(1);

            Supplier supplier = new Supplier()
            {
                KodeSupplier=1,
                NamaSupplier="Lazado",
                Alamat="Jkt",
                NoTelepon="0219090"
            };

            //Supplier result = supplierService.Save(supplier);
            //var result = supplierService.Update(supplier);
            //supplierService.Delete(1);
            //var result = supplierService.Find();
            //var result = supplierService.FindByNama("48");
            //var result = supplierService.FindOne(3);
            Role role=new Role()
            {
                KodeRole=2,
                NamaRole="admin",
            };

            Pengguna pengguna = new Pengguna()
            {
                Username="blinkawan",
                Nama="Beta",
                Password=PasswordHash.PasswordHash.CreateHash("abcde"),
                Role=role
            };

            //var result = penggunaService.Save(pengguna);
            //if (PasswordHash.PasswordHash.ValidatePassword("abcde", "sha1:1000:Yfvr94veDB4kb3A/k7rQUTRcwE99EsX2:qunx6YJeFZdfA4QeixwzB7w/nEWeado7"))
            //{
            //    var x = "hehe";
            //}
            //var result = penggunaService.Update(pengguna);
            //penggunaService.Delete(1);
            //var result = penggunaService.FindWithRole();
            var result = penggunaService.FindOneWithRole(2);
        }
    }
}
