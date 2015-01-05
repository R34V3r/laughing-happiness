using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.model.Web
{
    public class Logboek
    {
        public int RegisterId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string Stacktrace { get; set; }
    }
}
