// Models/CustomerRequestModel.cs
namespace YourNamespace.Models
{
    public class CustomerRequestModel
    {
        public string? CardCode { get; set; } // Code
        public string? CardType { get; set; } // Code
        public string? CardName { get; set; } // Name
        public string? Address { get; set; } // Address
        public string? GroupCode { get; set; } // Group
        public string? ContactPerson { get; set; } // Contact Person
        public string? Notes { get; set; } // Remarks
        public string? CardForeignName { get; set; } // Foreign Name
        public string? PayTermsGrpCode { get; set; } // Payment Terms
        public string? Cellular { get; set; } // Mobile Phone
        public string? EmailAddress { get; set; } // Email
    }
}
