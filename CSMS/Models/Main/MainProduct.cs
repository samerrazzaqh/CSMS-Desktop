using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMS.Models.Main
{
    public class MainProduct
    {
        public string ProductName { get; set; }
        public byte[] ProductImage { get; set; }
        public int ProductRate { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductBrand { get; set; }
        public string ProductCategory { get; set; }
        public string ProductStatus { get; set; }
    }
}
