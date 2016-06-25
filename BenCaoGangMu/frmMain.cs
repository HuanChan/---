using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

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

        //_user：登录成功的用户
        public frmMain(User _user)
        {
            InitializeComponent();
            user = _user;
        }

        //1 判断子窗体是否已经存在。
        //返回值。已经存在：true；不存在：false。
        //注意：formName，为对应的类名
        private bool GetInstanceState(string formName)
        {
            //获得 frmMain 窗体的子窗体的数量
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                //依次判断指定的子窗体是否存在
                if (this.MdiChildren[i].Name == formName)
                {
                    //如果存在名为 formName 的子窗体，该子窗体获得焦点并返回True
                    this.MdiChildren[i].Focus();
                    return true;
                }
            }
            //如果不存在名为 formName 的子窗体就返回 false
            return false;
        }

        //2 （应用泛型和反射机制）显示子窗体
        private void showChildForm<T>()     //获取子窗口的类型
        {
            Type modelType = typeof(T);     //获得子窗体泛型的类型 modelType
            T model = Activator.CreateInstance<T>();//反射创建对象：创建子窗体泛型的引用 model

            PropertyInfo pi = modelType.GetProperty("WindowState", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (pi != null)
            {
                //设定属性对应的值
                pi.SetValue(model, FormWindowState.Normal, null);
            }
            PropertyInfo pi1 = modelType.GetProperty("StartPosition", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (pi1 != null)
            {
                //设定属性对应的值
                pi1.SetValue(model, FormStartPosition.CenterParent, null);
            }
            PropertyInfo pi2 = modelType.GetProperty("MdiParent", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (pi2 != null)
            {
                //设定属性对应的值
                pi2.SetValue(model, this, null);
            }
            MethodInfo methodInfo = modelType.GetMethod("Show", new Type[] { });
            methodInfo.Invoke(model, null);
        }

        //2 （应用基类）显示子窗体
        private void showChildForm(Form form)
        {
            form.WindowState = FormWindowState.Normal;//子窗体为默认窗口大小
            form.StartPosition = FormStartPosition.CenterParent;//子窗体在其父窗体中居中
            form.MdiParent = this;      //子窗体的父窗体是this
            form.Show();
        }

        //获取用户的权限
        private IList<Actions> getActions()
        {
            return UserService.queryActions(user.UserId);
        }

        //3 根据权限，设置相应的菜单项为有效
        private void showMenu()
        {
            //定义包含所有菜单项的数组（注意：要包含所有的菜单项）
            ToolStripMenuItem[] menus = new ToolStripMenuItem[]
            {
                资料管理ToolStripMenuItem,销售员资料管理ToolStripMenuItem,客户资料管理ToolStripMenuItem,商品资料管理ToolStripMenuItem,供应商资料管理ToolStripMenuItem,采购管理ToolStripMenuItem,编辑采购单ToolStripMenuItem,编辑采购退货单ToolStripMenuItem,浏览采购信息ToolStripMenuItem,浏览采购退货信息ToolStripMenuItem,销售管理ToolStripMenuItem,编辑销售单ToolStripMenuItem,编辑销售退货单ToolStripMenuItem,浏览销售信息ToolStripMenuItem,浏览销售退货信息ToolStripMenuItem,统计查询ToolStripMenuItem,供应商交易记录ToolStripMenuItem,客户交易记录ToolStripMenuItem,库存查询ToolStripMenuItem,采购汇总ToolStripMenuItem,数据管理ToolStripMenuItem,数据备份ToolStripMenuItem,完整备份ToolStripMenuItem,差异备份ToolStripMenuItem,日志备份ToolStripMenuItem,数据还原ToolStripMenuItem,完整备份还原数据库ToolStripMenuItem,差异还原备份ToolStripMenuItem,日志还原备份ToolStripMenuItem,历史数据管理ToolStripMenuItem,导出ToolStripMenuItem,导出采购表数据ToolStripMenuItem,导出销售表数据ToolStripMenuItem,导出采购退货表数据ToolStripMenuItem,导出销售退货表数据ToolStripMenuItem,导入ToolStripMenuItem,导入采购表数据ToolStripMenuItem,导入销售表数据ToolStripMenuItem,导入采购退货表数据ToolStripMenuItem,导入销售退货表数据ToolStripMenuItem,系统管理ToolStripMenuItem,用户管理ToolStripMenuItem,角色管理ToolStripMenuItem,权限管理ToolStripMenuItem,用户角色管理ToolStripMenuItem,角色权限管理ToolStripMenuItem,修改密码ToolStripMenuItem,退出系统ToolStripMenuItem
            };

            //获取用户权限
            IList<Actions> actions = getActions();
            //根据权限，设置相应菜单项为有效
            foreach (Actions action in actions)
            {
                //根据菜单对象的属性 Text 中是否包含 ActionsName（角色名称）来判断
                foreach (ToolStripMenuItem menu in menus)
                {
                    if (menu.Text.Trim().IndexOf(action.ActionName.Trim()) >= 0)
                    {
                        menu.Enabled = true;
                        break;
                    }
                }
            }
        }

        //4 遍历菜单项，将所有的菜单项设置为无效
        private void CheckMenu(MenuStrip Menu)
        {
            foreach (ToolStripMenuItem n in Menu.Items)
            {
                CheckSubMenu(n);
            }
        }
        //利用递归法，依次将菜单项设置为无效
        private void CheckSubMenu(ToolStripMenuItem menuItem)
        {

            menuItem.Enabled = false;
            for (int i = 0; i < menuItem.DropDownItems.Count; i++)
            {
                if (menuItem.DropDownItems[i] is ToolStripSeparator)
                {
                    continue;
                }
                else
                {
                    CheckSubMenu((ToolStripMenuItem)menuItem.DropDownItems[i]);
                }
            }
        }

        //窗体载入 load 事件
        private void frmMain_Load(object sender, EventArgs e)
        {
            //初始化状态栏，显示时间
            this.tsslDate.Text = "当前时间：" + DateTime.Now.Year.ToString() + " 年 " + DateTime.Now.Month.ToString() + " 月 " + DateTime.Now.Day.ToString() + " 日 " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            //遍历菜单项，将所有菜单项设置为无效
            CheckMenu(msMain);
            //根据权限，设置相应菜单项为有效
            showMenu();
        }

        //以下为 系统管理菜单 的子菜单

        // 一 资料管理菜单子项
        private void 销售员资料管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmSalesMan")) return;
            showChildForm<frmSalesMan>();
        }

        private void 客户资料管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmCustomer")) return;
            showChildForm<frmCustomer>();
        }

        private void 商品资料管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmProduct")) return;
            showChildForm<frmProduct>();
        }

        private void 供应商资料管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmSupplier")) return;
            showChildForm<frmSupplier>();
        }
        //二 采购管理菜单子项
        private void 编辑采购单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmPurchaseList")) return;
            showChildForm<frmPurchaseList>();
        }

        private void 编辑采购退货单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmRetPurchaseList")) return;
            showChildForm<frmRetPurchaseList>();
        }

        private void 浏览采购信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmPurchaseBrow")) return;
            showChildForm<frmPurchaseBrow>();
        }

        private void 浏览采购退货信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmRetPurchaseBrow")) return;
            showChildForm<frmRetPurchaseBrow>();
        }
        //三 销售管理菜单子项
        private void 编辑销售单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmDeliveryList")) return;
            showChildForm<frmDeliveryList>();
        }

        private void 编辑销售退货单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmRetDeliveryList")) return;
            showChildForm<frmRetDeliveryList>();
        }

        private void 浏览销售信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmDeliveryBrow")) return;
            showChildForm<frmDeliveryBrow>();
        }

        private void 浏览销售退货信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmRetDeliveryBrow")) return;
            showChildForm<frmRetDeliveryBrow>();
        }
        //四 统计查询菜单子项
        private void 供应商交易记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmSupplierBusiReport")) return;
            showChildForm<frmSupplierBusiReport>();
        }

        private void 客户交易记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmCustomerBusinessRecord")) return;
            showChildForm<frmCustomerBusinessRecord>();
        }

        private void 库存查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmStockQuery")) return;
            showChildForm<frmStockQuery>();
        }

        private void 采购汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmPurchaseGather")) return;
            showChildForm<frmPurchaseGather>();
        }

        //五 数据管理菜单子项
        private void 完整备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmBackup")) return;
            showChildForm<frmBackup>();
        }

        private void 完整备份还原数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmRestore")) return;
            showChildForm<frmRestore>();
        }

        //六 用户管理菜单子项
        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmUser")) return;
            frmUser frmuser = new frmUser();
            frmuser.WindowState = FormWindowState.Normal;
            frmuser.StartPosition = FormStartPosition.CenterParent;
            frmuser.MdiParent = this;
            frmuser.Show();
        }

        private void 角色管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetInstanceState("frmRole")) return;
            frmRole frmrole = new frmRole();
            frmrole.WindowState = FormWindowState.Normal;
            frmrole.StartPosition = FormStartPosition.CenterParent;
            frmrole.MdiParent = this;
            frmrole.Show();
        }

        private void 权限管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //（应用泛型和反射机制）显示子窗体
            if (GetInstanceState("frmAction")) return;
            showChildForm<frmAction>();
        }

        private void 用户角色管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //（应用泛型和反射机制）显示子窗体
            if (GetInstanceState("frmUser_Role")) return;
            showChildForm<frmUser_Role>();
        }

        private void 角色权限管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //（应用基类）显示子窗体
            if (GetInstanceState("frmRole_Action")) return;
            //showChildForm(new frmRole_Action());     //用复选框列表控件
            showChildForm(new frmRole_Action_New());   //用权限树
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //（应用基类）显示子窗体
            if (GetInstanceState("frmModifyPwd")) return;
            showChildForm(new frmModifyPwd(user));
        }

        //七 退出菜单
        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //八 关闭将要窗体时，发生
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "软件提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        //关闭窗体时，发生
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            this.Dispose();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
