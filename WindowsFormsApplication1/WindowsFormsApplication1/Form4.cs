using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        private string userName = "sa";
        private string password = "zhaoxuan965816";
        private string server = "127.0.0.1";  //ip地址
        private string connSrt = ""; //数据库连接字符串
        private string sql = "";   //sql语句
        private SqlConnection conn = null;  //声明一个数据库的链接
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void conndatabase()
        {
            connSrt = "Data Source=ZX-PC\\MSSQLSERVER1;Initial Catalog=Order;Persist Security Info=True;User ID=sa;Password=zhaoxuan960518";
            conn = new SqlConnection(connSrt);  //实例化链接
            conn.Open();   //打开链接

            sql = "select * from Order_Test";
            SqlCommand sc = new SqlCommand(sql, conn); //sql语句
            SqlDataAdapter sda = new SqlDataAdapter(sc); //数据适配器
            DataSet ds = new DataSet();           //DataSet 表示数据在内存中的缓存
            sda.Fill(ds, "Order_Test");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Order_Test";

            conn.Close();
            conn.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();
            conndatabase();
            init2();
        }

        private void init()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "colbtn";
            btn.HeaderText = "操作";
            btn.DefaultCellStyle.NullValue = "打印";
            dataGridView1.Columns.Add(btn);
        }

        private void init2()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "colbtn2";
            btn.HeaderText = "追踪";
            btn.DefaultCellStyle.NullValue = "查询";
            dataGridView1.Columns.Add(btn);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String s = null;
            if (dataGridView1.Columns[e.ColumnIndex].Name == "colbtn")
            {
                //占击按钮操作 e.rowindex
                s = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); 
                MessageBox.Show(s);
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "colbtn2")
            {
                //占击按钮操作 e.rowindex
                s = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                MessageBox.Show(s);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Insert();
        }

        public void Insert()
        {
            connSrt = "Data Source=ZX-PC\\MSSQLSERVER1;Initial Catalog=Order;Persist Security Info=True;User ID=sa;Password=zhaoxuan960518";
            conn = new SqlConnection(connSrt);  //实例化链接
            conn.Open();   //打开链接

            String orderid_insert = "789";
            String maino_insert = "";
            String companydel_insert = "";
            String addressdel_insert = "";
            String companycos_insert = "";
            String addresscos_insert = "";
            String cargo_insert = "";
            String paymethod_insert = "";

            string sql = "insert into Order_Test(orderid, maino, companydel, addressdel,companycos,addresscos,cargo,paymethod) values ('" +
                orderid_insert + "', '" + maino_insert + "', '" + companydel_insert + "', '" + addressdel_insert + "', '" + companycos_insert + "', '" + addresscos_insert + "', '" + cargo_insert + "','" + paymethod_insert + "')";
            SqlCommand sc = new SqlCommand(sql, conn); //sql语句
            sc.ExecuteReader();

            conn.Close();
            conn.Dispose();
        }

    }
}
