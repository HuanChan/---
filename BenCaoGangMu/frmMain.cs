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
//using BenCaoGangMu.Roles;
using BenCaoGangMu.Users;
//using BenCaoGangMu.FormData;
using BenCaoGangMu.BusinessLogicLayer;

namespace BenCaoGangMu
{
    public partial class frmMain : Form
    {
        private User user;
        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(User _user)
        {
            InitializeComponent();
            user = _user;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
