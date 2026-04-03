using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMS.Models.Main
{
    public class MainOrder
    {
        public DateTime OrdersDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public int TotalAmount { get; set; }
        public int PaidAmount { get; set; }
        public int DueAmount { get; set; }
        public int Discount { get; set; }
        public int GrandTotal { get; set; }
        public string PaymentStatus { get; set; }
    }
}
