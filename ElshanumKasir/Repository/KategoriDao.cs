using ElshanumKasir.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Repository
{
    class KategoriDao
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;

        public KategoriDao(MySqlConnection connection)
        {
            this._connection = connection;
        }

        public Kategori Save(Kategori kategori,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("INSERT INTO m_kategori(nama_kategori,keterangan) values(@NAMA_KATEGORI,@KETERANGAN)", _connection, transaction);

            MySqlParameter paramNamaKategori = new MySqlParameter();
            paramNamaKategori.MySqlDbType = MySqlDbType.String;
            paramNamaKategori.ParameterName = "@NAMA_KATEGORI";
            paramNamaKategori.Value = kategori.NamaKategori;

            MySqlParameter paramKeterangan = new MySqlParameter();
            paramKeterangan.MySqlDbType = MySqlDbType.String;
            paramKeterangan.ParameterName = "@KETERANGAN";
            paramKeterangan.Value = kategori.Keterangan;

            _command.Parameters.Add(paramNamaKategori);
            _command.Parameters.Add(paramKeterangan);

            _command.ExecuteNonQuery();
            kategori.KodeKategori = Convert.ToInt32(_command.LastInsertedId);

            return kategori;
        }

        public Kategori Update(Kategori kategori,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("UPDATE m_kategori SET nama_kategori=@NAMA_KATEGORI,keterangan=@KETERANGAN WHERE kode_kategori=@KODE_KATEGORI",
                        _connection,transaction);

            MySqlParameter paramNamaKategori = new MySqlParameter();
            paramNamaKategori.MySqlDbType = MySqlDbType.String;
            paramNamaKategori.ParameterName = "@NAMA_KATEGORI";
            paramNamaKategori.Value = kategori.NamaKategori;

            MySqlParameter paramKeterangan = new MySqlParameter();
            paramKeterangan.MySqlDbType = MySqlDbType.String;
            paramKeterangan.ParameterName = "@KETERANGAN";
            paramKeterangan.Value = kategori.Keterangan;

            MySqlParameter paramKodeKategori = new MySqlParameter();
            paramKodeKategori.MySqlDbType = MySqlDbType.Int32;
            paramKodeKategori.ParameterName = "@KODE_KATEGORI";
            paramKodeKategori.Value = kategori.KodeKategori;

            _command.Parameters.Add(paramNamaKategori);
            _command.Parameters.Add(paramKeterangan);
            _command.Parameters.Add(paramKodeKategori);

            _command.ExecuteNonQuery();

            return kategori;
        }

        public void Delete(int id,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("DELETE FROM m_kategori WHERE kode_kategori=@KODE_KATEGORI", _connection, transaction);

            MySqlParameter paramKodeKategori = new MySqlParameter();
            paramKodeKategori.MySqlDbType = MySqlDbType.Int32;
            paramKodeKategori.ParameterName = "@KODE_KATEGORI";
            paramKodeKategori.Value = id;

            _command.Parameters.Add(paramKodeKategori);

            _command.ExecuteNonQuery();
        }

        public List<Kategori> Find()
        {
            _command = new MySqlCommand("SELECT kode_kategori,nama_kategori,keterangan FROM m_kategori",
                        _connection);

            MySqlDataReader reader = _command.ExecuteReader();

            List<Kategori> kategoris = new List<Kategori>();

            Kategori kategori;

            while(reader.Read())
            {
                kategori = new Kategori();
                kategori.KodeKategori = reader.GetInt32("kode_kategori");
                kategori.NamaKategori = reader.GetString("nama_kategori");
                kategori.Keterangan = reader.GetString("keterangan");

                kategoris.Add(kategori);
            }

            return kategoris;
        }

        public Kategori FindOne(int id)
        {
            _command = new MySqlCommand("SELECT kode_kategori,nama_kategori,keterangan FROM m_kategori WHERE kode_kategori=@KODE_KATEGORI",
                        _connection);

            MySqlParameter paramKodeKategori = new MySqlParameter();
            paramKodeKategori.MySqlDbType = MySqlDbType.Int32;
            paramKodeKategori.ParameterName = "@KODE_KATEGORI";
            paramKodeKategori.Value = id;

            _command.Parameters.Add(paramKodeKategori);

            MySqlDataReader reader = _command.ExecuteReader();

            Kategori kategori = new Kategori();
            if(reader.Read())
            {
                kategori.KodeKategori = reader.GetInt32("kode_kategori");
                kategori.NamaKategori = reader.GetString("nama_kategori");
                kategori.Keterangan = reader.GetString("keterangan");
            }

            return kategori;
        }
    }
}
