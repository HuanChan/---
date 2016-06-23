using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BenCaoGangMu.Entity;
using BenCaoGangMu.BusinessLogicLayer;

namespace BenCaoGangMu.Users
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                //“密码：” 和 “确定” 之间的一个 Text 为无的字符串，框选即可选择
                lblmsg.Text = "请输入用户名";
                return;
            }
            else if (txtPwd.Text == "")
            {
                lblmsg.Text = "请输入密码";
                return;
            }

            //根据姓名，查找要登录系统的用户
            //实例化一个user类：user 类用于设置属性
            User user = UserService.queryByUserName(txtUserName.Text.Trim());
            if (user == null)
            {
                lblmsg.Text = "用户名不存在";
                return;
            }
            else if (user.UserPwd != txtPwd.Text.Trim())
            {
                lblmsg.Text = "密码错误";
                return;
            }

            //登陆成功，进入系统主窗体
            frmMain mainform = new frmMain(user);
            this.Hide();
            mainform.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
