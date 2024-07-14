// Models/IncomingPaymentRequestModel.cs
namespace YourNamespace.Models
{
    public class IncomingPaymentRequestModel
    {
        public string? CardCode { get; set; } // Customer code
        public string? DocDate { get; set; } // Payment date
        public decimal? CashSum { get; set; } // Total cash amount
        public IncomingPaymentInvoice[]? PaymentInvoices { get; set; } // Linked invoices
    }

    public class IncomingPaymentInvoice
    {
        public int? DocEntry { get; set; } // Invoice document entry
        public decimal? SumApplied { get; set; } // Amount applied to the invoice
    }
}
