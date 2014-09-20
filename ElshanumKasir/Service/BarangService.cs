using ElshanumKasir.Model;
using ElshanumKasir.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Service
{
    class BarangService
    {
        private BarangDao _barangDao;
        private MySqlConnection _connection;
        public BarangService(MySqlConnection connection)
        {
           this. _connection = connection;
           _barangDao = new BarangDao(_connection);
        }

        public Barang Save(Barang barang)
        {
            _connection.Open();

            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Barang barangReturn = _barangDao.Save(barang, transaction);
                transaction.Commit();

                return barangReturn;
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }
            

            _connection.Close();

            return null;
        }

        public Barang Update(Barang barang)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Barang barangReturn = _barangDao.Update(barang, transaction);
                transaction.Commit();

                return barangReturn;
            }
            catch (MySqlException ex)
            {
                transaction.Rollback();
            }


            _connection.Close();

            return null;
        }

        public void Delete(int id)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                _barangDao.Delete(id, transaction);
                transaction.Commit();
            }
            catch (MySqlException ex)
            {
                transaction.Rollback();
            }


            _connection.Close();
        }

        public List<Barang> Find()
        {
            _connection.Open();
            List<Barang> barangs = _barangDao.Find();
            _connection.Close();
            return barangs;
        }

        public Barang FindOne(int id)
        {
            _connection.Open();
            Barang barang = _barangDao.FindOne(id);
            _connection.Close();
            return barang;
        }

        public Barang FindOneWithKategori(int id)
        {
            _connection.Open();
            Barang barang = _barangDao.FindOneWithKategori(id);
            _connection.Close();
            return barang;
        }

        public List<Barang> FindWithKategori()
        {
            _connection.Open();
            List<Barang> barangs = _barangDao.FindWithKategori();
            _connection.Close();
            return barangs;
        }
    }
}
