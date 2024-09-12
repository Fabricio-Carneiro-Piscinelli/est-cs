using AutoMapper;
using APIPOC.Entities;
using APIPOC.Models;

namespace APIPOC.Profiles;

public class RangoAgilProfile : Profile {
    public RangoAgilProfile() {
        CreateMap<Rango, RangoDTO>().ReverseMap();
        CreateMap<Ingrediente, IngredienteDTO>()
            .ForMember(
                d => d.RangoId,
                o => o.MapFrom(s => s.Rangos.First().Id)
            );
    }
}