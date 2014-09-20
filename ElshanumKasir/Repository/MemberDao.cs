using ElshanumKasir.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElshanumKasir.Repository
{
    class MemberDao
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;

        public MemberDao(MySqlConnection connection)
        {
            this._connection = connection;
        }

        public Member Save(Member member,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("INSERT INTO m_member(nama_member,diskon) "+
                        "values(@NAMA_MEMBER,@DISKON)",_connection,transaction);

            MySqlParameter paramNamaMember = new MySqlParameter();
            paramNamaMember.MySqlDbType = MySqlDbType.String;
            paramNamaMember.ParameterName = "@NAMA_MEMBER";
            paramNamaMember.Value = member.NamaMember;

            MySqlParameter paramDiskon = new MySqlParameter();
            paramDiskon.MySqlDbType = MySqlDbType.Float;
            paramDiskon.ParameterName = "@DISKON";
            paramDiskon.Value = member.Diskon;

            _command.Parameters.Add(paramNamaMember);
            _command.Parameters.Add(paramDiskon);

            _command.ExecuteNonQuery();
            member.KodeMember = Convert.ToInt32(_command.LastInsertedId);

            return member;
        }

        public Member Update(Member member,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("UPDATE m_member SET nama_member=@NAMA_MEMBER,diskon=@DISKON "+
                                        "WHERE kode_member=@KODE_MEMBER", _connection, transaction);

            MySqlParameter paramNamaMember = new MySqlParameter();
            paramNamaMember.MySqlDbType = MySqlDbType.String;
            paramNamaMember.ParameterName = "@NAMA_MEMBER";
            paramNamaMember.Value = member.NamaMember;

            MySqlParameter paramDiskon = new MySqlParameter();
            paramDiskon.MySqlDbType = MySqlDbType.Float;
            paramDiskon.ParameterName = "@DISKON";
            paramDiskon.Value = member.Diskon;

            MySqlParameter paramKodeMember = new MySqlParameter();
            paramKodeMember.MySqlDbType = MySqlDbType.Int32;
            paramKodeMember.ParameterName = "@KODE_MEMBER";
            paramKodeMember.Value = member.KodeMember;

            _command.Parameters.Add(paramNamaMember);
            _command.Parameters.Add(paramDiskon);
            _command.Parameters.Add(paramKodeMember);

            _command.ExecuteNonQuery();
            return member;
        }

        public void Delete(int id,MySqlTransaction transaction)
        {
            _command = new MySqlCommand("DELETE FROM m_member WHERE kode_member=@KODE_MEMBER",
                        _connection,transaction);

            MySqlParameter paramKodeMember = new MySqlParameter();
            paramKodeMember.MySqlDbType = MySqlDbType.Int32;
            paramKodeMember.ParameterName = "@KODE_MEMBER";
            paramKodeMember.Value = id;

            _command.Parameters.Add(paramKodeMember);

            _command.ExecuteNonQuery();
        }

        public List<Member> Find()
        {
            _command = new MySqlCommand("SELECT kode_member,nama_member,diskon FROM m_member",
                        _connection);

            List<Member> members = new List<Member>();

            Member member;

            MySqlDataReader reader = _command.ExecuteReader();

            while(reader.Read())
            {
                member = new Member();
                member.KodeMember = reader.GetInt32("kode_member");
                member.NamaMember = reader.GetString("nama_member");
                member.Diskon = reader.GetFloat("diskon");

                members.Add(member);
            }

            return members;
        }

        public Member FindOne(int id)
        {
            _command = new MySqlCommand("SELECT kode_member,nama_member,diskon FROM "+
                        "m_member WHERE kode_member=@KODE_MEMBER", _connection);

            MySqlParameter paramKodeMember = new MySqlParameter();
            paramKodeMember.MySqlDbType = MySqlDbType.Int32;
            paramKodeMember.ParameterName = "@KODE_MEMBER";
            paramKodeMember.Value = id;

            _command.Parameters.Add(paramKodeMember);

            MySqlDataReader reader = _command.ExecuteReader();

            Member member = new Member();

            if(reader.Read())
            {     
                member.KodeMember = reader.GetInt32("kode_member");
                member.NamaMember = reader.GetString("nama_member");
                member.Diskon = reader.GetFloat("diskon");
            }

            return member;
        }
    }
}
