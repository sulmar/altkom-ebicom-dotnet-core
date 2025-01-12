﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Altkom.DotnetCore.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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

            app.UseOwin(pipeline => pipeline(environment => OwinHandler));


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

            // Przykład zastosowania routera

            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet("/api/orders/{id:int}",
                request => request.Response.WriteAsync($"Order id {request.GetRouteValue("id")}"));

            routeBuilder.MapPost("/api/orders", request => request.Response.WriteAsync("Created"));
            IRouter router = routeBuilder.Build();
            app.UseRouter(router);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        // http://owin.org/spec/spec/owin-1.0.0.html
        private async Task OwinHandler(IDictionary<string, object> environment)
        {
            string requestMethod = (string) environment["owin.RequestMethod"];
            string requestPath = (string)environment["owin.RequestPath"];

            Stream responseStream = (Stream)environment["owin.ResponseBody"];
            string responseText = "{\"FirstName\":\"Marcin\"}";

            var responseHeaders = (IDictionary<string, string[]>)environment["owin.ResponseHeaders"];
            responseHeaders["Content-Type"] = new string[] { "application/json" };

            byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

            await responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);


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
