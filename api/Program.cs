using CarMarketAnalysis.Configuration;
using CarMarketAnalysis.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CarMarketAnalysis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

            builder.Services.AddDbContext<DatabaseContext>((serviceCollection, options) =>
            {
                var settings = serviceCollection.GetRequiredService<IOptions<AppSettings>>().Value;
                options.UseSqlServer(settings.ConnectionStrings.DefaultConnection);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services and repositories

            builder.Services.AddAuthorization();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
