using ElshanumKasir.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Repository
{
    class PelangganDao
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;

        public PelangganDao(MySqlConnection connection)
        {
            this._connection = connection;
        }

        public Pelanggan Save(Pelanggan pelanggan,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("INSERT INTO m_pelanggan(nama_pelanggan,alamat,no_telepon,kode_member) values(@NAMA_PELANGGAN,@ALAMAT,@NO_TELEPON,@KODE_MEMBER)",
                        _connection, transaction);

            MySqlParameter paramNamaPelanggan = new MySqlParameter();
            paramNamaPelanggan.MySqlDbType = MySqlDbType.String;
            paramNamaPelanggan.ParameterName = "@NAMA_PELANGGAN";
            paramNamaPelanggan.Value = pelanggan.NamaPelanggan;

            MySqlParameter paramAlamat = new MySqlParameter();
            paramAlamat.MySqlDbType = MySqlDbType.String;
            paramAlamat.ParameterName = "@ALAMAT";
            paramAlamat.Value = pelanggan.Alamat;

            MySqlParameter paramNoTelepon = new MySqlParameter();
            paramNoTelepon.MySqlDbType = MySqlDbType.String;
            paramNoTelepon.ParameterName = "@NO_TELEPON";
            paramNoTelepon.Value = pelanggan.NoTelepon;

            MySqlParameter paramKodeMember = new MySqlParameter();
            paramKodeMember.MySqlDbType = MySqlDbType.Int32;
            paramKodeMember.ParameterName = "@KODE_MEMBER";
            paramKodeMember.Value = pelanggan.Member.KodeMember;

            _command.Parameters.Add(paramNamaPelanggan);
            _command.Parameters.Add(paramAlamat);
            _command.Parameters.Add(paramNoTelepon);
            _command.Parameters.Add(paramKodeMember);

            _command.ExecuteNonQuery();
            pelanggan.KodePelanggan = Convert.ToInt32(_command.LastInsertedId);

            return pelanggan;
        }

        public Pelanggan Update(Pelanggan pelanggan,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("UPDATE m_pelanggan SET nama_pelanggan=@NAMA_PELANGGAN, alamat=@ALAMAT,no_telepon=@NO_TELEPON, kode_member=@KODE_MEMBER WHERE kode_pelanggan=@KODE_PELANGGAN",
                        _connection, transaction);

            MySqlParameter paramNamaPelanggan = new MySqlParameter();
            paramNamaPelanggan.MySqlDbType = MySqlDbType.String;
            paramNamaPelanggan.ParameterName = "@NAMA_PELANGGAN";
            paramNamaPelanggan.Value = pelanggan.NamaPelanggan;

            MySqlParameter paramAlamat = new MySqlParameter();
            paramAlamat.MySqlDbType = MySqlDbType.String;
            paramAlamat.ParameterName = "@ALAMAT";
            paramAlamat.Value = pelanggan.Alamat;

            MySqlParameter paramNoTelepon = new MySqlParameter();
            paramNoTelepon.MySqlDbType = MySqlDbType.String;
            paramNoTelepon.ParameterName = "@NO_TELEPON";
            paramNoTelepon.Value = pelanggan.NoTelepon;

            MySqlParameter paramKodePelanggan = new MySqlParameter();
            paramKodePelanggan.MySqlDbType = MySqlDbType.Int32;
            paramKodePelanggan.ParameterName = "@KODE_PELANGGAN";
            paramKodePelanggan.Value = pelanggan.KodePelanggan;

            MySqlParameter paramKodeMember = new MySqlParameter();
            paramKodeMember.MySqlDbType = MySqlDbType.Int32;
            paramKodeMember.ParameterName = "@KODE_MEMBER";
            paramKodeMember.Value = pelanggan.Member.KodeMember;

            _command.Parameters.Add(paramNamaPelanggan);
            _command.Parameters.Add(paramAlamat);
            _command.Parameters.Add(paramNoTelepon);
            _command.Parameters.Add(paramKodeMember);
            _command.Parameters.Add(paramKodePelanggan);

            _command.ExecuteNonQuery();

            return pelanggan;
        }

        public void Delete(int id,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("DELETE FROM m_pelanggan WHERE kode_pelanggan=@KODE_PELANGGAN",
                        _connection, transaction);

            MySqlParameter paramKodePelanggan = new MySqlParameter();
            paramKodePelanggan.MySqlDbType = MySqlDbType.Int32;
            paramKodePelanggan.ParameterName = "@KODE_PELANGGAN";
            paramKodePelanggan.Value = id;

            _command.Parameters.Add(paramKodePelanggan);

            _command.ExecuteNonQuery();
        }

        public List<Pelanggan> FindWithMember()
        {
            _command = new MySqlCommand("SELECT p.kode_pelanggan,p.nama_pelanggan,p.alamat,p.no_telepon,m.kode_member,m.nama_member,m.diskon "+
                                        "FROM m_pelanggan p INNER JOIN m_member m on p.kode_member=m.kode_member",_connection);

            List<Pelanggan> pelanggans = new List<Pelanggan>();

            MySqlDataReader reader = _command.ExecuteReader();

            Pelanggan pelanggan;
            Member member;

            while(reader.Read())
            {
                pelanggan = new Pelanggan();
                pelanggan.KodePelanggan = reader.GetInt32("kode_pelanggan");
                pelanggan.NamaPelanggan = reader.GetString("nama_pelanggan");
                pelanggan.Alamat = reader.GetString("alamat");
                pelanggan.NoTelepon = reader.GetString("no_telepon");

                member = new Member();
                member.KodeMember = reader.GetInt32("kode_member");
                member.NamaMember = reader.GetString("nama_member");
                member.Diskon = reader.GetFloat("diskon");

                pelanggan.Member = member;

                pelanggans.Add(pelanggan);
            }

            return pelanggans;
        }

        public List<Pelanggan> FindWithMemberByNama(string nama)
        {
            _command = new MySqlCommand("SELECT p.kode_pelanggan,p.nama_pelanggan,p.alamat,p.no_telepon,m.kode_member,m.nama_member,m.diskon " +
                                        "FROM m_pelanggan p INNER JOIN m_member m on p.kode_member=m.kode_member "+
                                        "WHERE p.nama_pelanggan like @NAMA_PELANGGAN", _connection);

            List<Pelanggan> pelanggans = new List<Pelanggan>();

            MySqlParameter paramNamaPelanggan = new MySqlParameter();
            paramNamaPelanggan.MySqlDbType = MySqlDbType.String;
            paramNamaPelanggan.ParameterName = "@NAMA_PELANGGAN";
            paramNamaPelanggan.Value = "%" + nama + "%";

            _command.Parameters.Add(paramNamaPelanggan);

            MySqlDataReader reader = _command.ExecuteReader();

            Pelanggan pelanggan;
            Member member;

            while (reader.Read())
            {
                pelanggan = new Pelanggan();
                pelanggan.KodePelanggan = reader.GetInt32("kode_pelanggan");
                pelanggan.NamaPelanggan = reader.GetString("nama_pelanggan");
                pelanggan.Alamat = reader.GetString("alamat");
                pelanggan.NoTelepon = reader.GetString("no_telepon");

                member = new Member();
                member.KodeMember = reader.GetInt32("kode_member");
                member.NamaMember = reader.GetString("nama_member");
                member.Diskon = reader.GetFloat("diskon");

                pelanggan.Member = member;

                pelanggans.Add(pelanggan);
            }

            return pelanggans;
        }

        public Pelanggan FindOneWithMember(int id)
        {
            _command = new MySqlCommand("SELECT p.kode_pelanggan,p.nama_pelanggan,p.alamat,p.no_telepon,m.kode_member,m.nama_member,m.diskon " +
                                        "FROM m_pelanggan p INNER JOIN m_member m on p.kode_member=m.kode_member "+
                                        "WHERE kode_pelanggan=@KODE_PELANGGAN", _connection);

            MySqlParameter paramKodePelanggan = new MySqlParameter();
            paramKodePelanggan.MySqlDbType = MySqlDbType.Int32;
            paramKodePelanggan.ParameterName = "@KODE_PELANGGAN";
            paramKodePelanggan.Value = id;

            _command.Parameters.Add(paramKodePelanggan);

            MySqlDataReader reader = _command.ExecuteReader();

            Pelanggan pelanggan = new Pelanggan();
            Member member = new Member();

            if (reader.Read())
            {
                
                pelanggan.KodePelanggan = reader.GetInt32("kode_pelanggan");
                pelanggan.NamaPelanggan = reader.GetString("nama_pelanggan");
                pelanggan.Alamat = reader.GetString("alamat");
                pelanggan.NoTelepon = reader.GetString("no_telepon");

                member.KodeMember = reader.GetInt32("kode_member");
                member.NamaMember = reader.GetString("nama_member");
                member.Diskon = reader.GetFloat("diskon");

                pelanggan.Member = member;

            }

            return pelanggan;
        }
    }
}
