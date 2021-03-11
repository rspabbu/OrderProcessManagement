using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderProcessManagement.DomainServices;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace OrderProcessManagement
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
            services.AddControllers();
            services.AddSingleton<IManageInventory,ManageInventory>();
            services.AddScoped<IProcessPaymentService,ProcessPaymentService>();
            services.AddScoped<IOrderProcessingService,OrderProcessingService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddHttpClient();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Version="v1",
                    Title= "Order Process Management Api",
                    Description= "Order Process Management Api"

            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerUI(x => { x.DocumentTitle = "Order processing Api"; x.RoutePrefix = string.Empty; x.SwaggerEndpoint ( "/swagger/v1/swagger.json","v1"); });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
