using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.UseCases.StudentWorkshop.AddStundentWorkshop;
using Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewStudentUseCases;
using Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginProfessor;
using Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginStudent;
using Ellp.Api.Application.UseCases.Workshops.AddWorkshops;
using Ellp.Api.Infra.SqlServer.Repository;
using Ellp.Api.Infra.SqlServer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
        builder.Configuration.AddEnvironmentVariables();

        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }

        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ListenAnyIP(5000);
        });

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Oficina 2", Version = "v1" });
            c.MapType<int?>(() => new OpenApiSchema { Type = "integer", Format = "int32", Nullable = true });
        });

        // Obter a string de conexão exclusivamente do appsettings.json
        var connectionString = "Server=tcp:db-oficina-a.database.windows.net,1433;Initial Catalog=dboOf;Persist Security Info=False;User ID=adminer;Password=Admin123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=true;Connection Timeout=90;";

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A string de conexão 'DbConnectionString' não foi encontrada no appsettings.json.");
        }

        builder.Services.AddDbContext<SqlServerDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
        builder.Services.AddScoped<IWorkshopRepository, WorkshopRepository>();
        builder.Services.AddScoped<IStudentWorkshopRepository, StudentWorkshopRepository>();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(GetLoginStudentUseCase).Assembly,
            typeof(GetLoginProfessorUseCase).Assembly,
            typeof(AddNewStudentUseCase).Assembly,
            typeof(AddWorkshopUseCase).Assembly,
            typeof(AddStudentWorkshopUseCase).Assembly
        ));

        builder.Services.AddHealthChecks()
            .AddSqlServer(
                connectionString: connectionString,
                name: "SQL Server",
                tags: new[] { "db", "sql", "sqlserver" });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseCors("AllowAll");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Oficina 2 v1"));
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseAuthorization();

        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var result = new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        exception = entry.Value.Exception?.Message,
                        duration = entry.Value.Duration.ToString()
                    })
                };
                await context.Response.WriteAsJsonAsync(result);
            }
        });

        app.Run();
    }
}
