﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egrower.Infrastructure.DAL;
using Egrower.Infrastructure.Factories;
using Egrower.Infrastructure.Factories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EGrower.WebApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ();
            services.AddScoped<IEmailFactory,EmailFactory>();

            services.AddDbContext<EGrowerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EGrowerDatabase"),
                b => b.MigrationsAssembly("Egrower.Infrastructure")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseMvc ();
        }
    }
}