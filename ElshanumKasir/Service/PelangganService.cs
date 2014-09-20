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
    class PelangganService
    {
        private MySqlConnection _connection;
        private PelangganDao _pelangganDao;

        public PelangganService(MySqlConnection connection)
        {
            this._connection = connection;
            _pelangganDao = new PelangganDao(_connection);
        }

        public Pelanggan Save(Pelanggan pelanggan)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Pelanggan pelangganReturn = _pelangganDao.Save(pelanggan, transaction);
                transaction.Commit();

                return pelangganReturn;
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }

            _connection.Close();

            return null;
        }

        public Pelanggan Update(Pelanggan pelanggan)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Pelanggan pelangganReturn = _pelangganDao.Update(pelanggan,transaction);
                transaction.Commit();
                return pelangganReturn;
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
                _pelangganDao.Delete(id,transaction);
                transaction.Commit();
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }

            _connection.Close();
        }

        public List<Pelanggan> FindWithMember()
        {
            _connection.Open();
            List<Pelanggan> pelanggans = _pelangganDao.FindWithMember();
            _connection.Close();
            return pelanggans;
        }

        public List<Pelanggan> FindWithMemberByNama(string nama)
        {
            _connection.Open();
            List<Pelanggan> pelanggans = _pelangganDao.FindWithMemberByNama(nama);
            _connection.Close();
            return pelanggans;
        }

        public Pelanggan FindOneWithMember(int id)
        {
            _connection.Open();
            Pelanggan pelanggan = _pelangganDao.FindOneWithMember(id);
            _connection.Close();
            return pelanggan;
        }

    }
}
