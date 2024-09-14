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
app.MapGet("/", () => "teste de dados");




var rangosEndpoints = app.MapGroup("/rangos");
var rangosComIdEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
var rangosComIngredientes = rangosComIdEndpoints.MapGroup("/ingredientes");

rangosEndpoints.MapGet("", async Task<IResult> (
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

rangosComIngredientes.MapGet("", async (
    RangoDbContext rangoDbContext, 
    IMapper mapper,
    int rangoId) => {
        return mapper.Map<IEnumerable<IngredienteDTO>>((await rangoDbContext.Rangos
                        .Include(rango => rango.Ingredientes)
                        .FirstOrDefaultAsync(rango => rango.Id == rangoId))?.Ingredientes);
});

rangosComIdEndpoints.MapGet("", async (RangoDbContext rangoDbContext, IMapper mapper, int rangoId) => {
    return mapper.Map<RangoDTO>((
        await rangoDbContext.Rangos.FirstOrDefaultAsync(d => d.Id == rangoId)
    ));
}).WithName("getRangos");

rangosEndpoints.MapPost("", async Task<CreatedAtRoute<RangoDTO>>(
    RangoDbContext rangoDbContext,
    IMapper mapper,
    [FromBody] RangoParaCriacaoDTO rangoParaCriacaoDTO) => 
{
    var rangoEntity = mapper.Map<Rango>(rangoParaCriacaoDTO);
    rangoDbContext.Add(rangoEntity);
    await rangoDbContext.SaveChangesAsync();
    var rangoToReturn = mapper.Map<RangoDTO>(rangoEntity);

    return TypedResults.CreatedAtRoute(rangoToReturn, "getRangos", new {rangoId = rangoToReturn.Id});
});

rangosComIdEndpoints.MapPut("", async Task<Results<NotFound, Ok>>(
    RangoDbContext rangoDbContext,
    IMapper mapper,
    int rangoId,
    [FromBody] RangoParaAtualizacaoDTO bunda) =>     
{   
    var rangos = await rangoDbContext.Rangos.FirstOrDefaultAsync(item => item.Id == rangoId);
    if (rangos == null)
        return TypedResults.NotFound();

    mapper.Map(bunda, rangos);
    await rangoDbContext.SaveChangesAsync();

    return TypedResults.Ok();
});

rangosComIdEndpoints.MapDelete("", async Task<Results<NoContent, Ok>>(
    RangoDbContext rangoDbContext,
    IMapper mapper,
    int rangoId
) => {
    var rangos = await rangoDbContext.Rangos.FirstOrDefaultAsync(item => item.Id == rangoId);
    if( rangos == null) 
        return TypedResults.NoContent();

    rangoDbContext.Rangos.Remove(rangos);
    await rangoDbContext.SaveChangesAsync();

    return TypedResults.Ok();
});

app.Run();
