using Microsoft.EntityFrameworkCore;
using APIPOC.DbContexts;
using APIPOC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext para usar SQLite
builder.Services.AddDbContext<RangoDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:RangoDbConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.RegisterIngredientesEndpoints();
app.RegisterRangosEndpoints();

app.Run();
