using ElshanumKasir.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Repository
{
    class PenggunaDao
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;

        public PenggunaDao(MySqlConnection connection)
        {
            this._connection = connection;
        }

        public Pengguna Save(Pengguna pengguna,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("INSERT INTO m_pengguna(username,nama,password,kode_role) "+
                       "values(@USERNAME,@NAMA,@PASSWORD,@KODE_ROLE)", _connection, transaction);

            MySqlParameter paramUsername = new MySqlParameter();
            paramUsername.MySqlDbType = MySqlDbType.String;
            paramUsername.ParameterName = "@USERNAME";
            paramUsername.Value = pengguna.Username;

            MySqlParameter paramNama = new MySqlParameter();
            paramNama.MySqlDbType = MySqlDbType.String;
            paramNama.ParameterName = "@NAMA";
            paramNama.Value = pengguna.Nama;

            MySqlParameter paramPassword = new MySqlParameter();
            paramPassword.MySqlDbType = MySqlDbType.String;
            paramPassword.ParameterName = "@PASSWORD";
            paramPassword.Value = pengguna.Password;

            MySqlParameter paramKodeRole = new MySqlParameter();
            paramKodeRole.MySqlDbType = MySqlDbType.Int32;
            paramKodeRole.ParameterName = "@KODE_ROLE";
            paramKodeRole.Value = pengguna.Role.KodeRole;

            _command.Parameters.Add(paramUsername);
            _command.Parameters.Add(paramNama);
            _command.Parameters.Add(paramPassword);
            _command.Parameters.Add(paramKodeRole);

            _command.ExecuteNonQuery();
            pengguna.KodePengguna = Convert.ToInt32(_command.LastInsertedId);
            return pengguna;
        }

        public Pengguna Update(Pengguna pengguna,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("UPDATE m_pengguna SET " +
                       "username=@USERNAME,nama=@NAMA,password=@PASSWORD,kode_role=@KODE_ROLE "+
                       "WHERE kode_pengguna=@KODE_PENGGUNA", _connection, transaction);

            MySqlParameter paramUsername = new MySqlParameter();
            paramUsername.MySqlDbType = MySqlDbType.String;
            paramUsername.ParameterName = "@USERNAME";
            paramUsername.Value = pengguna.Username;

            MySqlParameter paramNama = new MySqlParameter();
            paramNama.MySqlDbType = MySqlDbType.String;
            paramNama.ParameterName = "@NAMA";
            paramNama.Value = pengguna.Nama;

            MySqlParameter paramPassword = new MySqlParameter();
            paramPassword.MySqlDbType = MySqlDbType.String;
            paramPassword.ParameterName = "@PASSWORD";
            paramPassword.Value = pengguna.Password;

            MySqlParameter paramKodeRole = new MySqlParameter();
            paramKodeRole.MySqlDbType = MySqlDbType.Int32;
            paramKodeRole.ParameterName = "@KODE_ROLE";
            paramKodeRole.Value = pengguna.Role.KodeRole;

            MySqlParameter paramKodePengguna = new MySqlParameter();
            paramKodePengguna.MySqlDbType = MySqlDbType.Int32;
            paramKodePengguna.ParameterName = "@KODE_PENGGUNA";
            paramKodePengguna.Value = pengguna.KodePengguna;

            _command.Parameters.Add(paramUsername);
            _command.Parameters.Add(paramNama);
            _command.Parameters.Add(paramPassword);
            _command.Parameters.Add(paramKodeRole);
            _command.Parameters.Add(paramKodePengguna);

            _command.ExecuteNonQuery();
            return pengguna;
        }

        public void Delete(int id,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("DELETE FROM m_pengguna where "+
                       "kode_pengguna=@KODE_PENGGUNA", _connection,transaction);

            MySqlParameter paramKodePengguna = new MySqlParameter();
            paramKodePengguna.MySqlDbType = MySqlDbType.Int32;
            paramKodePengguna.ParameterName = "@KODE_PENGGUNA";
            paramKodePengguna.Value = id;

            _command.Parameters.Add(paramKodePengguna);
            _command.ExecuteNonQuery();
        }

        public List<Pengguna> FindWithRole()
        {
            _command = new MySqlCommand("SELECT p.kode_pengguna,p.username,p.nama,"+
                       "r.nama_role FROM m_pengguna p INNER JOIN m_role r "+
                       "ON p.kode_role=r.kode_role",_connection);

            MySqlDataReader reader = _command.ExecuteReader();

            List<Pengguna> penggunas = new List<Pengguna>();
            Pengguna pengguna;
            Role role;

            while(reader.Read())
            {
                pengguna = new Pengguna();
                pengguna.KodePengguna = reader.GetInt32("kode_pengguna");
                pengguna.Username = reader.GetString("username");
                pengguna.Nama = reader.GetString("nama");

                role = new Role();
                role.NamaRole = reader.GetString("nama_role");

                pengguna.Role = role;

                penggunas.Add(pengguna);
            }

            return penggunas;
        }

        public Pengguna FindOneWithRole(int id)
        {
            _command = new MySqlCommand("SELECT p.kode_pengguna,p.username,p.nama," +
                       "r.nama_role FROM m_pengguna p INNER JOIN m_role r " +
                       "ON p.kode_role=r.kode_role WHERE kode_pengguna=@KODE_PENGGUNA",
                       _connection);

            MySqlParameter paramKodePengguna = new MySqlParameter();
            paramKodePengguna.MySqlDbType = MySqlDbType.Int32;
            paramKodePengguna.ParameterName = "@KODE_PENGGUNA";
            paramKodePengguna.Value = id;

            _command.Parameters.Add(paramKodePengguna);

            MySqlDataReader reader = _command.ExecuteReader();

            Pengguna pengguna = new Pengguna();
            Role role = new Role();

            if (reader.Read())
            {
                pengguna.KodePengguna = reader.GetInt32("kode_pengguna");
                pengguna.Username = reader.GetString("username");
                pengguna.Nama = reader.GetString("nama");
                
                role.NamaRole = reader.GetString("nama_role");

                pengguna.Role = role;
            }

            return pengguna;
        }
    }
}
