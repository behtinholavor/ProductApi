using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace product.stock.api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            StockFactory._configuration = Configuration;
        }

        public IConfiguration Configuration { get; }        

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddDbContext<StockContext>(options =>
               options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")),
               contextLifetime: ServiceLifetime.Transient);

            services.AddIdentity<User, UserRole>()
                .AddDefaultTokenProviders();            

            services.AddScoped<StockService>();
            services.AddScoped<IStockRepository, StockRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Security:Token"])),
                 ClockSkew = TimeSpan.Zero
             });
            
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });

        }

        // This method gets called by the runtime. 
        //Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseSwagger();
            var swaggerUrl = "/camara-registro/swagger/v1/swagger.json";
#if DEBUG
            swaggerUrl = "/swagger/v1/swagger.json";
#endif
            app.UseSwaggerUI(c => c.SwaggerEndpoint(swaggerUrl, "DTI Full Stack - Product API"));

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
