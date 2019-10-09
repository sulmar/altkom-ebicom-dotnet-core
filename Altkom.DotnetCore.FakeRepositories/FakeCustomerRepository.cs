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

        private IQueryable<Customer> customers => entities.Where(c => !c.IsRemoved).AsQueryable();

        public ICollection<Customer> Get(CustomerSearchCriteria criteria)
        {
            var results = customers;

            if (!string.IsNullOrEmpty(criteria.City))
                results = results.Where(c => c.FirstName == criteria.City);

            if (!string.IsNullOrEmpty(criteria.Street))
                results = results.Where(c => c.FirstName == criteria.Street);

            return customers.ToList();
        }

        public override void Remove(int id)
        {
            Customer customer = Get(id);
            customer.IsRemoved = true;
        }

        public override bool IsExists(int key)
        {
            return customers.Any(c => c.Id == key);
        }

        public bool TryAthorize(string username, string password, out Customer customer)
        {
            customer = customers.SingleOrDefault(c => c.UserName == username && c.HashPassword == password);

            return customer != null;
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
