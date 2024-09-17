using APIPOC.EndpointsFilters;
using APIPOC.EndpointsHandlers;
namespace APIPOC.Extensions;

public static class EndpointsRouteBuilderExtensions {

    public static void RegisterRangosEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var rangosEndpoints = endpointRouteBuilder.MapGroup("/rangos");
        var rangosComIdEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
        var rangosComIdAndLockFilterEndpoints = rangosComIdEndpoints.AddEndpointFilter(new RangoIsLocketFilter(7));

        rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync);
        rangosComIdEndpoints.MapGet("", RangosHandlers.GetRangosIdAsync).WithName("getRangos");
        rangosEndpoints.MapPost("", RangosHandlers.NewRango)
            .AddEndpointFilter<ValidateAnnotationFilters>();
        rangosComIdEndpoints.MapPut("", RangosHandlers.UpdateRangos).AddEndpointFilter( new RangoIsLocketFilter(7));
        rangosComIdAndLockFilterEndpoints.MapDelete("", RangosHandlers.DeleteRangos);
    }

     public static void RegisterIngredientesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var rangosComIngredientes = endpointRouteBuilder.MapGroup("/rangos/{rangoId:int}/ingredientes");

        rangosComIngredientes.MapGet("", IngredientesHandlers.GetIngredientes);
        rangosComIngredientes.MapPost("", () => {
            throw new NotImplementedException();
        });

    }
}
