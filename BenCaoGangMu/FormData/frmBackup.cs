using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenCaoGangMu.BusinessLogicLayer;

namespace BenCaoGangMu.FormData
{
    public partial class frmBackup : Form
    {
        public frmBackup()
        {
            InitializeComponent();
        }

        private void frmBackup_Load(object sender, EventArgs e)
        {
            //为数据库名称下拉列表赋值
            cboDbName.DataSource = DataBackup_RestoreService.getDbNames();
            cboDbName.Text = "BenCaoGangMu";
        }
        //浏览文件夹
        private void btnSelectedFold_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = folderDialog.SelectedPath;
            }
        }
        //完整备份
        private void btnDBBack_Click(object sender, EventArgs e)
        {
            //OpenFileDialog fileDialog = new OpenFileDialog();
            //SaveFileDialog fileDialog = new SaveFileDialog();
            //FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            //if (fileDialog.ShowDialog() == DialogResult.OK)
            //{
            //   MessageBox.Show( fileDialog.SelectedPath);
            //}     
            if (!checkValue()) return;
            string dbname;              //要备份的数据库名称
            string bkpath;              //备份文件保存的文件夹
            string bkfname;             //备份文件名,文件名中可以用\DBNAME\代表数据库名,\DATE\代表日期,\TIME\代表时间
            string bktype;             //备份类型:'DB'备份数据库,'DF' 差异备份,'LOG' 日志备份
            bool appendfile;           //追加/覆盖备份文件
            dbname = cboDbName.Text.Trim();
            bkpath = txtPath.Text.Trim() + "\\";
            bkfname = dbname + "_\\DATE\\_db.bak";
            bktype = "DB";
            appendfile = false;

            bool flag = backup(dbname, bkpath, bkfname, bktype, appendfile);
            if (flag)
            {
                lblmsg.Text = "操作成功 ";
            }
            else
            {
                lblmsg.Text = "操作失败 ";
            }

        }

        //差异备份
        private void btnDFback_Click(object sender, EventArgs e)
        {
            if (!checkValue()) return;
            string dbname;              //要备份的数据库名称
            string bkpath;              //备份文件保存的文件夹
            string bkfname;             //备份文件名,文件名中可以用\DBNAME\代表数据库名,\DATE\代表日期,\TIME\代表时间
            string bktype;             //备份类型:'DB'备份数据库,'DF' 差异备份,'LOG' 日志备份
            bool appendfile;           //追加/覆盖备份文件
            dbname = cboDbName.Text.Trim();
            bkpath = txtPath.Text.Trim() + "\\";
            bkfname = dbname + "_\\DATE\\_df.bak";
            bktype = "DF";
            appendfile = false;

            bool flag = backup(dbname, bkpath, bkfname, bktype, appendfile);
            if (flag)
            {
                lblmsg.Text = "操作成功 ";
            }
            else
            {
                lblmsg.Text = "操作失败 ";
            }
        }
        //事务日志备份
        private void btnLogBack_Click(object sender, EventArgs e)
        {
            if (!checkValue()) return;
            string dbname;              //要备份的数据库名称
            string bkpath;              //备份文件保存的文件夹
            string bkfname;             //备份文件名,文件名中可以用\DBNAME\代表数据库名,\DATE\代表日期,\TIME\代表时间
            string bktype;             //备份类型:'DB'备份数据库,'DF' 差异备份,'LOG' 日志备份
            bool appendfile;           //追加/覆盖备份文件
            dbname = cboDbName.Text.Trim();
            bkpath = txtPath.Text.Trim() + "\\";
            bkfname = dbname + "_\\DATE\\_log.bak";
            bktype = "LOG";
            appendfile = false;

            bool flag = backup(dbname, bkpath, bkfname, bktype, appendfile);
            if (flag)
            {
                lblmsg.Text = "操作成功 ";
            }
            else
            {
                lblmsg.Text = "操作失败 ";
            }
        }
        //关闭按钮
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private bool backup(string dbname, string bkpath, string bkfname, string bktype, bool appendfile)
        {
            return DataBackup_RestoreService.backup(dbname, bkpath, bkfname, bktype, appendfile);
        }
        //检查文本框值的合理性
        private bool checkValue()
        {
            if (cboDbName.Text == "")
            {
                lblmsg.Text = "请选择要备份的数据库";
                return false;
            }
            else if (txtPath.Text == "")
            {
                lblmsg.Text = "请选择备份文件保存的文件夹";
                return false;
            }
            return true;
        }


    }
}