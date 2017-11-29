namespace com.sf.openapi.common.entity
{
    using System;

    public class HeadMessageResp : HeadMessageReq
    {
        public string code;
        public string message;

        public HeadMessageResp()
        {
        }

        public HeadMessageResp(string code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}

