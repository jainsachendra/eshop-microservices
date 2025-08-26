namespace CatalogAPI.Products.GetProductByCatagory;

public record GetProductByCategory(IEnumerable<Product> Products);


public class GetProductByCategoryEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (string category, ISender mediator) =>
            {
                var query = new GetProductCategoryQuery(category);
                var result = await mediator.Send(query);
                return Results.Ok(result.Adapt<GetProductByCategory>());
            }).WithName("GetProductByCategory")
              .WithSummary("Retrieves products by category")
              .Produces<GetProductByCategory>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}

