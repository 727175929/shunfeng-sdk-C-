using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using com.sf.openapi.express.sample.order.tools;
using com.sf.openapi.express.sample.security.tools;
using com.sf.openapi.common.util;
using com.sf.openapi.express.sample.order.dto;
using com.sf.openapi.common.entity;
using com.sf.openapi.security.sample.dto;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        String accesstoken = Form1.Form1Value;
        public Form2()
        {
            InitializeComponent();
        }

        private void 下单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 fr2 = new Form1();
            this.Hide();
            fr2.Show();
        }

        public static string GettransMessageId()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string t = DateTime.Now.ToString("yyyyMMdd");
            return t + Convert.ToInt64(ts.TotalSeconds).ToString();
        }   //获取流水号   流水号格式 20171130+10位（时间戳）

        public void OrderQuery(String OrderId_check)
        {
            string url = "https://open-sbox.sf-express.com/rest/v1.0/order/query/access_token/" + accesstoken + "/sf_appid/00037521/sf_appkey/21662E074E84B37EB4DBA0F89F9803AA";
            MessageReq<OrderQueryReqDto> req = new MessageReq<OrderQueryReqDto>();
            HeadMessageReq req2 = new HeadMessageReq
            {
                transType = 0xcb,
                transMessageId = GettransMessageId()
            };
            req.head = req2;
            OrderQueryReqDto dto = new OrderQueryReqDto
            {
                orderId = OrderId_check
            };
            req.body = dto;
            MessageResp<OrderQueryRespDto> resp = OrderTools.orderQuery(url, req);
            string newLine = "message:" + resp.head.message + "\n";
            //richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
           /* newLine = "transMessageId(流水号):" + resp.head.transMessageId + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = "transType(处理类型):" + resp.head.transType + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = "code（状态码）:" + resp.head.code + "\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);*/
            if (resp.head.code.Equals("EX_CODE_OPENAPI_0200"))
            {
                newLine = "resp.body.destcode(目的地代码):" + resp.body.destCode + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                newLine = "resp.body.originCode(原寄地代码):" + resp.body.originCode + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                newLine = "resp.body.orderId(订单号):" + resp.body.orderId + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                newLine = "resp.body.remark(留言):" + resp.body.remark + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                newLine = null;
            }
            else
            {
                newLine = "查询失败,错误信息:" + resp.head.message + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String orderid = textBox1.Text.ToString();
            OrderQuery(orderid);
        } //查询订单号
    }
}
