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
    class MemberService
    {
        private MySqlConnection _connection;
        private MemberDao _memberDao;

        public MemberService(MySqlConnection connection)
        {
            this._connection = connection;
            _memberDao = new MemberDao(_connection);
        }

        public Member Save(Member member)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Member memberReturn=_memberDao.Save(member,transaction);
                transaction.Commit();
                return memberReturn;
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }

            _connection.Close();

            return null;
        }

        public Member Update(Member member)
        {
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                Member memberReturn=_memberDao.Update(member,transaction);
                transaction.Commit();
                return memberReturn;
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
                _memberDao.Delete(id,transaction);
                transaction.Commit();
            }
            catch(MySqlException ex)
            {
                transaction.Rollback();
            }
            
            _connection.Close();
        }

        public List<Member> Find()
        {
            _connection.Open();
            List<Member> members = _memberDao.Find();
            _connection.Close();
            return members;
        }

        public Member FindOne(int id)
        {
            _connection.Open();
            Member member = _memberDao.FindOne(id);
            _connection.Close();
            return member;
        }
    }
}
