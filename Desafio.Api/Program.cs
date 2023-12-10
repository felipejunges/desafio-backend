using Desafio.Api.Configuration;
using Desafio.Application;
using Desafio.Application.Configurations;
using Desafio.Application.Services.Tokens;
using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using Desafio.Domain.UnitOfWork;
using Desafio.Infra.Context;
using Desafio.Infra.Repositories;
using Desafio.Infra.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Begin - Add services to the container.
builder.Services.AddDbContext<CadastroDb>(db =>
{
    db.UseInMemoryDatabase("Cadastro");
    db.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    
    //db.UseSqlServer("Data Source=SERVER_DO_SQL;Initial Catalog=Desafio;Persist Security Info=True;User ID=USER_DO_SQL;Password=SENHA_DO_SQL;TrustServerCertificate=True");
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var authConfiguration = builder.Configuration.GetSection("Auth").Get<AuthConfiguration>() ?? throw new NullReferenceException("'Auth' deve ser configurado no appsettings.json");
builder.Services.AddSingleton(authConfiguration);
// End - Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5153", "http://localhost:5173", "http://127.0.0.1:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddApiSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CadastroDb>();
    db.Database.EnsureCreated();

    if (!db.Usuarios.Any())
    {
        db.Usuarios.Add(
            new Usuario(
                0,
                "Felipe Junges",
                "felipejunges@yahoo.com.br",
                "felipe123",
                "Admin",
                true));

        db.SaveChanges();
    }
}

app.Run();
