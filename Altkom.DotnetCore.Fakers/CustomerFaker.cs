using Altkom.DotnetCore.Models;
using Bogus;
using System;

namespace Altkom.DotnetCore.Fakers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker(AddressFaker addressFaker)
        {
            StrictMode(true);
            // UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Email, (f, c) => $"{c.FirstName} {c.LastName}@ebicom.pl");
            RuleFor(p => p.Phone, f => f.Phone.PhoneNumber());
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.3f));
            RuleFor(p => p.HomeAddress, f => addressFaker.Generate());
            RuleFor(p => p.InvoiceAddress, f => addressFaker.Generate());
            RuleFor(p => p.UserName, (f, c) => f.Person.UserName);
            RuleFor(p => p.HashPassword, f => "12345");
        }
    }
}
