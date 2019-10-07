using Altkom.DotnetCore.Fakers;
using Altkom.DotnetCore.IRepositories;
using Altkom.DotnetCore.Models;
using Altkom.DotnetCore.Models.SearchCriterias;
using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.DotnetCore.FakeRepositories
{

    public class FakeCustomerRepository : FakeEntityRepository<Customer, int>, ICustomerRepository
    {
        public FakeCustomerRepository(Faker<Customer> entityFaker) : base(entityFaker)
        {
        }

        public ICollection<Customer> Get(CustomerSearchCriteria criteria)
        {
            throw new System.NotImplementedException();
        }

        public override void Remove(int id)
        {
            Customer customer = Get(id);
            customer.IsRemoved = true;
        }
    }

    /*
    public class FakeCustomerRepository : ICustomerRepository
    {
        private ICollection<Customer> customers;


        public decimal Calculate(Customer customer)
        {
            return 100;
        }

        public FakeCustomerRepository(CustomerFaker customerFaker)
        {
            customers = customerFaker.Generate(100);
        }

        public void Add(Customer entity)
        {
            customers.Add(entity);
        }

        public ICollection<Customer> Get()
        {
            return customers;
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public void Remove(int id)
        {
            Customer customer = Get(id);
            customer.IsRemoved = true;
           // customers.Remove(Get(id));
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }

    */
}
