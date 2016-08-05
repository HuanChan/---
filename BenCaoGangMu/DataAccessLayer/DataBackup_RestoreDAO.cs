using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//数据的备份、还原  
namespace BenCaoGangMu.DataAccessLayer
{
    class DataBackup_RestoreDAO
    {
        private string constring;
        public DataBackup_RestoreDAO()
        {
            constring = ConfigurationManager.ConnectionStrings["master_conString"].ToString();
        }
        /// <summary>
        ///    备份数据
        /// </summary>
        /// <param name="dbname">要备份的数据库名称</param>
        /// <param name="bkpath">备份文件的存放目录</param>
        /// <param name="bkfname">备份文件名,文件名中可以用\DBNAME\代表数据库名,\DATE\代表日期,\TIME\代表时间</param>
        /// <param name="bktype">备份类型:'DB'备份数据库,'DF' 差异备份,'LOG' 日志备份</param>
        /// <param name="appendfile">追加/覆盖备份文件,true：追加 ，false：覆盖</param>
        /// <returns>成功：true,失败:false</returns>
        public bool backup(string dbname, string bkpath, string bkfname, string bktype, bool appendfile)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@dbname", SqlDbType.VarChar, 30);
            parameters[1] = new SqlParameter("@bkpath", SqlDbType.NVarChar, 260);
            parameters[2] = new SqlParameter("@bkfname", SqlDbType.NVarChar, 260);
            parameters[3] = new SqlParameter("@bktype", SqlDbType.NVarChar, 10);
            parameters[4] = new SqlParameter("@appendfile", SqlDbType.Bit);

            parameters[0].Value = dbname;
            parameters[1].Value = bkpath;
            parameters[2].Value = bkfname;
            parameters[3].Value = bktype;
            parameters[4].Value = appendfile;
            return new DB(constring).executeSQL_Proc("p_backupdb", parameters) != 0 ? true : false;
        }

        /// <summary>
        ///    还原数据
        /// </summary>
        /// <param name="bkfile">定义要恢复的备份文件名</param> 
        /// <param name="dbname">定义恢复后的数据库名</param>
        /// <param name="bktype">恢复类型:'DB'--完成恢复数据库(RECOVERY),'DBNOR'--继续恢复数据库(NORECOVERY)</param>
        /// <returns>成功：true,失败:false</returns>
        public bool restore(string bkfile, string dbname, string bktype, bool overexist)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@bkfile", SqlDbType.NVarChar, 1000);
            parameters[1] = new SqlParameter("@dbname", SqlDbType.VarChar, 30);
            parameters[2] = new SqlParameter("@retype", SqlDbType.NVarChar, 10);
            parameters[3] = new SqlParameter("@overexist", SqlDbType.Bit);

            parameters[0].Value = bkfile;
            parameters[1].Value = dbname;
            parameters[2].Value = bktype;
            parameters[3].Value = @overexist;
            return new DB(constring).executeSQL_Proc("p_RestoreDb", parameters) != 0 ? true : false;
        }
        /// <summary>
        ///    获取服务器中所有数据库的名称
        /// </summary>
        public IList<string> getDbNames()
        {
            IList<string> dbnames = new List<string>();
            string sql = "use master select * from dbo.sysdatabases";
            SqlDataReader reader = (SqlDataReader)(new DB(constring).getSqlDataReader(sql, null));
            while (reader.Read())
            {
                dbnames.Add(reader["name"].ToString());
            }
            reader.Close();
            return dbnames;
        }

    }
}
