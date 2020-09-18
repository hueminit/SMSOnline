using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public string ProductName { set; get; }
        public decimal ProductPrice { set; get; }
        public int ProductQuantity { set; get; }
    }
}
