using APIPOC.DbContexts;
using APIPOC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APIPOC.EndpointsHandlers;

public static class IngredientesHandlers {
    public static async Task<Results<NotFound, Ok<IEnumerable<IngredienteDTO>>>> GetIngredientes (
        RangoDbContext rangoDbContext, 
        IMapper mapper,
        int rangoId
    ){
        var rangos = await rangoDbContext.Rangos.FirstOrDefaultAsync(item => item.Id == rangoId);
        if( rangos == null) 
            return TypedResults.NotFound();

        return TypedResults.Ok(mapper.Map<IEnumerable<IngredienteDTO>>((await rangoDbContext.Rangos
            .Include(rango => rango.Ingredientes)
            .FirstOrDefaultAsync(rango => rango.Id == rangoId))?.Ingredientes));
    }
}