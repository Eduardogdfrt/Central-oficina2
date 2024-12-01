using Ellp.Api.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;
using Ellp.Api.Infra.SqlServer.Repository;
using Ellp.Api.Infra.SqlServer;
using Ellp.Api.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Carrega as configurações do appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Carrega as configurações do appsettings.{Environment}.json
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

// Carrega variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Escutar na porta 8080 para todas as interfaces
    serverOptions.ListenAnyIP(8080);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Oficina 2", Version = "v1" });
});

// Configura o DbContext para usar a string de conexão do appsettings ou de variáveis de ambiente
var connectionString = builder.Configuration.GetConnectionString("DbConnectionString") ?? builder.Configuration["ConnectionStrings:DbConnectionString"];

builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginStudent.GetLoginStudentUseCase).Assembly,
    typeof(Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginProfessor.GetLoginProfessorUseCase).Assembly
));

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:7172")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

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

app.UseCors("AllowedOrigins");

app.MapControllers();

app.Run();
