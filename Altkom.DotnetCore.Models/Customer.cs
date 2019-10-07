using System;

namespace Altkom.DotnetCore.Models
{

    public class Customer : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsRemoved { get; set; }
    }
}
