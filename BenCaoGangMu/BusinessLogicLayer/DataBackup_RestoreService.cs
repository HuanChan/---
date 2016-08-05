using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenCaoGangMu.DataAccessLayer;

//数据的备份、恢复
namespace BenCaoGangMu.BusinessLogicLayer
{
    class DataBackup_RestoreService
    {
        /// <summary>
        ///    备份数据
        /// </summary>
        /// <param name="dbname">要备份的数据库名称</param>
        /// <param name="bkpath">备份文件的存放目录</param>
        /// <param name="bkfname">备份文件名,文件名中可以用\DBNAME\代表数据库名,\DATE\代表日期,\TIME\代表时间</param>
        /// <param name="bktype">备份类型:'DB'备份数据库,'DF' 差异备份,'LOG' 日志备份</param>
        /// <param name="appendfile">追加/覆盖备份文件,true：追加 ，false：覆盖</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool backup(string dbname, string bkpath, string bkfname, string bktype, bool appendfile)
        {
            return new DataBackup_RestoreDAO().backup(dbname, bkpath, bkfname, bktype, appendfile);
        }

        /// <summary>
        ///    备份数据
        /// </summary>
        /// <param name="bkfile">定义要恢复的备份文件名</param> 
        /// <param name="dbname">定义恢复后的数据库名</param>
        /// <param name="bktype">恢复类型:'DB'--完成恢复数据库(RECOVERY),'DBNOR'--继续恢复数据库(NORECOVERY)</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool restore(string bkfile, string dbname, string bktype, bool appendfile)
        {
            return new DataBackup_RestoreDAO().restore(bkfile, dbname, bktype, appendfile);
        }

        /// <summary>
        ///    获取服务器中所有数据库的名称
        /// </summary>
        public static IList<string> getDbNames()
        {
            return new DataBackup_RestoreDAO().getDbNames();
        }

    }
}
