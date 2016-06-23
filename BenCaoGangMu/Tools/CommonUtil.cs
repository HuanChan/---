using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Data.SqlClient;

//此类用于处理错误
//本项目把错误分为系统错误和数据库错误，并对这两种错误信息的格式进行了定制。
namespace BenCaoGangMu.Tools
{
    /// <summary>
    /// 错误信息的处理     common:常见的；util：利用。
    /// </summary>
    class CommonUtil
    {
        // 1 计算两个数的乘积
        public static decimal getTotal(string price, string amount)
        {
            return decimal.Parse(price) * decimal.Parse(amount);
        }
        // 2 取得格式后的数据库错误
        public static string getDBErrorMessage(SqlException e)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("数据库操作错误:错误号 ").Append(e.ErrorCode.ToString() + "\n");
            msg.Append(" 错误信息: ").Append(e.Message);
            return msg.ToString();
        }
        // 3 取得格式后的系统错误
        public static string getSysErrorMessage(Exception e)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(" 系统错误信息: ").Append(e.Message);
            return msg.ToString();
        }
        //4-1
        /// <summary>
        /// 将DataGridView的全部记录导出到Excel
        /// </summary>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="strTitle">导出的Excel标题</param>
        public static void DataGridViewExportToExcel1(DataGridView dgv, string strTitle)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.FileName = strTitle + ".xls";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) //导出时，点击【取消】按钮
            {
                return;
            }

            Stream myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));

            string strHeaderText = "";

            try
            {
                //写标题
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        strHeaderText += "\t";
                    }

                    strHeaderText += dgv.Columns[i].HeaderText;
                }

                sw.WriteLine(strHeaderText);

                //写内容
                string strItemValue = "";

                for (int j = 0; j < dgv.RowCount; j++)
                {
                    strItemValue = "";

                    for (int k = 0; k < dgv.ColumnCount; k++)
                    {
                        if (k > 0)
                        {
                            strItemValue += "\t";
                        }

                        strItemValue += dgv.Rows[j].Cells[k].Value.ToString();
                    }

                    sw.WriteLine(strItemValue); //把dgv的每一行的信息写为sw的每一行
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "软件提示");
                throw ex;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
        //4-2
        /// <summary>
        /// 将DataGridView中的数据导出到Excel，本函数可以排除所有的复选框列
        /// </summary>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="strTitle">导出的Excel标题</param>
        public static void DataGridViewExportToExcel2(DataGridView dgv, string strTitle)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.FileName = strTitle + ".xls";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) //导出时，点击【取消】按钮
            {
                return;
            }

            Stream myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));

            string strHeaderText = "";

            try
            {  //写列标题，请注意：i=0 从第一列开始,i=1 从第二列开始，依次类推                
                for (int i = 0; i < dgv.ColumnCount; i++)
                {   //去掉类型为复选框的单元格
                    if (dgv.Columns[i].GetType().FullName == "System.Windows.Forms.DataGridViewCheckBoxColumn")
                        continue;
                    //if (flag)
                    //{
                    //    strHeaderText += "\t";
                    //}
                    strHeaderText += dgv.Columns[i].HeaderText + "\t";
                    strHeaderText.Remove(strHeaderText.LastIndexOf("\t"), 1);
                }


                sw.WriteLine(strHeaderText);

                //写内容
                string strItemValue = "";

                for (int j = 0; j < dgv.RowCount; j++)
                {
                    strItemValue = "";
                    //请注意：k=0 从第一列开始,k=1 从第二列开始，依次类推
                    for (int k = 0; k < dgv.ColumnCount; k++)
                    {   //去掉类型为复选框的单元格
                        if (dgv.Columns[k].GetType().FullName == "System.Windows.Forms.DataGridViewCheckBoxColumn")
                            continue;
                        //if (k > 0)
                        //{
                        //    strItemValue += "\t";
                        //}

                        strItemValue += dgv.Rows[j].Cells[k].Value.ToString() + "\t";
                    }
                    strItemValue.Remove(strItemValue.LastIndexOf("\t"), 1);

                    sw.WriteLine(strItemValue); //把dgv的每一行的信息写为sw的每一行
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "软件提示");
                throw ex;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
        //4-3
        /// <summary>
        /// 功能：将DataGridView中选中的记录导出到Excel中:本函数要求 DataGridView的第一列须为复选框，
        /// </summary>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="strTitle">导出的Excel标题</param>
        public static void DataGridViewExportToExcel(DataGridView dgv, string strTitle)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.FileName = strTitle + ".xls";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) //导出时，点击【取消】按钮
            {
                return;
            }

            Stream myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));

            string strHeaderText = "";

            try
            {
                //写标题
                for (int i = 1; i < dgv.ColumnCount; i++)
                {
                    if (i > 1)
                    {
                        strHeaderText += "\t";
                    }

                    strHeaderText += dgv.Columns[i].HeaderText;
                }

                sw.WriteLine(strHeaderText);

                //写内容
                string strItemValue = "";

                for (int j = 0; j < dgv.RowCount; j++)
                {
                    if ((bool)dgv.Rows[j].Cells[0].EditedFormattedValue != true)
                    {
                        continue;
                    }
                    strItemValue = "";
                    for (int k = 1; k < dgv.ColumnCount; k++)
                    {
                        if (k > 1)
                        {
                            strItemValue += "\t";
                        }

                        strItemValue += dgv.Rows[j].Cells[k].Value.ToString();
                    }

                    sw.WriteLine(strItemValue); //把dgv的每一行的信息写为sw的每一行
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "软件提示");
                throw ex;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
    }
}
