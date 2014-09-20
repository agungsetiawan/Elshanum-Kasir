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
    class SupplierService
    {
        private MySqlConnection _connection;
        private SupplierDao _supplierDao;

        public SupplierService(MySqlConnection connection)
        {
            this._connection = connection;
            _supplierDao = new SupplierDao(_connection);
        }

        public Supplier Save(Supplier supplier)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Supplier supplierReturn=_supplierDao.Save(supplier, transaction);
                transaction.Commit();
                return supplierReturn;
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }

            _connection.Close();
            return null;
        }

        public Supplier Update(Supplier supplier)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Supplier supplierReturn = _supplierDao.Update(supplier, transaction);
                transaction.Commit();
                return supplierReturn;
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
                _supplierDao.Delete(id,transaction);
                transaction.Commit();
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }

            _connection.Close();
        }

        public List<Supplier> Find()
        {
            _connection.Open();
            List<Supplier> suppliers = _supplierDao.Find();
            _connection.Close();
            return suppliers;
        }

        public List<Supplier> FindByNama(string nama)
        {
            _connection.Open();
            List<Supplier> suppliers = _supplierDao.FindByNama(nama);
            _connection.Close();
            return suppliers;
        }

        public Supplier FindOne(int id)
        {
            _connection.Open();
            Supplier supplier = _supplierDao.FindOne(id);
            _connection.Close();
            return supplier;
        }
    }
}
