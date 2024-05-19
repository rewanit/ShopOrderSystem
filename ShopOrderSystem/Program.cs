
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShopOrderSystem.Data;
using ShopOrderSystem.Data.Interceptors;
using ShopOrderSystem.Data.Repositories;
using ShopOrderSystem.Data.Repositories.Interfaces;
using ShopOrderSystem.Services;
using ShopOrderSystem.Services.Interfaces;
using ShopOrderSystem.Utility;
using ShopOrderSystem.Utility.Auth;

namespace ShopOrderSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IOrderItemDetailRepository, OrderItemDetailRepository>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IProductTypeService, ProductTypeService>();

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddProblemDetails();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Shop Order System API",
                    Version = "v1",
                    Description = "API для системы заказов магазина"
                });

                options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    Description = "Basic auth",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Basic",
                    Type = SecuritySchemeType.Http
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }
                        },
                        new List<string>(){ }
                    }
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddLogging(x => x.AddConsole());

            builder.Services.AddDbContext<ShopOrderSystemDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("ShopOrderDB"))
            );
            builder.Services.AddMemoryCache();

            builder.Services.AddAuthentication("Basic")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            builder.Services.AddScoped<IAuthService, AuthService>();



            var app = builder.Build();

           
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop Order System API V1");
            });

            app.UseExceptionHandler();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
