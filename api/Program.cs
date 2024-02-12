using CarMarketAnalysis.Configuration;
using CarMarketAnalysis.Data;
using CarMarketAnalysis.Data.Repositories.BrandRepository;
using CarMarketAnalysis.Data.Repositories.CarRepository;
using CarMarketAnalysis.Data.Repositories.ModelRepository;
using CarMarketAnalysis.Data.Seeders;
using CarMarketAnalysis.Services.BrandService;
using CarMarketAnalysis.Services.ModelService;
using CarMarketAnalysis.Services.ScrapServices.Pages;
using CarMarketAnalysis.Services.ScrapServices.PlaywrightService;
using CarMarketAnalysis.Utilities.Sieve;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.Playwright;
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

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                });

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
            builder.Services.AddScoped<ICarRepository, CarRepository>();

            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IModelService, ModelService>();
            builder.Services.AddScoped<IPlaywrightService, PlaywrightService>();
            builder.Services.AddScoped<IPages, Pages>();

            builder.Services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();

            builder.Services.AddAuthorization();


            var app = builder.Build();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");

                if (app.Environment.IsDevelopment())
                {
                    options.RoutePrefix = "swagger";
                }
                else
                {
                    options.RoutePrefix = string.Empty;
                }
            });

            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            //if (app.Environment.IsDevelopment())
            //{
                var seeder = new Seeder(dbContext);
                int recordsToSeed = 10;
                seeder.Seed(recordsToSeed);

                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
