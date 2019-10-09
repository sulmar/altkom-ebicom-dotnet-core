using Altkom.DotnetCore.FakeRepositories;
using Altkom.DotnetCore.Fakers;
using Altkom.DotnetCore.IRepositories;
using Altkom.DotnetCore.Models;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Altkom.DotnetCore.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello .NET Core!");
          
            GetCustomersTest();

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();

        }

        // Przykład wstrzykiwania zależności w aplikacji konsolowej
        private static void GetCustomersTest()
        {
            // dotnet add package Microsoft.Extensions.DependencyInjection
            IServiceCollection services = new ServiceCollection();

            services
             .AddSingleton<ICustomerRepository, FakeCustomerRepository>()
             .AddSingleton<Faker<Customer>, CustomerFaker>()
             .AddSingleton<AddressFaker>();

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                ICustomerRepository customerRepository = serviceProvider.GetService<ICustomerRepository>();

                var customers = customerRepository.Get();

                foreach (Customer customer in customers)
                {
                    Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.IsRemoved}");
                }
            }
        }
    }
}
