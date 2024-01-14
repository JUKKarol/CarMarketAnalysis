using CarMarketAnalysis.Configuration;
using CarMarketAnalysis.Data;
using CarMarketAnalysis.Data.Repositories.BrandRepository;
using CarMarketAnalysis.Data.Repositories.CarRepository;
using CarMarketAnalysis.Data.Repositories.GenerationRepository;
using CarMarketAnalysis.Data.Repositories.ModelRepository;
using CarMarketAnalysis.Utilities.Sieve;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Sieve.Services;
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
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IModelRepository, ModelRepository>();
            builder.Services.AddScoped<IGenerationRepository, GenerationRepository>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();

            builder.Services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();

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
