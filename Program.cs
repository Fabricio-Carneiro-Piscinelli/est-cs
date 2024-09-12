using Microsoft.EntityFrameworkCore;
using APIPOC.DbContexts;
using Microsoft.AspNetCore.Mvc;
using APIPOC.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using APIPOC.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext para usar SQLite
builder.Services.AddDbContext<RangoDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:RangoDbConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

Console.WriteLine("teste 2+2!");

// Configura um endpoint de teste
app.MapGet("/", () => "teste de dados");

app.MapGet("/rangos", async Task<IResult> (
    RangoDbContext rangoDbContext,
    IMapper mapper,
    [FromQuery(Name = "name")] string? rangoName) => {
    var rangosEntity = await rangoDbContext.Rangos
        .Where(x => rangoName == null || x.Nome.Contains(rangoName))
        .ToListAsync();


    if (rangosEntity == null || !rangosEntity.Any())
    {
        return TypedResults.NoContent();
    }
    
    return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
});

// app.MapGet("/rango/{rangoId:int}/ingredientes", async (
//     RangoDbContext rangoDbContext, 
//     IMapper mapper,
//     int rangoId) => {
//         return mapper.Map<IEnumerable<IngredienteDTO>>((await rangoDbContext.Rangos
//                         .Include(rango => rango.Ingredientes)
//                         .FirstOrDefaultAsync(rango => rango.Id == rangoId))?.Ingredientes);
// });

// app.MapGet("/rango/{id:int}", async (RangoDbContext rangoDbContext, IMapper mapper, int id) => {
//     return mapper.Map<RangoDTO>((await rangoDbContext.Rangos.FirstOrDefaultAsync(d => d.Id == id)));
// });


app.Run();
