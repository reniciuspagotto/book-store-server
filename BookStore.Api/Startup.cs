﻿using BookStore.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMVC();
            services.ConfigureDatabase(Configuration);
            services.ConfigureRepositories();
            services.ConfigureHandlers();
            services.ConfigureServices();
            services.ConfigureSwagger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataContext context)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.ConfigureMigrations(context);
            app.UseMvc();
        }
    }
}