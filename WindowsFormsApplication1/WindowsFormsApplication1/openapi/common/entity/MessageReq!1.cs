namespace com.sf.openapi.common.entity
{
    using System;

    public class MessageReq<T>
    {
        public T body;
        public HeadMessageReq head;

        public T getBody()
        {
            return this.body;
        }
    }
}

