namespace com.sf.openapi.common.util
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public class SFOpenClient
    {
        public static T2 doPost<T1, T2>(string url, T1 req)
        {
            //System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            HttpWebRequest request = HttpWebUtils.getHttpWebRequest(url, "POST");
            string s = JsonHelper.ObjectToJson(req);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            request.ContentLength = bytes.Length;
            request.GetRequestStream().Write(bytes, 0, bytes.Length);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return JsonHelper.JsonToObject<T2>(reader.ReadToEnd());
        }
    }
}

