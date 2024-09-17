namespace APIPOC.EndpointsFilters;

public class RangoIsLocketFilter(int lockedRangoId) :  IEndpointFilter
{
    public readonly int _lockedRangoId = lockedRangoId;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
        int rangoId;
        if (context.HttpContext.Request.Method == "PUT") {
            rangoId = context.GetArgument<int>(2);
        }else if (context.HttpContext.Request.Method == "DELETE") {
            rangoId = context.GetArgument<int>(1);
        }else {
            throw new NotSupportedException("este filtro não e suportado");
        }  
        if(rangoId == _lockedRangoId) {
            return TypedResults.Problem(new () {
                Status = 400,
                Title = "Não pode ser modificado ou deletar este ID",
                Detail = "Você nao pode  modificado ou deletar esta receita"
            });
        }
        var result = await next.Invoke(context);
        return result;
    }
}