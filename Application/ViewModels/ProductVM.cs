using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public DateTime CurrentDate { get; set; }
        public int stock { get; set; }
    }
}
