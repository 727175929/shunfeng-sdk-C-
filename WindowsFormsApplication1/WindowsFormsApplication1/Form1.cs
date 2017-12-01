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
using com.sf.openapi.security.sample.dto;
using com.sf.openapi.common.entity;
using com.sf.openapi.express.sample.waybill.dto;
using com.sf.openapi.express.sample.waybill.tools;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public static string Form1Value; //  全局变量传给其他界面的有效值

        int key1 = 0, key2 = 0, key3 = 0, key4 = 0, key5 = 0, key6 = 0, key7 = 0, key8 = 0;

        String accesstoken = "BAEF8CF266CE4691AB0EA0BB2B6E3706";
        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "标准快递";
            comboBox2.Text = "收方付";
            comboBox3.Text = "不需要上门取件";
            comboBox4.Text = "申请";
            comboBox5.Text = "生成";
            comboBox6.Text = "不需要";
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
                orderId = textBox1.Text.ToString(),
                expressType = 1,
                payMethod = 1,
                needReturnTrackingNo = 0,
                isDoCall = 1,
                isGenBillNo = 1,
                isGenEletricPic = 1,
                payArea = textBox3.Text.ToString(),
                custId = textBox2.Text.ToString(),
              // sendStartTime = dateTimePicker1.ToString(),
                sendStartTime = "2014-4-24 09:30:00",
                remark = textBox4.Text.ToString()
            };
            DeliverConsigneeInfoDto dto2 = new DeliverConsigneeInfoDto
            {
                address = textBox12.Text.ToString(),
                city = "深圳",
                company = textBox13.Text.ToString(),
                contact = textBox14.Text.ToString(),
                country = "南山区",
                province = textBox16.Text.ToString(),
                shipperCode = textBox17.Text.ToString(),
                tel = textBox15.Text.ToString(),
                mobile = textBox18.Text.ToString()
            };
            DeliverConsigneeInfoDto dto3 = new DeliverConsigneeInfoDto
            {
                address = textBox12.Text.ToString(),
                city = "深圳",
                company = textBox13.Text.ToString(),
                contact = textBox14.Text.ToString(),
                country = "南山区",
                province = textBox16.Text.ToString(),
                shipperCode = textBox17.Text.ToString(),
                tel = textBox15.Text.ToString(),
                mobile = textBox18.Text.ToString()
            };
            CargoInfoDto dto4 = new CargoInfoDto
            {
                parcelQuantity = Convert.ToInt32(textBox19.Text.ToString()),
                cargo = textBox20.Text.ToString(),
                cargoCount = textBox21.Text.ToString(),
                cargoUnit = textBox22.Text.ToString(),
                cargoWeight = textBox23.Text.ToString(),
                cargoAmount = textBox24.Text.ToString(),
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

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click_1(object sender, EventArgs e)
        {

        }

        private short GetexpressType()
        {
            if (comboBox1.Text.ToString().Equals("标准快递"))
                return 1;
            else if (comboBox1.Text.ToString().Equals("顺丰特惠"))
                return 2;
            else if (comboBox1.Text.ToString().Equals("电商特惠"))
                return 3;
            else if (comboBox1.Text.ToString().Equals("顺丰次晨"))
                return 5;
            else if (comboBox1.Text.ToString().Equals("顺丰即日"))
                return 6;
            else if (comboBox1.Text.ToString().Equals("电商速配"))
                return 7;
            else
                return 15;
        }

        private short GetpayMethod()
        {
            if (comboBox2.Text.ToString().Equals("寄方付"))
                return 1;
            else if (comboBox2.Text.ToString().Equals("收方付"))
                return 2;
            else
                return 3;
        }

        private short GetisDocall()
        {
            if (comboBox3.Text.ToString().Equals("不需要上门取件"))
                return 0;
            else 
                return 1;
        }

        private short GetisGenBillno()
        {
            if (comboBox4.Text.ToString().Equals("不申请"))
                return 0;
            else
                return 1;
        }

        private short GetisGenEletricPic()
        {
            if (comboBox5.Text.ToString().Equals("生成"))
                return 1;
            else
                return 0;
        }

        private short GetneedReturnTrackingNo()
        {
            if (comboBox5.Text.ToString().Equals("不需要"))
                return 0;
            else
                return 1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            short expressType = GetexpressType();  //快件产品默认代码1 标准快递
            short payMethod = GetpayMethod();  //付款方式默认代码2 收方付
            short isDocall = GetisDocall();   //是否下call（通知收派员上门取件）默认代码 0 不通知  
            short isGenBillno = GetisGenBillno(); //是否申请运单号 默认值1  申请【默认值】
            short isGenEletricPic = GetisGenEletricPic(); //是否生成电子运单图片 默认值1 生成
            short needReturnTrackingNo = GetneedReturnTrackingNo(); // 是否需要签回单号  默认值0 不需要
            
            String address = textBox5.Text.ToString();
            if (address.Equals(""))
            {
                address = "未填写";
            }
            String company = textBox6.Text.ToString();
            if (company.Equals(""))
            {
                company = "未填写";
            }
            String contact = textBox7.Text.ToString();
            if (contact.Equals(""))
            {
                contact = "未填写";
            }
            String tel = textBox8.Text.ToString();
            if (tel.Equals(""))
            {
                tel = "未填写";
            }



            OrderReqDto dto = new OrderReqDto
            {
                orderId = textBox1.Text.ToString(),
                expressType = expressType,
                payMethod = payMethod,
                needReturnTrackingNo = needReturnTrackingNo,
                isDoCall = isDocall,
                isGenBillNo = isGenBillno,
                isGenEletricPic = isGenEletricPic,
                payArea = textBox3.Text.ToString(),
                custId = textBox2.Text.ToString(),
                //sendStartTime = dateTimePicker1.ToString(),
                sendStartTime = "2014-4-24 09:30:00",
                remark = textBox4.Text.ToString()
            };
            DeliverConsigneeInfoDto dto2 = new DeliverConsigneeInfoDto
            {
                address = address,
                city = "北京市",
                company = company,
                contact = contact,
                country = "中国",
                province = textBox9.Text.ToString(),
                shipperCode = textBox10.Text.ToString(),
                tel = tel,
                mobile = textBox11.Text.ToString()
            };
            String tel_2 = textBox18.Text.ToString();
            if (tel_2.Equals(""))
            {
                tel_2 = "未填写";
            }
            DeliverConsigneeInfoDto dto3 = new DeliverConsigneeInfoDto
            {
                address = textBox12.Text.ToString(),
                city = "深圳",
                company = textBox13.Text.ToString(),
                contact = textBox14.Text.ToString(),
                country = "南山区",
                province = textBox16.Text.ToString(),
                shipperCode = textBox17.Text.ToString(),
                tel = tel_2,
                mobile = textBox18.Text.ToString()
            };
            CargoInfoDto dto4 = new CargoInfoDto
            {
                parcelQuantity = Convert.ToInt32(textBox19.Text.ToString()),
                cargo = textBox20.Text.ToString(),
                cargoCount = textBox21.Text.ToString(),
                cargoUnit = textBox22.Text.ToString(),
                cargoWeight = textBox23.Text.ToString(),
                cargoAmount = textBox24.Text.ToString(),
                cargoTotalWeight = 12
            };
            order_order(dto, dto2, dto3, dto4);
           // test();
        }      //下单

        public void order_order(OrderReqDto dto,DeliverConsigneeInfoDto dto2,DeliverConsigneeInfoDto dto3,CargoInfoDto dto4)
        {
            string url = "https://open-sbox.sf-express.com/rest/v1.0/order/access_token/" + accesstoken + "/sf_appid/00037521/sf_appkey/21662E074E84B37EB4DBA0F89F9803AA";
            MessageReq<OrderReqDto> req = new MessageReq<OrderReqDto>();
            HeadMessageReq req2 = new HeadMessageReq
            {
                transType = 200,
                transMessageId = GettransMessageId()
            };
            req.head = req2;
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
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
           /* if (textBox1.Text.ToString().Length == 0)
            {
                //MessageBox.Show(textBox1.Text.ToString());
                this.errorProvider1.SetError(this.textBox1, "不能为空");
            }
            else
            {
                key1 = 1;
                this.errorProvider1.Dispose();
            }*/
        }

        private void z(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString().Length == 0)
            {
                key1 = 0;
                //MessageBox.Show(textBox1.Text.ToString());
                this.errorProvider1.SetError(this.textBox1, "不能为空");
            }
            else
            {
                key1 = 1;
                this.errorProvider1.Dispose();
            }
            checkkey();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (textBox12.Text.ToString().Length == 0)
            {
                key3 = 0;
                //MessageBox.Show(textBox1.Text.ToString());
                this.errorProvider1.SetError(this.textBox12, "不能为空");
            }
            else
            {
                key3 = 1;
                this.errorProvider1.Dispose();
            }
            checkkey();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.ToString().Length == 0)
            {
                key2 = 0;
                //MessageBox.Show(textBox1.Text.ToString());
                this.errorProvider1.SetError(this.textBox2, "不能为空");
            }
            else
            {
                key2 = 1;
                this.errorProvider1.Dispose();
            }
            checkkey();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (textBox13.Text.ToString().Length == 0)
            {
                key4 = 0;
                //MessageBox.Show(textBox1.Text.ToString());
                this.errorProvider1.SetError(this.textBox13, "不能为空");
            }
            else
            {
                key4 = 1;
                this.errorProvider1.Dispose();
            }
            checkkey();
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (textBox14.Text.ToString().Length == 0)
            {
                key5 = 0;
                //MessageBox.Show(textBox1.Text.ToString());
                this.errorProvider1.SetError(this.textBox14, "不能为空");
            }
            else
            {
                key5 = 1;
                this.errorProvider1.Dispose();
            }
            checkkey();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (textBox15.Text.ToString().Length == 0)
            {
                key6 = 0;
                //MessageBox.Show(textBox1.Text.ToString());
                this.errorProvider1.SetError(this.textBox15, "不能为空");
            }
            else
            {
                key6 = 1;
                this.errorProvider1.Dispose();
            }
            checkkey();
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            if (textBox16.Text.ToString().Length == 0)
            {
                key7 = 0;
                //MessageBox.Show(textBox1.Text.ToString());
                this.errorProvider1.SetError(this.textBox16, "不能为空");
            }
            else
            {
                key7 = 1;
                this.errorProvider1.Dispose();
            }
            checkkey();
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            if (textBox20.Text.ToString().Length == 0)
            {
                key8 = 0;
                //MessageBox.Show(textBox1.Text.ToString());
                this.errorProvider1.SetError(this.textBox20, "不能为空");
            }
            else
            {
                key8 = 1;
                this.errorProvider1.Dispose();
            }
            checkkey();
        }

        private void checkkey()
        {
            key2 = 1; //暂时改为1
            int i = key1*key2*key3*key4*key5*key6*key7*key8;
            if (i == 1)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void testwaybill()
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
                orderId = textBox1.Text.ToString()
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
        }   //64转图片转码

        private void button2_Click_1(object sender, EventArgs e)
        {
            testwaybill(); //测试打印
        }
 
    }
}
 