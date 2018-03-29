using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Egrower.Infrastructure.Aggregates;
using Egrower.Infrastructure.DAL;
using Egrower.Infrastructure.DTO;
using Egrower.Infrastructure.Factories;
using Egrower.Infrastructure.Factories.Interfaces;
using Egrower.Infrastructure.Repositories;
using EGrower.Core.Domain;
using EGrower.Core.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EGrower.WebApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ();

            services.AddMvc()
                .AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);

            services.AddScoped<IEmailClientAggregate,EmailClientAggregate>();
            services.AddScoped<IEmailClientFactory, EmailClientFactory>();
            services.AddScoped<IEmailMessageRepository, EmailMessageRepository>();
            services.AddScoped<IAtachmentRepository, AtachmentRepository>();

            services.AddDbContext<EGrowerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EGrowerDatabase"),
                b => b.MigrationsAssembly("Egrower.Infrastructure")));

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<EmailMessage, EmailMessageDTO>();
            });
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