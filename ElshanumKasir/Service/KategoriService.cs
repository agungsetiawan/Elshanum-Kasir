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
    class KategoriService
    {
        private MySqlConnection _connection;
        private KategoriDao _kategoriDao;

        public KategoriService(MySqlConnection connection)
        {
            this._connection = connection;
            _kategoriDao = new KategoriDao(_connection);
        }

        public Kategori Save(Kategori kategori)
        {
            _connection.Open();

            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Kategori kategoriReturn = _kategoriDao.Save(kategori, transaction);
                transaction.Commit();
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }

            _connection.Close();

            return null;
        }

        public Kategori Update(Kategori kategori)
        {
            _connection.Open();

            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Kategori kategoriReturn = _kategoriDao.Update(kategori,transaction);
                transaction.Commit();
            }
            catch(MySqlException ex)
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
                _kategoriDao.Delete(id,transaction);
                transaction.Commit();
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }

            _connection.Close();
        }

        public List<Kategori> Find()
        {
            _connection.Open();
            List<Kategori> kategoris = _kategoriDao.Find();
            _connection.Close();
            return kategoris;
        }

        public Kategori FindOne(int id)
        {
            _connection.Open();
            Kategori kategoriReturn = _kategoriDao.FindOne(id);
            _connection.Close();
            return kategoriReturn;
        }
    }
}
