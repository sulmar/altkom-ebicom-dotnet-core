﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.DotnetCore.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Altkom.DotnetCore.SignalR
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes => routes.MapHub<CustomersHub>("/hubs/customers"));

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello Signal-R Hub!");
            });
        }
    }
}
