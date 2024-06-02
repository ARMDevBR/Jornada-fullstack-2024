using Finan.Api.Data;
using Finan.Core.Handlers;
using Finan.Core.Models;
using Finan.Core.Requests.Categories;
using Finan.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Finan.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description
        };

        try
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync(); // Faz o "commit" no banco. Se � chamar, ser� feito rollback.

            return new Response<Category?>(category, 201, "Categoria criada com sucesso!");
        }
        /*catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }*/
        catch
        {
            return new Response<Category?>(null, 500, "N�o foi poss�vel criar a categoria.");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Categoria n�o encontrada.");

            category.Title = request.Title;
            category.Description = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync(); // Faz o "commit" no banco. Se � chamar, ser� feito rollback.

            return new Response<Category?>(category, message: "Categoria atualizada com sucesso!");
        }
        catch
        {
            return new Response<Category?>(null, 500, "N�o foi poss�vel atualizar a categoria.");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Categoria n�o encontrada.");

            context.Categories.Remove(category);
            await context.SaveChangesAsync(); // Faz o "commit" no banco. Se � chamar, ser� feito rollback.

            return new Response<Category?>(category, message: "Categoria exclu�da com sucesso!");
        }
        catch
        {
            return new Response<Category?>(null, 500, "N�o foi poss�vel excluir a categoria.");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return category is null
                ? new Response<Category?>(null, 404, "Categoria n�o encontrada.")
                : new Response<Category?>(category);
        }
        catch
        {
            return new Response<Category?>(null, 500, "N�o foi poss�vel recuperar a categoria.");
        }
    }

    public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context.Categories.AsNoTracking().Where(x => x.UserId == request.UserId).OrderBy(x => x.Title);

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>?>(
                categories,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Category>?>(null, 500, "N�o foi poss�vel consultar as categorias.");
        }
    }
}