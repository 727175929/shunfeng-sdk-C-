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
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            test();
        }

        public void test()
        {
            string url = "https://open-sbox.sf-express.com/rest/v1.0/order/access_token/BAEF8CF266CE4691AB0EA0BB2B6E3706/sf_appid/00037521/sf_appkey/21662E074E84B37EB4DBA0F89F9803AA";
            MessageReq<OrderReqDto> req = new MessageReq<OrderReqDto>();
            HeadMessageReq req2 = new HeadMessageReq{
                transType = 200,
                transMessageId = "201409040916141688"
            };
            req.head = req2;
            OrderReqDto dto = new OrderReqDto
            {
                orderId = "201409051418379991429499",
                expressType = 2,
                payMethod = 1,
                needReturnTrackingNo = 0,
                isDoCall = 1,
                isGenBillNo = 1,
                custId = "7552732920",
                payArea = "755CQ",
                sendStartTime = "2014-4-24 09:30:00",
                remark = "易碎物品，小心轻放"
            };
            DeliverConsigneeInfoDto dto2 = new DeliverConsigneeInfoDto
            {
                address = "上地",
                city = "朝阳",
                company = "京东",
                contact = "李四",
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
                country = "中国",
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
            AddedServiceDto item = new AddedServiceDto
            {
                name = "COD",
                value = "20000"
            };
            list.Add(item);
            AddedServiceDto dot6 = new AddedServiceDto
            {
                name = "CUSTID",
                value = "7552732920"
            };
            list.Add(dot6);
            dto.deliverInfo = dto2;
            dto.consigneeInfo = dto3;
            dto.cargoInfo = dto4;
            dto.addedServices = list;
            req.body = dto;
            MessageResp<OrderRespDto> resp = OrderTools.order(url, req);
        }    //模拟下单

        public void testOrderQuery()
        {
            string url = "https://open-sbox.sf-express.com/rest/v1.0/order/query/access_token/6ECFF3C074A239BC28EF97D9627229B9/sf_appid/00037521/sf_appkey/21662E074E84B37EB4DBA0F89F9803AA";
            MessageReq<OrderQueryReqDto> req = new MessageReq<OrderQueryReqDto>();
            HeadMessageReq req2 = new HeadMessageReq
            {
                transType = 0xcb,
                transMessageId = "201409040916141689"
            };
            req.head = req2;
            OrderQueryReqDto dto = new OrderQueryReqDto
            {
                orderId = "201409051418379991429499"
            };
            req.body = dto;
            MessageResp<OrderQueryRespDto> resp = OrderTools.orderQuery(url, req);
            richTextBox1.AppendText("message:" + resp.head.message + "\n");
            richTextBox1.AppendText("transMessageId(流水号):" + resp.head.transMessageId + "\n");
            richTextBox1.AppendText("transType(处理类型):" + resp.head.transType + "\n");
            richTextBox1.AppendText("code（状态码）:" + resp.head.code + "\n");
            richTextBox1.AppendText("resp.body:" + resp.body + "\n");
        }

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
            richTextBox1.AppendText("accessToken:" + resp.body.accessToken + "\n");
            richTextBox1.AppendText("refreshToken:" + resp.body.refreshToken + "\n");
            richTextBox1.AppendText("code:" + resp.head.code + "\n");
            richTextBox1.AppendText("message:" + resp.head.message + "\n");

        }
        private void button2_Click(object sender, EventArgs e)
        {
            testOrderQuery();//模拟查询单子
        }

        private void button3_Click(object sender, EventArgs e)
        {
            testgetAccessToken();
        }   
    }
}
 