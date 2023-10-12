using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_4._5
{
    public class Products : Items
    {
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}
