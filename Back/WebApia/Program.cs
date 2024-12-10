using Ellp.Api.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;
using Ellp.Api.Infra.SqlServer.Repository;
using Ellp.Api.Infra.SqlServer;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Oficina 2", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("DbConnectionString") ?? builder.Configuration["ConnectionStrings:DbConnectionString"];

builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IWorkshopRepository, WorkshopRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginStudent.GetLoginStudentUseCase).Assembly,
    typeof(Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginProfessor.GetLoginProfessorUseCase).Assembly,
    typeof(Ellp.Api.Application.UseCases.AddParticipantUsecases.AddNewStudentUseCases.AddNewStudentUseCase).Assembly,
    typeof(Ellp.Api.Application.UseCases.AddWorkshops.AddWorkshopUseCase).Assembly
));

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:7172", "http://localhost:3000", "http://localhost:5000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
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

// Adiciona suporte a arquivos estáticos
app.UseDefaultFiles(); // Permite servir index.html por padrão
app.UseStaticFiles();  // Habilita o serviço de arquivos estáticos

app.UseRouting();
app.UseCors("AllowedOrigins");
app.UseAuthorization();

// Mapeia as rotas da API
app.MapControllers();

// Adiciona suporte ao roteamento do SPA
app.MapFallbackToFile("index.html"); // Redireciona todas as rotas não-API para o index.html do React

app.Run();
