﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class Sale
    {

        public int ID { get; set; }
        public DateTime Timestamp { get; set; }
        public int CustomerID { get; set; }
        public int RegisterID { get; set; }
        public int ProductID { get; set; }
        public double Amount { get; set; }
        public double TotalPrice { get; set; }

    }
}
