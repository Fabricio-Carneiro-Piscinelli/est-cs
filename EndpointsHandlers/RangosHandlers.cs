using Microsoft.EntityFrameworkCore;
using APIPOC.DbContexts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using APIPOC.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using APIPOC.Entities;

namespace APIPOC.EndpointsHandlers;

public static class RangosHandlers {
    public static async Task<IResult> GetRangosAsync (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        [FromQuery(Name = "name")] string? rangoName
    ) {
        var rangosEntity = await rangoDbContext.Rangos
            .Where(x => rangoName == null || x.Nome.Contains(rangoName))
            .ToListAsync();


        if (rangosEntity == null || !rangosEntity.Any()) {
            return TypedResults.NoContent();
        }
        
        return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
    }

    public static async Task<RangoDTO> GetRangosIdAsync (
        RangoDbContext rangoDbContext,
        IMapper mapper, int rangoId
    ){
        return mapper.Map<RangoDTO>((
            await rangoDbContext.Rangos.FirstOrDefaultAsync(d => d.Id == rangoId)
        ));
    }

    public static async Task<CreatedAtRoute<RangoDTO>> NewRango(
        RangoDbContext rangoDbContext,
        IMapper mapper,
        [FromBody] RangoParaCriacaoDTO rangoParaCriacaoDTO)
    {
        var rangoEntity = mapper.Map<Rango>(rangoParaCriacaoDTO);
        rangoDbContext.Add(rangoEntity);
        await rangoDbContext.SaveChangesAsync();
        var rangoToReturn = mapper.Map<RangoDTO>(rangoEntity);

        return TypedResults.CreatedAtRoute(rangoToReturn, "getRangos", new {rangoId = rangoToReturn.Id});
    }

    public static async Task<Results<NotFound, Ok>> UpdateRangos(
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId,
        [FromBody] RangoParaAtualizacaoDTO bunda)   
    {   
        var rangos = await rangoDbContext.Rangos.FirstOrDefaultAsync(item => item.Id == rangoId);
        if (rangos == null)
            return TypedResults.NotFound();

        mapper.Map(bunda, rangos);
        await rangoDbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }
    
    public static async Task<Results<NoContent, Ok>> DeleteRangos (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId
    ) {
        var rangos = await rangoDbContext.Rangos.FirstOrDefaultAsync(item => item.Id == rangoId);
        if( rangos == null) 
            return TypedResults.NoContent();

        rangoDbContext.Rangos.Remove(rangos);
        await rangoDbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }
}
