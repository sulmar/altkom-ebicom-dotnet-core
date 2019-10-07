using Altkom.DotnetCore.FakeRepositories;
using Altkom.DotnetCore.Fakers;
using Altkom.DotnetCore.IRepositories;
using Altkom.DotnetCore.Models;
using System;

namespace Altkom.DotnetCore.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello .NET Core!");

            ICustomerRepository customerRepository = new FakeCustomerRepository(new CustomerFaker());
           
            var customers = customerRepository.Get();

            foreach (Customer customer in customers)
            {

                Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.IsRemoved}");
            }

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();

        }
    }
}
