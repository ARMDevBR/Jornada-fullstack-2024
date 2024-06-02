using Finan.Api.Common.Api;
using Finan.Core;
using Finan.Core.Handlers;
using Finan.Core.Models;
using Finan.Core.Requests.Categories;
using Finan.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Recupera todas as categorias")
            .WithDescription("Recupera todas as categorias")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

    //localhost:8080/v1/categories/1/25 => página e tamanho da página pela rota
    //localhost:8080/v1/categories?pageNumber=1&pageSize=25 => página e tamanho da página pela query
    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = ApiConfiguration.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}