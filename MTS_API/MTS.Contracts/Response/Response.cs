using System;
using System.Collections.Generic;
using System.Text;

namespace MTS.Contracts.Response
{
    public class Response
    {
        public bool IsError { get; set; }
        public int ErrorCode { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
    }
}
