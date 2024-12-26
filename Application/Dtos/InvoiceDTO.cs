using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        //public List<InvoiceItemDTO> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceStatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public enum InvoiceStatusDTO
    {
        Draft,
        Sent,
        Paid,
        Overdue
    }
}
