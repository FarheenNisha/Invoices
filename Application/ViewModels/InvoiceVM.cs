using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class InvoiceVM
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        //public List<InvoiceItemVM> Items { get; set; }
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
