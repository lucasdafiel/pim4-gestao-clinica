using Microsoft.EntityFrameworkCore;
using System.Text;
using WebClinic.Core.Interfaces;
using WebClinic.Data.Context;
using WebClinic.Data.Repositories;
using WebClinic.Web.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// 1. Pega a Connection String do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Adiciona o DbContext ao cont�iner de servi�os e configura o provedor SQL Server
builder.Services.AddDbContext<WebClinicContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// 3. Registra o nosso reposit�rio REAL. Agora, quando algu�m pedir um IPacienteRepository,
// o sistema entregar� uma inst�ncia de PacienteRepository.
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
// Inje��o de Depend�ncia dos Servi�os
builder.Services.AddScoped<ITokenService, TokenService>(); // <-- Registra o servi�o de token

// --- CONFIGURA��O DA AUTENTICA��O JWT ---
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true; // Use true em produ��o
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adiciona os servi�os para descobrir e descrever os endpoints da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // ADICIONE AS LINHAS ABAIXO AQUI
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Adicione esta linha para habilitar a autentica��o
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
