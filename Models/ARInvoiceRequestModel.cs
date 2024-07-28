namespace YourNamespace.Models
{
    public class ARInvoiceRequestModel
    {
        public string Series { get; set; }
        public string? CardCode { get; set; }
        public string? DocDate { get; set; }
        public string? DocDueDate { get; set; }
        public string? Comments { get; set; }
        public string? ReferenceNumber { get; set; }  // New field
        public string? PostingDate { get; set; }     // New field
        public string? DeliveryDate { get; set; }    // New field
     
      
        public string? TransportationCode { get; set; }    // New field
        public decimal DiscountAmount { get; set; }  // New field
        public ARInvoiceDocumentLine[]? DocumentLines { get; set; }
    }

    public class ARInvoiceDocumentLine
    {
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; } // New field
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public string? TaxCode { get; set; }         // New field
        public decimal Discount { get; set; }        // New field
        public decimal TotalNetAmount { get; set; }  // New field
        public string? UOMCode { get; set; }         // New field
        public string? FreeText { get; set; }
        public string? ProjectCode { get; set; }
    }
}
