using ElshanumKasir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ElshanumKasir.Repository
{
    class BarangDao
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;

        public BarangDao(MySqlConnection connection)
        {
            this._connection = connection;
        }
        public Barang Save(Barang barang,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("INSERT INTO m_barang" +
                 "(nama_barang,harga_jual,harga_beli,stok,keterangan,kode_kategori) values" +
                 "(@NAMA_BARANG,@HARGA_JUAL,@HARGA_BELI,@STOK,@KETERANGAN,@KODE_KATEGORI)", _connection,transaction);

            MySqlParameter paramNamaBarang = new MySqlParameter();
            paramNamaBarang.ParameterName = "@NAMA_BARANG";
            paramNamaBarang.MySqlDbType = MySqlDbType.VarChar;
            paramNamaBarang.Value = barang.NamaBarang;

            MySqlParameter paramHargaJual = new MySqlParameter();
            paramHargaJual.ParameterName = "@HARGA_JUAL";
            paramHargaJual.MySqlDbType = MySqlDbType.Decimal;
            paramHargaJual.Value = barang.HargaJual;

            MySqlParameter paramHargaBeli = new MySqlParameter();
            paramHargaBeli.ParameterName = "@HARGA_BELI";
            paramHargaBeli.MySqlDbType = MySqlDbType.Decimal;
            paramHargaBeli.Value = barang.HargaBeli;

            MySqlParameter paramStok = new MySqlParameter();
            paramStok.ParameterName = "@STOK";
            paramStok.MySqlDbType = MySqlDbType.Int32;
            paramStok.Value = barang.Stok;

            MySqlParameter paramKeterangan = new MySqlParameter();
            paramKeterangan.ParameterName = "@KETERANGAN";
            paramKeterangan.MySqlDbType = MySqlDbType.VarChar;
            paramKeterangan.Value = barang.Keterangan;

            MySqlParameter paramKodeKategori = new MySqlParameter();
            paramKodeKategori.ParameterName = "@KODE_KATEGORI";
            paramKodeKategori.MySqlDbType = MySqlDbType.Int32;
            paramKodeKategori.Value = barang.Kategori.KodeKategori;

            _command.Parameters.Add(paramNamaBarang);
            _command.Parameters.Add(paramHargaJual);
            _command.Parameters.Add(paramHargaBeli);
            _command.Parameters.Add(paramStok);
            _command.Parameters.Add(paramKeterangan);
            _command.Parameters.Add(paramKodeKategori);

            _command.ExecuteNonQuery();

            barang.KodeBarang=Convert.ToInt32(_command.LastInsertedId);

            return barang;
        }

        public Barang Update(Barang barang,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("UPDATE m_barang SET "+
            "nama_barang=@NAMA_BARANG,"+
            "harga_jual=@HARGA_JUAL,"+
            "harga_beli=@HARGA_BELI,"+
            "stok=@STOK,"+
            "keterangan=@KETERANGAN,"+
            "kode_kategori=@KODE_KATEGORI "+
            "WHERE kode_barang=@KODE_BARANG", _connection,transaction);

            MySqlParameter paramNamaBarang = new MySqlParameter();
            paramNamaBarang.ParameterName = "@NAMA_BARANG";
            paramNamaBarang.MySqlDbType = MySqlDbType.VarChar;
            paramNamaBarang.Value = barang.NamaBarang;

            MySqlParameter paramHargaJual = new MySqlParameter();
            paramHargaJual.ParameterName = "@HARGA_JUAL";
            paramHargaJual.MySqlDbType = MySqlDbType.Decimal;
            paramHargaJual.Value = barang.HargaJual;

            MySqlParameter paramHargaBeli = new MySqlParameter();
            paramHargaBeli.ParameterName = "@HARGA_BELI";
            paramHargaBeli.MySqlDbType = MySqlDbType.Decimal;
            paramHargaBeli.Value = barang.HargaBeli;

            MySqlParameter paramStok = new MySqlParameter();
            paramStok.ParameterName = "@STOK";
            paramStok.MySqlDbType = MySqlDbType.Int32;
            paramStok.Value = barang.Stok;

            MySqlParameter paramKeterangan = new MySqlParameter();
            paramKeterangan.ParameterName = "@KETERANGAN";
            paramKeterangan.MySqlDbType = MySqlDbType.VarChar;
            paramKeterangan.Value = barang.Keterangan;

            MySqlParameter paramKodeKategori = new MySqlParameter();
            paramKodeKategori.ParameterName = "@KODE_KATEGORI";
            paramKodeKategori.MySqlDbType = MySqlDbType.Int32;
            paramKodeKategori.Value = barang.Kategori.KodeKategori;

            MySqlParameter paramKodeBarang = new MySqlParameter();
            paramKodeBarang.ParameterName = "@KODE_BARANG";
            paramKodeBarang.Value = barang.KodeBarang;
            paramKodeBarang.MySqlDbType = MySqlDbType.Int32;

            _command.Parameters.Add(paramNamaBarang);
            _command.Parameters.Add(paramHargaJual);
            _command.Parameters.Add(paramHargaBeli);
            _command.Parameters.Add(paramStok);
            _command.Parameters.Add(paramKeterangan);
            _command.Parameters.Add(paramKodeKategori);
            _command.Parameters.Add(paramKodeBarang);

            _command.ExecuteNonQuery();
            return barang;
        }

        public void Delete(int id,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("DELETE FROM m_barang "+
                     "WHERE kode_barang=@KODE_BARANG",_connection,transaction);

            MySqlParameter paramKodeBarang = new MySqlParameter();
            paramKodeBarang.ParameterName = "@KODE_BARANG";
            paramKodeBarang.Value = id;
            paramKodeBarang.MySqlDbType = MySqlDbType.Int32;

            _command.Parameters.Add(paramKodeBarang);

            _command.ExecuteNonQuery();

        }

        public List<Barang> Find()
        {
            _command = new MySqlCommand("SELECT * FROM m_barang",_connection);
            MySqlDataReader reader = _command.ExecuteReader();

            List<Barang> barangs = new List<Barang>();

            Barang barang;
            while(reader.Read())
            {
                barang= new Barang();
                barang.KodeBarang = reader.GetInt32("kode_barang");
                barang.NamaBarang = reader.GetString("nama_barang");
                barang.HargaJual = reader.GetDecimal("harga_jual");
                barang.HargaBeli = reader.GetDecimal("harga_beli");
                barang.Stok = reader.GetInt32("stok");
                barang.Keterangan = reader.GetString("keterangan");

                barangs.Add(barang);
            }

            return barangs;
        }

        public List<Barang> FindWithKategori()
        {
            _command = new MySqlCommand("SELECT b.kode_barang,b.nama_barang,b.harga_jual,b.harga_beli,b.stok,b.keterangan,k.kode_kategori,k.nama_kategori FROM m_barang b "+
                        "INNER JOIN m_kategori k on b.kode_kategori=k.kode_kategori", _connection);
            MySqlDataReader reader = _command.ExecuteReader();

            List<Barang> barangs = new List<Barang>();

            Barang barang;
            Kategori kategori;
            while (reader.Read())
            {
                barang = new Barang();
                barang.KodeBarang = reader.GetInt32("kode_barang");
                barang.NamaBarang = reader.GetString("nama_barang");
                barang.HargaJual = reader.GetDecimal("harga_jual");
                barang.HargaBeli = reader.GetDecimal("harga_beli");
                barang.Stok = reader.GetInt32("stok");
                barang.Keterangan = reader.GetString("keterangan");

                kategori = new Kategori();
                kategori.KodeKategori = reader.GetInt32("kode_kategori");
                kategori.NamaKategori = reader.GetString("nama_kategori");

                barang.Kategori = kategori;

                barangs.Add(barang);
            }

            return barangs;
        }


        public Barang FindOne(int id)
        {
            _command = new MySqlCommand("SELECT kode_barang,nama_barang,harga_jual,harga_beli,stok,keterangan FROM m_barang WHERE kode_barang=@KODE_BARANG",
                        _connection);

            MySqlParameter paramKodeBarang = new MySqlParameter();
            paramKodeBarang.ParameterName = "@KODE_BARANG";
            paramKodeBarang.Value = id;
            paramKodeBarang.MySqlDbType = MySqlDbType.Int32;

            _command.Parameters.Add(paramKodeBarang);

            MySqlDataReader reader = _command.ExecuteReader();

            Barang barang = new Barang();
            if(reader.Read())
            {
                barang.KodeBarang = reader.GetInt32("kode_barang");
                barang.NamaBarang = reader.GetString("nama_barang");
                barang.HargaJual = reader.GetDecimal("harga_jual");
                barang.HargaBeli = reader.GetDecimal("harga_beli");
                barang.Stok = reader.GetInt32("stok");
                barang.Keterangan = reader.GetString("keterangan");
            }

            return barang;
        }

        public Barang FindOneWithKategori(int id)
        {
            _command = new MySqlCommand("SELECT b.kode_barang,b.nama_barang,b.harga_jual,b.harga_beli,b.stok,b.keterangan,k.kode_kategori,k.nama_kategori FROM m_barang b " +
                        "INNER JOIN m_kategori k on b.kode_kategori=k.kode_kategori WHERE b.kode_barang=@KODE_BARANG", _connection);

            MySqlParameter paramKodeBarang = new MySqlParameter();
            paramKodeBarang.MySqlDbType = MySqlDbType.Int32;
            paramKodeBarang.ParameterName = "@KODE_BARANG";
            paramKodeBarang.Value = id;

            _command.Parameters.Add(paramKodeBarang);

            MySqlDataReader reader = _command.ExecuteReader();

            Barang barang = new Barang();
            Kategori kategori;

            if(reader.Read())
            {
                barang.KodeBarang = reader.GetInt32("kode_barang");
                barang.NamaBarang = reader.GetString("nama_barang");
                barang.HargaJual = reader.GetDecimal("harga_jual");
                barang.HargaBeli = reader.GetDecimal("harga_beli");
                barang.Stok = reader.GetInt32("stok");
                barang.Keterangan = reader.GetString("keterangan");

                kategori = new Kategori();
                kategori.KodeKategori = reader.GetInt32("kode_kategori");
                kategori.NamaKategori = reader.GetString("nama_kategori");

                barang.Kategori = kategori;
            }   

            return barang;
  
        }

    }
}
