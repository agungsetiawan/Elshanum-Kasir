using ElshanumKasir.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Repository
{
    class SupplierDao
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;

        public SupplierDao(MySqlConnection connection)
        {
            this._connection = connection;
        }

        public Supplier Save(Supplier supplier,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("INSERT INTO m_supplier(nama_supplier,alamat,no_telepon) "+
                        "VALUES(@NAMA_SUPPLIER,@ALAMAT,@NO_TELEPON)", _connection, transaction);

            MySqlParameter paramNamaSupplier = new MySqlParameter();
            paramNamaSupplier.MySqlDbType = MySqlDbType.String;
            paramNamaSupplier.ParameterName = "@NAMA_SUPPLIER";
            paramNamaSupplier.Value = supplier.NamaSupplier;

            MySqlParameter paramAlamat = new MySqlParameter();
            paramAlamat.MySqlDbType = MySqlDbType.String;
            paramAlamat.ParameterName = "@ALAMAT";
            paramAlamat.Value = supplier.Alamat;

            MySqlParameter paramNoTelepon = new MySqlParameter();
            paramNoTelepon.MySqlDbType = MySqlDbType.String;
            paramNoTelepon.ParameterName = "@NO_TELEPON";
            paramNoTelepon.Value = supplier.NoTelepon;

            _command.Parameters.Add(paramNamaSupplier);
            _command.Parameters.Add(paramAlamat);
            _command.Parameters.Add(paramNoTelepon);

            _command.ExecuteNonQuery();
            supplier.KodeSupplier = Convert.ToInt32(_command.LastInsertedId);

            return supplier;
        }

        public Supplier Update(Supplier supplier,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("UPDATE m_supplier SET "+
                        "nama_supplier=@NAMA_SUPPLIER,alamat=@ALAMAT, "+
                        "no_telepon=@NO_TELEPON WHERE "+
                        "kode_supplier=@KODE_SUPPLIER", _connection, transaction);

            MySqlParameter paramNamaSupplier = new MySqlParameter();
            paramNamaSupplier.MySqlDbType = MySqlDbType.String;
            paramNamaSupplier.ParameterName = "@NAMA_SUPPLIER";
            paramNamaSupplier.Value = supplier.NamaSupplier;

            MySqlParameter paramAlamat = new MySqlParameter();
            paramAlamat.MySqlDbType = MySqlDbType.String;
            paramAlamat.ParameterName = "@ALAMAT";
            paramAlamat.Value = supplier.Alamat;

            MySqlParameter paramNoTelepon = new MySqlParameter();
            paramNoTelepon.MySqlDbType = MySqlDbType.String;
            paramNoTelepon.ParameterName = "@NO_TELEPON";
            paramNoTelepon.Value = supplier.NoTelepon;

            MySqlParameter paramKodeSupplier = new MySqlParameter();
            paramKodeSupplier.MySqlDbType = MySqlDbType.Int32;
            paramKodeSupplier.ParameterName = "@KODE_SUPPLIER";
            paramKodeSupplier.Value = supplier.KodeSupplier;

            _command.Parameters.Add(paramNamaSupplier);
            _command.Parameters.Add(paramAlamat);
            _command.Parameters.Add(paramNoTelepon);
            _command.Parameters.Add(paramKodeSupplier);

            _command.ExecuteNonQuery();
            return supplier;
        }

        public void Delete(int id,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("DELETE FROM m_supplier WHERE "+
                        "kode_supplier=@KODE_SUPPLIER", _connection, transaction);

            MySqlParameter paramKodeSupplier = new MySqlParameter();
            paramKodeSupplier.MySqlDbType = MySqlDbType.Int32;
            paramKodeSupplier.ParameterName = "@KODE_SUPPLIER";
            paramKodeSupplier.Value = id;

            _command.Parameters.Add(paramKodeSupplier);

            _command.ExecuteNonQuery();
        }

        public List<Supplier> Find()
        {
            _command = new MySqlCommand("SELECT kode_supplier,nama_supplier,alamat,no_telepon "+
                        "FROM m_supplier", _connection);

            List<Supplier> suppliers = new List<Supplier>();

            MySqlDataReader reader = _command.ExecuteReader();

            Supplier supplier;

            while(reader.Read())
            {
                supplier = new Supplier();
                supplier.KodeSupplier = reader.GetInt32("kode_supplier");
                supplier.NamaSupplier = reader.GetString("nama_supplier");
                supplier.Alamat = reader.GetString("alamat");
                supplier.NoTelepon = reader.GetString("no_telepon");

                suppliers.Add(supplier);
            }

            return suppliers;
        }

        public List<Supplier> FindByNama(string nama)
        {
            _command = new MySqlCommand("SELECT kode_supplier,nama_supplier,alamat,no_telepon " +
                        "FROM m_supplier WHERE nama_supplier like @NAMA_SUPPLIER", _connection);

            MySqlParameter paramNamaSupplier = new MySqlParameter();
            paramNamaSupplier.MySqlDbType = MySqlDbType.String;
            paramNamaSupplier.ParameterName = "@NAMA_SUPPLIER";
            paramNamaSupplier.Value = "%" + nama + "%";

            _command.Parameters.Add(paramNamaSupplier);

            List<Supplier> suppliers = new List<Supplier>();

            MySqlDataReader reader = _command.ExecuteReader();

            Supplier supplier;

            while (reader.Read())
            {
                supplier = new Supplier();
                supplier.KodeSupplier = reader.GetInt32("kode_supplier");
                supplier.NamaSupplier = reader.GetString("nama_supplier");
                supplier.Alamat = reader.GetString("alamat");
                supplier.NoTelepon = reader.GetString("no_telepon");

                suppliers.Add(supplier);
            }

            return suppliers;
        }

        public Supplier FindOne(int id)
        {
            _command = new MySqlCommand("SELECT kode_supplier,nama_supplier,alamat,no_telepon "+
                        "FROM m_supplier WHERE kode_supplier=@KODE_SUPPLIER", _connection);

            MySqlParameter paramKodeSupplier = new MySqlParameter();
            paramKodeSupplier.MySqlDbType = MySqlDbType.Int32;
            paramKodeSupplier.ParameterName = "@KODE_SUPPLIER";
            paramKodeSupplier.Value = id;

            _command.Parameters.Add(paramKodeSupplier);

            Supplier supplier = new Supplier();

            MySqlDataReader reader = _command.ExecuteReader();

            if(reader.Read())
            {
                supplier.KodeSupplier = reader.GetInt32("kode_supplier");
                supplier.NamaSupplier = reader.GetString("nama_supplier");
                supplier.Alamat = reader.GetString("alamat");
                supplier.NoTelepon = reader.GetString("no_telepon");
            }

            return supplier;
        }
    }
}
