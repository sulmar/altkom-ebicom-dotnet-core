using System;

namespace Altkom.DotnetCore.Models
{

    public class Customer : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsRemoved { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public Address HomeAddress { get; set; }
        public Address InvoiceAddress { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
    }
}
