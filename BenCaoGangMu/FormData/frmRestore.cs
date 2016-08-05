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
    public partial class frmRestore : Form
    {
        public frmRestore()
        {
            InitializeComponent();
        }
        //浏览按钮
        private void btnSelectedFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "备份文件|*.bak";
            fileDialog.Multiselect = false;
            fileDialog.InitialDirectory = @"d:\DataBackup\";
            fileDialog.FilterIndex = 0;
            fileDialog.ShowReadOnly = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtBackFileName.Text = fileDialog.FileName;
                int i = txtBackFileName.Text.LastIndexOf("\\");
                int j = txtBackFileName.Text.IndexOf("_", i);
                txtDBName.Text = txtBackFileName.Text.Substring(i + 1, j - i - 1);
            }
        }
        //完整还原
        private void btnDBBack_Click(object sender, EventArgs e)
        {
            if (restore())
            {
                lblmsg.Text = "操作成功";
            }
            else
            {
                lblmsg.Text = "操作失败";
            }
        }
        //差异还原(?)
        private void btnDFback_Click(object sender, EventArgs e)
        {
            if (restore())
            {
                lblmsg.Text = "操作成功";
            }
            else
            {
                lblmsg.Text = "操作失败";
            }
        }
        //事务日志还原(?)
        private void btnLogBack_Click(object sender, EventArgs e)
        {
            if (restore())
            {
                lblmsg.Text = "操作成功";
            }
            else
            {
                lblmsg.Text = "操作失败";
            }
        }
        //关闭按钮
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //判断文本框的值是否为空
        private bool checkValue()
        {
            if (txtBackFileName.Text == "")
            {
                lblmsg.Text = "请选择要还原的文件";
                return false;
            }
            else if (txtDBName.Text == "")
            {
                lblmsg.Text = "请输入还原后的数据库名";
                return false;
            }
            return true;
        }
        /// <summary>
        ///    还原数据
        /// </summary>
        /// <param name="bkfile">定义要恢复的备份文件名</param> 
        /// <param name="dbname">定义恢复后的数据库名</param>
        /// <param name="bktype">恢复类型:'DB'--完成恢复数据库(RECOVERY),'DBNOR'--继续恢复数据库(NORECOVERY)</param>
        /// <returns>成功：true,失败:false</returns>
        public bool restore()
        {
            string bkfile;
            string dbname;
            string bktype;
            bool appendfile;
            bkfile = txtBackFileName.Text.Trim();
            dbname = txtDBName.Text.Trim();
            bktype = rdoContinue.Checked ? "DBNOR" : "DB";   //判断还原是否结束
            appendfile = false;
            return DataBackup_RestoreService.restore(bkfile, dbname, bktype, appendfile);
        }

        private void frmRestore_Load(object sender, EventArgs e)
        {

        }
    }
}

