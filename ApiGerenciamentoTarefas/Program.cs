using Api.Application.Interfaces.Services;
using Api.Application.Profiles;
using Api.Application.Service;
using Api.Domain.Entities;
using Api.Domain.Repositories;
using Api.Infrastructure.Context;
using Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
builder.Services.AddScoped<ITaskServices, TaskServices>();
builder.Services.AddScoped<IRepository<TaskToDo>, Repository<TaskToDo>>();

builder.Services.AddSwaggerGen(a =>
{
    a.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Description = "jwt",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Name = "Authorization"
    });
    a.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//configurar authentication
/*
 ValidateIssuer
Fun��o: Valida se o emissor (issuer) do token � confi�vel.
Descri��o: O emissor � uma entidade que gera e assina o token JWT. Este segmento garante que o token est� vindo do emissor esperado e n�o de uma fonte n�o autorizada.

ValidateAudience
Fun��o: Valida se a audi�ncia (audience) do token � adequada.
Descri��o: A audi�ncia � quem o token � destinado a atingir. Este segmento garante que o token � v�lido para a aplica��o ou servi�o que o recebe.

ValidateLifetime
Fun��o: Valida se o token ainda est� dentro do per�odo de validade.
Descri��o: O token JWT possui uma data de expira��o. Este segmento garante que o token n�o expirou e ainda � v�lido para uso.

ValidateIssuerSigningKey
Fun��o: Valida a chave usada para assinar o token.
Descri��o: O token JWT � assinado digitalmente para garantir a integridade e autenticidade. Este segmento garante que a assinatura do token � v�lida e foi criada com uma chave confi�vel.
 */
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uma-chave-super-segura-e-longa-suficiente"))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
