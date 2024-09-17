
using APIPOC.Models;
using MiniValidation;

namespace APIPOC.EndpointsFilters {
    public class ValidateAnnotationFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var rangoParaCriacaoDTO = context.GetArgument<RangoParaCriacaoDTO>(2);
            if (!MiniValidator.TryValidate(rangoParaCriacaoDTO, out var validationErrors)){
                return TypedResults.ValidationProblem(validationErrors);
            }
            return await next(context);
        }
    }
}