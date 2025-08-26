namespace CatalogAPI.Products.GetProductByid;

public record GetProductbyIdResponse(Product Product);


public class GetProductByIdEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:int}",
            async (int id, ISender mediator) =>
            {
                var query = new GetProdcutByIdQuery(id);
                var result = await mediator.Send(query);
                return Results.Ok(result.Adapt<GetProductbyIdResponse>());
            }).WithName("GetProductById")
              .WithSummary("Retrieves a product by ID")
              .Produces<GetProductbyIdResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
