
namespace CatalogAPI.Products.DeleteProduct;

//public record DeleteProductCommandRequest(int Id);
public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:int}",
            async (int id, ISender mediator) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await mediator.Send(command);
                return Results.Ok(result.Adapt<DeleteProductResponse>());
            }).WithName("DeleteProduct")
              .WithSummary("Deletes a product by ID")
              .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}

