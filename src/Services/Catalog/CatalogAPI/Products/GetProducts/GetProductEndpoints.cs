namespace CatalogAPI.Products.GetProducts;

public record GetProductRequest(int? pageNumber,int? PageSize=10);
public record GetProductResponse(IEnumerable<Product> products);
public class GetProductEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
            async  ([AsParameters] GetProductRequest request, ISender mediator) =>
            {
                var query = request.Adapt<GetProductQuery>();
                var result = await mediator.Send(query);
                return Results.Ok(result.Adapt<GetProductResponse>());
            }).WithName("GetProducts")
              .WithSummary("Retrieves all products")
              .Produces<GetProductResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
