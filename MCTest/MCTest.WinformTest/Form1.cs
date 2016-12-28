using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MaterialSkin.Controls;

namespace MCTest.WinformTest
{
    public partial class frmMain : Form
    {
        BindingList<InterestCalc> calcBl = new BindingList<InterestCalc>(); 
        BindingList<DateCalc> calcDate = new BindingList<DateCalc>(); 
        public frmMain()
        {
            InitializeComponent();
        }
         

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                notifyIcon1.Visible = true; //托盘图标隐藏
            }
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon1.Visible = true;//展示出notifyicon控件
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true; //弹出MainForm
                this.WindowState = FormWindowState.Normal; //还原窗体 
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) //关闭事件
        {
            DialogResult result;
            result = MessageBox.Show("确定退出吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {

                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.AllowUserToAddRows = true;
            dataGridView3.DataSource = calcBl;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.AllowUserToAddRows = true;
            dataGridView2.DataSource = calcDate;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < calcBl.Count; i++)
            {
                calcBl[i].Calc();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.Columns[e.ColumnIndex].Name == "colCalc")
            {
                calcBl[e.RowIndex].Calc();
                dataGridView3.Refresh();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "colCalcD")
            {
                calcDate[e.RowIndex].Calc();
                dataGridView2.Refresh();
            }
            txtResult.Text = calcDate[e.RowIndex].TxtResult;  
            monthCalendar1.SelectionRange = new SelectionRange(calcDate[e.RowIndex].LowerSelectedDate, calcDate[e.RowIndex].UpperSelectedDate);
            monthCalendar1.BoldedDates = calcDate[e.RowIndex].SelectedDate.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = !monthCalendar1.Visible;
        }
    }
}
