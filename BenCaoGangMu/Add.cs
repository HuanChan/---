using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BenCaoGangMu
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        private void Add_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Server=覃焕阐\MSSQL2016;database=BenCao;Integrated Security=True");
            try
            {
                conn.Open();
                if (checkedListBoxXingWei.CheckedItems.Count > 0)
                {
                    textBoxXingWei.Clear();
                }
                foreach (string item in checkedListBoxXingWei.CheckedItems)
                {
                    textBoxXingWei.Text = item.ToString();
                }
                for (int k = 0; k < checkedListBoxXingWei.Items.Count; k++)
                {
                    checkedListBoxXingWei.SetItemChecked(k, false);
                }

                //{n}：索引占位符，即格式项
                string sql = "INSERT INTO BenCao VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                sql = string.Format(sql,textBoxName.Text,textBoxBieMing.Text,textBoxXingWei.Text,textBoxGuiJing.Text,richTextBoxGongXiao.Text,richTextBoxZhuZhi.Text,richTextBoxZhuYi.Text);
                SqlCommand cmd = new SqlCommand(sql,conn);//使用new创建类，调用SqlCommand()构造函数
                cmd.CommandType = CommandType.Text;
                int m = cmd.ExecuteNonQuery();
                if (m != -1)
                {
                    MessageBox.Show("添加成功");
                }

            }
            catch { MessageBox.Show("没有添加成功"); }
            finally { conn.Close(); }
        }
    }
}
