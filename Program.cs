using Microsoft.EntityFrameworkCore;
using APIPOC.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext para usar SQLite
builder.Services.AddDbContext<RangoDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:RangoDbConStr"])
);

var app = builder.Build();

Console.WriteLine("teste 2+2!");

// Configura um endpoint de teste
app.MapGet("/", () => "teste de dados");

app.Run();
