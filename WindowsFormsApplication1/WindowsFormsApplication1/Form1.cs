using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.sf.openapi.express.sample.order.tools;
using com.sf.openapi.express.sample.security.tools;
using com.sf.openapi.common.util;
using com.sf.openapi.express.sample.order.dto;
using com.sf.openapi.common.entity;
using com.sf.openapi.security.sample.dto;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public static string Form1Value; //  全局变量传给其他界面的有效值

        String accesstoken = "BAEF8CF266CE4691AB0EA0BB2B6E3706";
        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "标准快递";
            comboBox2.Text = "第三方付";
            comboBox3.Text = "不需要上门取件";
            comboBox4.Text = "申请";
            comboBox5.Text = "生成";
            
            testgetAccessToken();   //初始化获得AccessToken
            Form1Value = accesstoken;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            test();
        }

        public void test()
        {
            string url = "https://open-sbox.sf-express.com/rest/v1.0/order/access_token/" + accesstoken + "/sf_appid/00037521/sf_appkey/21662E074E84B37EB4DBA0F89F9803AA";
            MessageReq<OrderReqDto> req = new MessageReq<OrderReqDto>();
            HeadMessageReq req2 = new HeadMessageReq{
                transType = 200,
                transMessageId = GettransMessageId()
            };
            req.head = req2;
            OrderReqDto dto = new OrderReqDto
            {
                orderId = "OPEN20171130-1",
                expressType = 1,
                payMethod = 1,
                needReturnTrackingNo = 0,
                isDoCall = 1,
                isGenBillNo = 1,
                isGenEletricPic = 1,
                payArea = "",
                custId = "7550010173",
                sendStartTime = "2018-4-24 09:30:00",
                remark = "易碎物品，小心轻放"
            };
            DeliverConsigneeInfoDto dto2 = new DeliverConsigneeInfoDto
            {
                address = "神罗科技公司",
                city = "北京市",
                company = "神罗科技",
                contact = "李逍遥",
                country = "中国",
                province = "北京",
                shipperCode = "787564",
                tel = "010-95123669",
                mobile = "13612822894"
            };
            DeliverConsigneeInfoDto dto3 = new DeliverConsigneeInfoDto
            {
                address = "世界第一广场",
                city = "深圳",
                company = "顺丰",
                contact = "张三",
                country = "南山区",
                province = "广东",
                shipperCode = "518100",
                tel = "0755-33915561",
                mobile = "18588413321"
            };
            CargoInfoDto dto4 = new CargoInfoDto
            {
                parcelQuantity = 1,
                cargo = "手机",
                cargoCount = "1000",
                cargoUnit = "部",
                cargoWeight = "12",
                cargoAmount = "5200",
                cargoTotalWeight = 12
            };
            List<AddedServiceDto> list = new List<AddedServiceDto>();
            /*AddedServiceDto item = new AddedServiceDto
            {
                name = "COD",
                value = "20000"
            };
            list.Add(item);*/
            AddedServiceDto dot6 = new AddedServiceDto
            {/*
                name = "CUSTID",
                value = "7552732920"*/
            };
            list.Add(dot6);
            dto.deliverInfo = dto2;
            dto.consigneeInfo = dto3;
            dto.cargoInfo = dto4;
            dto.addedServices = list;
            req.body = dto;
            MessageResp<OrderRespDto> resp = OrderTools.order(url, req);
            string newLine = "信息:" + resp.head.message + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = null; 
        }    //模拟下单

        public void testOrderQuery()
        {
            string url = "https://open-sbox.sf-express.com/rest/v1.0/order/query/access_token/"+accesstoken+"/sf_appid/00037521/sf_appkey/21662E074E84B37EB4DBA0F89F9803AA";
            MessageReq<OrderQueryReqDto> req = new MessageReq<OrderQueryReqDto>();
            HeadMessageReq req2 = new HeadMessageReq
            {
                transType = 0xcb,
                transMessageId = GettransMessageId()
            };
            req.head = req2;
            OrderQueryReqDto dto = new OrderQueryReqDto
            {
                orderId = "OPEN20160701-011"
            };
            req.body = dto;
            MessageResp<OrderQueryRespDto> resp = OrderTools.orderQuery(url, req);
            string newLine = "message:" + resp.head.message + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = "transMessageId(流水号):" + resp.head.transMessageId + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = "transType(处理类型):" + resp.head.transType + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = "code（状态码）:" + resp.head.code + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = "resp.body:" + resp.body + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
        } //模拟查询

        public void testgetAccessToken()   //获得AccessToken
        {
            String url = "https://open-sbox.sf-express.com/public/v1.0/security/access_token/sf_appid/00037521/sf_appkey/21662E074E84B37EB4DBA0F89F9803AA";
            MessageReq<TokenReqDto> req = new MessageReq<TokenReqDto>();
            HeadMessageReq req2 = new HeadMessageReq()
            {
                transType = 0x12D,
                //transMessageId = "201404120000000001"
            };
            req.head = req2;
            /*TokenReqDto dto = new TokenReqDto
            {
            };
            req.body = dto;*/
            MessageResp<TokenRespDto> resp = SecurityTools.applyAccessToken(url, req);
            string newLine = "accessToken:" + resp.body.accessToken + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = "refreshToken:" + resp.body.refreshToken + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = "code:" + resp.head.code + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = "message:" + resp.head.message + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = null;   //清空newLine
            accesstoken = resp.body.accessToken;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            testOrderQuery();//模拟查询单子
        }

        private void button3_Click(object sender, EventArgs e)
        {
            testgetAccessToken();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
         {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8)) 
         {
             e.Handled = false;
         } 
         else 
         { 
             e.Handled = true;
         }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public static string GettransMessageId()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string t = DateTime.Now.ToString("yyyyMMdd");
            return t+Convert.ToInt64(ts.TotalSeconds).ToString();
        }   //获取流水号   流水号格式 20171130+10位（时间戳）

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void 下单ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 查询单号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            this.Hide();
            fr2.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
 