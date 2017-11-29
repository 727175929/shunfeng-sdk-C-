namespace com.sf.openapi.common.entity
{
    using System;

    public class MessageResp<T>
    {
        public T body;
        public HeadMessageResp head;
    }
}

