using Finan.Api.Common.Api;
using Finan.Core.Handlers;
using Finan.Core.Models;
using Finan.Core.Requests.Categories;
using Finan.Core.Responses;
//using System.Security.Claims;

namespace Finan.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync) // Mesmo "id" de parâmetro do "HandleAsync"
            .WithName("Categories: Delete")
            .WithSummary("Exclui uma categoria")
            .WithDescription("Exclui uma categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        //ClaimsPrincipal user, // Usuário logado
        ICategoryHandler handler,
        long id)
    {
        var request = new DeleteCategoryRequest
        {
            //UserId = user.Identity?.Name ?? string.Empty, // usuário logado
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}