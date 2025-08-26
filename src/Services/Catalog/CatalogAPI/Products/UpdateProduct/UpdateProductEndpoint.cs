namespace CatalogAPI.Products.UpdateProduct;

public record class UpdateProductRequest(int Id, string Name, string Description, LinkedList<string> Categories, string ImageFile, decimal Price);
    

public record class UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products",
            async (UpdateProductRequest request, ISender mediator) =>
            {
                var command = request.Adapt<UpdateProductCommmand>();
                var result = await mediator.Send(command);
                return Results.Ok(result.Adapt<UpdateProductResponse>());
            }).WithName("UpdateProduct")
              .WithSummary("Updates an existing product")
              .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}

