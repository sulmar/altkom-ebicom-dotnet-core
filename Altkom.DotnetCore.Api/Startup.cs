using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Altkom.DotnetCore.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Altkom.DotnetCore.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseMiddleware<LoggerMiddleware>();

            app.UseLogger();

            app.Use(async (context, next) =>
            {
                Trace.WriteLine($"request 1 {context.Request.Method} {context.Request.Path}");

                await next.Invoke();

                Trace.WriteLine($"response 1 {context.Response.StatusCode}");
            });


            app.Use(async (context, next) =>
            {

                if (context.Request.Method == "POST")
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("brak dostepu");
                }
                else
                {
                    await next.Invoke();
                }

                Trace.WriteLine($"response 2{context.Response.StatusCode}");
            });

            // /dashboard

            app.Map("/dashboard", HandleDashboard);

            // tea
            // tea/green
            // tea/black

            app.Map("/tea", node =>
            {
                node.Map("/green", HandleGreeTea);
                node.Map("/black", HandleBlackTea);
                node.MapWhen(context => context.Request.Method == "POST", HandlerAddTea);
                node.Map(string.Empty, HandleTea);
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void HandlerAddTea(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.StatusCode = 201;
                await context.Response.WriteAsync("Tea was created");
            });
        }

        private void HandleTea(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("All tea!"));
        }

        private void HandleBlackTea(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Black tea!"));
        }

        private void HandleGreeTea(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Greeen tea!"));
        }

        private void HandleDashboard(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Dashboard!"));
        }
    }
}
