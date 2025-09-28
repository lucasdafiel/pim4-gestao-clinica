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

// 2. Adiciona o DbContext ao contêiner de serviços e configura o provedor SQL Server
builder.Services.AddDbContext<WebClinicContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// 3. Registra o nosso repositório REAL. Agora, quando alguém pedir um IPacienteRepository,
// o sistema entregará uma instância de PacienteRepository.
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
// Injeção de Dependência dos Serviços
builder.Services.AddScoped<ITokenService, TokenService>(); // <-- Registra o serviço de token

// --- CONFIGURAÇÃO DA AUTENTICAÇÃO JWT ---
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true; // Use true em produção
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

// Adiciona os serviços para descobrir e descrever os endpoints da API
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

app.UseAuthentication(); // Adicione esta linha para habilitar a autenticação
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
