using Altkom.DotnetCore.IRepositories;
using Altkom.DotnetCore.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.DotnetCore.FakeRepositories
{
    public class FakeEntityRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        protected ICollection<TEntity> entities;

        public FakeEntityRepository(Faker<TEntity> entityFaker)
        {
            entities = entityFaker.Generate(100); 
        }

        public virtual void Add(TEntity entity) => entities.Add(entity);

        public virtual ICollection<TEntity> Get() => entities;

        public virtual TEntity Get(TKey id) => entities.SingleOrDefault(e => e.Id.Equals(id));

        public virtual void Remove(TKey id) => entities.Remove(Get(id));

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
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
