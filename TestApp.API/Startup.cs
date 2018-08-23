namespace TestApp.API {
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using TestApp.API.Data.Context;
    using TestApp.API.Data.Repositories.Interfaces;
    using TestApp.API.Data.Repositories;
    using TestApp.API.Domain.Base;
    using TestApp.API.Domain;

    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.   
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);
            // connection string for dbContext
            services.AddDbContext<TestAppDataContext> (x => x.UseSqlServer (Configuration.GetConnectionString ("TestAppEntitiesDev")));

            // Repository wrapper
            services.AddScoped<IDataService, DataService> ();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository> ();

            // Set the Authentication mechanism used
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (Configuration.GetSection ("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // app.UseHsts();
            }

            // Allow cross origin for the client to access the api. Should be above the NVC section below
            app.UseCors (x => x.AllowAnyHeader ().AllowAnyMethod ().AllowAnyOrigin ().AllowCredentials ());

            app.UseAuthentication();

            // app.UseHttpsRedirection();
            app.UseMvc ();
        }
    }
}