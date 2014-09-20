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
    class PenggunaService
    {
        private MySqlConnection _connection;
        private PenggunaDao _penggunaDao;

        public PenggunaService(MySqlConnection connection)
        {
            this._connection = connection;
            _penggunaDao = new PenggunaDao(_connection);
        }

        public Pengguna Save(Pengguna pengguna)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Pengguna penggunaReturn = _penggunaDao.Save(pengguna, transaction);
                transaction.Commit();
                return penggunaReturn;
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }

            _connection.Close();
            return null;
        }

        public Pengguna Update(Pengguna pengguna)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Pengguna penggunaReturn = _penggunaDao.Update(pengguna, transaction);
                transaction.Commit();
                return penggunaReturn;
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
                _penggunaDao.Delete(id,transaction);
                transaction.Commit();
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }

            _connection.Close();
        }

        public List<Pengguna> FindWithRole()
        {
            _connection.Open();
            List<Pengguna> penggunas = _penggunaDao.FindWithRole();
            _connection.Close();
            return penggunas;
        }

        public Pengguna FindOneWithRole(int id)
        {
            _connection.Open();
            Pengguna pengguna = _penggunaDao.FindOneWithRole(id);
            _connection.Close();
            return pengguna;
        }
    }
}
