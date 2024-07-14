namespace YourNamespace.Models

{
    public class ARInvoiceRequestModel
    {
        public string? CardCode { get; set; }
        public string? DocDate { get; set; }
        public string? DocDueDate { get; set; }
        public string? Comments { get; set; }
        public ARInvoiceDocumentLine[]? DocumentLines { get; set; }
    }

    public class ARInvoiceDocumentLine
    {
        public string? ItemCode { get; set; }
        public string? Quantity { get; set; }
        public string? UnitPrice { get; set; }
        public string? FreeText { get; set; }
        public string? ProjectCode { get; set; }
    }
}
