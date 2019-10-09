using Altkom.DotnetCore.Models;
using Altkom.DotnetCore.Models.SearchCriterias;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Altkom.DotnetCore.IRepositories
{
    public interface ICustomerRepository : IEntityRepository<Customer, int>
    {
        // ICollection<Customer> Get(string city, string street, string country, string postcode);

        ICollection<Customer> Get(CustomerSearchCriteria criteria);
        bool TryAthorize(string username, string password, out Customer customer);
    }

}
