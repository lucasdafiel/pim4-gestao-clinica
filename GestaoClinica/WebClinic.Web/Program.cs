using WebClinic.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using WebClinic.Core.Interfaces;
using WebClinic.Data.Context;

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
