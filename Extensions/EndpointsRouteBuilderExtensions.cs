using APIPOC.EndpointsFilters;
using APIPOC.EndpointsHandlers;
using Microsoft.AspNetCore.Identity;

namespace APIPOC.Extensions;

public static class EndpointsRouteBuilderExtensions {

    public static void RegisterRangosEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGroup("/identity").MapIdentityApi<IdentityUser>();
        endpointRouteBuilder.MapGet("/pratos/{pratoId:int}", (int pratoId) => $"pratos {pratoId}")
            .WithOpenApi(operation => {
                operation.Deprecated = true;
                return operation;
            })
                .WithSummary("Este endpoint esta deprecated e sera descontinuado na versao 2 desta api")
                .WithDescription("por favor tatatatatata");

        var rangosEndpoints = endpointRouteBuilder.MapGroup("/rangos").RequireAuthorization("requiredBrazil").RequireAuthorization();
        var rangosComIdEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
        var rangosComIdAndLockFilterEndpoints = rangosComIdEndpoints; //.AddEndpointFilter(new RangoIsLocketFilter(7));

        rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync);
        rangosComIdEndpoints.MapGet("", RangosHandlers.GetRangosIdAsync).WithName("getRangos").AllowAnonymous();

        rangosEndpoints.MapPost("", RangosHandlers.NewRango)
            .AddEndpointFilter<ValidateAnnotationFilters>();
        rangosComIdEndpoints.MapPut("", RangosHandlers.UpdateRangos).AddEndpointFilter( new RangoIsLocketFilter(7));
        rangosComIdAndLockFilterEndpoints.MapDelete("", RangosHandlers.DeleteRangos);
    }

     public static void RegisterIngredientesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var rangosComIngredientes = endpointRouteBuilder.MapGroup("/rangos/{rangoId:int}/ingredientes").RequireAuthorization();

        rangosComIngredientes.MapGet("", IngredientesHandlers.GetIngredientes);
        rangosComIngredientes.MapPost("", () => {
            throw new NotImplementedException();
        });

    }
}
