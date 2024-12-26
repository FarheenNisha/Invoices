using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        //public List<InvoiceItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        //public InvoiceStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public enum InvoiceStatus
    {
        Draft,
        Sent,
        Paid,
        Overdue
    }
}
