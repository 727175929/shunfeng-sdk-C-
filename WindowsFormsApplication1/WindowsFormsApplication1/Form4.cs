using com.sf.openapi.common.entity;
using com.sf.openapi.common.util;
using com.sf.openapi.express.sample.order.dto;
using com.sf.openapi.express.sample.order.tools;
using com.sf.openapi.express.sample.route.dto;
using com.sf.openapi.express.sample.route.tools;
using com.sf.openapi.express.sample.security.tools;
using com.sf.openapi.security.sample.dto;
using com.sf.openapi.express.sample.waybill.dto;
using com.sf.openapi.express.sample.waybill.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        String accesstoken = Form1.Form1Value;
        private string connSrt = ""; //数据库连接字符串
        private string sql = "";   //sql语句
        private SqlConnection conn = null;  //声明一个数据库的链接
        public Form4()
        {
            InitializeComponent();
            init();
            init2();
            conndatabase();
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
            dataGridView1.DataSource = null;
            conndatabase();
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
                this.Cursor = Cursors.WaitCursor;//等待
                //占击按钮操作 e.rowindex
                s = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                testwaybill(s);
                this.Cursor = Cursors.Default;//正常状态
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "colbtn2")
            {
                this.Cursor = Cursors.WaitCursor;//等待
                //占击按钮操作 e.rowindex
                s = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                RouteQuery(s);
                this.Cursor = Cursors.Default;//正常状态
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Insert_test();
        }

        public void Insert_test()
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

        public void Insert(String orderid_insert, String maino_insert, String companydel_insert, String addressdel_insert, String companycos_insert, String addresscos_insert, String cargo_insert, String paymethod_insert)
        {
            connSrt = "Data Source=ZX-PC\\MSSQLSERVER1;Initial Catalog=Order;Persist Security Info=True;User ID=sa;Password=zhaoxuan960518";
            conn = new SqlConnection(connSrt);  //实例化链接
            conn.Open();   //打开链接

            string sql = "insert into Order_Test(orderid, maino, companydel, addressdel,companycos,addresscos,cargo,paymethod) values ('" +
                orderid_insert + "', '" + maino_insert + "', '" + companydel_insert + "', '" + addressdel_insert + "', '" + companycos_insert + "', '" + addresscos_insert + "', '" + cargo_insert + "','" + paymethod_insert + "')";
            SqlCommand sc = new SqlCommand(sql, conn); //sql语句
            sc.ExecuteReader();

            conn.Close();
            conn.Dispose();
        }

        private void RouteQuery(String id)
        {
            string url = "https://open-sbox.sf-express.com/rest/v1.0/route/query/access_token/" + accesstoken + "/sf_appid/00037521/sf_appkey/21662E074E84B37EB4DBA0F89F9803AA";
            MessageReq<RouteReqDto> req = new MessageReq<RouteReqDto>();
            HeadMessageReq req2 = new HeadMessageReq
            {
                transType = 0x1f5,
                transMessageId = GettransMessageId()
            };
            req.head = req2;
            RouteReqDto dto = new RouteReqDto
            {
                methodType = 2,
                trackingType = 1,
                trackingNumber = id
            };
            req.body = dto;
            MessageResp<List<RouteRespDto>> resp = RouteTools.routeQuery(url, req);
            string newLine = "订单追踪message:" + resp.head.message + "\n";
            //richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            if (resp.head.code.Equals("EX_CODE_OPENAPI_0200"))
            {
                for (int i = 0; i < resp.body.Count; i++)
                {
                    newLine = "{\n";
                    richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                    newLine = "     快递接收地址:" + resp.body[i].acceptAddress + "\n";
                    richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                    newLine = "     快递接收时间:" + resp.body[i].acceptTime + "\n";
                    richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                    newLine = "     客户订单号:" + resp.body[i].orderId + "\n";
                    richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                    newLine = "     快递运单号:" + resp.body[i].mailNo + "\n";
                    richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                    newLine = "     操作码:" + resp.body[i].opcode + "\n";
                    richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                    newLine = "     备注:" + resp.body[i].remark + "\n";
                    richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                    newLine = "}\n";
                    richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                }
            }
            else
            {
                newLine = "订单追踪message:" + resp.head.message + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            }
        }
        public static string GettransMessageId()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string t = DateTime.Now.ToString("yyyyMMdd");
            return t + Convert.ToInt64(ts.TotalSeconds).ToString();
        }   //获取流水号   流水号格式 20171130+10位（时间戳）

        private void testwaybill(String id)
        {
            string url = "https://open-sbox.sf-express.com/rest/v1.0/waybill/image/access_token/" + accesstoken + "/sf_appid/00037521/sf_appkey/21662E074E84B37EB4DBA0F89F9803AA";
            MessageReq<WaybillReqDto> req = new MessageReq<WaybillReqDto>();
            HeadMessageReq req2 = new HeadMessageReq
            {
                transType = 0xcd,
                transMessageId = GettransMessageId()
            };
            req.head = req2;
            WaybillReqDto dto = new WaybillReqDto
            {
                orderId = id
            };
            req.body = dto;
            MessageResp<WaybillRespDto> resp = WaybillDownloadTools.waybillDownload(url, req);
            //resp.body.images{string[]};
            string newLine = "打印信息:" + resp.head.message + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            if (resp.head.code.Equals("EX_CODE_OPENAPI_0200"))
            {
                String[] a = resp.body.images;
                Base64ToImg(a[0]);
            }
        }
        public static Bitmap Base64ToImg(string strBase64)
        {
            String dirctory_bitmap = System.IO.Directory.GetCurrentDirectory().ToString() + GettransMessageId() + ".bmp";
            byte[] bt = Convert.FromBase64String(strBase64);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bt);
            Bitmap bitmap = new Bitmap(stream);
            bitmap.Save(dirctory_bitmap);
            return bitmap;
        } //64转图片转码

        private void 下单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 fr1 = new Form1();
            this.Hide();
            fr1.Show();
        }

        private void 查询单号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            this.Hide();
            fr2.Show();
        }   


    }
}
