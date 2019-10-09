using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.DotnetCore.FakeRepositories;
using Altkom.DotnetCore.Fakers;
using Altkom.DotnetCore.IRepositories;
using Altkom.DotnetCore.Models;
using Altkom.DotnetCore.WebApi.Constraints;
using Altkom.DotnetCore.WebApi.Handlers;
using Bogus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;

namespace Altkom.DotnetCore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // services.AddDbContext<MyContext>();

            // .NET Core 2.1
            // services.AddDbContextPool<MyContext>();

            services
                .AddSingleton<ICustomerRepository, FakeCustomerRepository>()
                .AddSingleton<Faker<Customer>, CustomerFaker>()
                .AddSingleton<AddressFaker>();

            services.AddAuthentication("BasicAuthentication")
              .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // dotnet add package Microsoft.AspNetCore.Mvc.Formatters.Xml
            // curl -X GET  http://localhost:5000/api/customers -H "Accept: application/xml"
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; // Wyłączenie generowania wartości null w jsonie
                    options.SerializerSettings.Converters.Add(new StringEnumConverter(camelCaseText: true));  // Serializacja enum jako tekst
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // Zapobieganie cyklicznej serializacji

                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddXmlSerializerFormatters();

            services.Configure<RouteOptions>(options =>
                 options.ConstraintMap.Add("pesel", typeof(PeselRouteConstraint)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();


            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
