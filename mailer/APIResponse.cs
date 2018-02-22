using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Function
{
    public class APIResponse
    {
        public string Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string CorelationID { get; set; }

    }
}
