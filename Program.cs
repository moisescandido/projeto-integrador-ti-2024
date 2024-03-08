using Contatos.Models;
using Microsoft.EntityFrameworkCore;
using projeto.Components;
using projeto.db;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddBlazorBootstrap();
builder.Services.AddDbContext<ClimaContexto>(x => x.UseInMemoryDatabase("clima"));
builder.Services.AddScoped<Consultas>();
var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();
