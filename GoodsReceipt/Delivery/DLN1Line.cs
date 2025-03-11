using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery
{
    public class DLN1Line
    {
        public string Series { get; set; }
        public string DocNo { get; set; }
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
        public int BaseEntry { get; set; }
        public int BaseLine { get; set; }
        public int BaseType { get; set; }
    }
}
