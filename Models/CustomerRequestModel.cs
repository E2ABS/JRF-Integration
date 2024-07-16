// Models/CustomerRequestModel.cs
using System.Collections.Generic;
namespace YourNamespace.Models
{
    public class CustomerRequestModel
    {
        public string? Series { get; set; } // Code
        public string? CardType { get; set; } // Code
        public string? CardName { get; set; } // Name
        public string? Address { get; set; } // Address
        public string? GroupCode { get; set; } // Group
        public List<ContactPersonModel>? ContactEmployees { get; set; } // Contact Persons
        public string? Notes { get; set; } // Remarks
        public string? CardForeignName { get; set; } // Foreign Name
        public string? PayTermsGrpCode { get; set; } // Payment Terms
        public string? Cellular { get; set; } // Mobile Phone
        public string? EmailAddress { get; set; } // Email
        public string? City { get; set; } // City
    }

    public class ContactPersonModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? MobilePhone { get; set; }
        public string? E_Mail { get; set; } // Email
        public string? Remarks1 { get; set; } // Remarks
    }
}
