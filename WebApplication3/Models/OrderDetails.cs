using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Models
{
    public class OrderDetails
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
    }
}