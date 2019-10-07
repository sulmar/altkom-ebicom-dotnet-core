using Altkom.DotnetCore.Models;
using Bogus;
using System;

namespace Altkom.DotnetCore.Fakers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true);
            // UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Email, (f, c) => $"{c.FirstName} {c.LastName}@ebicom.pl");
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.3f));

        }
    }
}
