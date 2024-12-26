using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }

        [Display(Name = "Current Date")]
        [Column(TypeName = "datetime")]
        public DateTime CurrentDate { get; set; }
        public int stock { get; set; }
    }
}
