
namespace CatalogAPI.Products.CreateProducts;
public record  CreateProductRequest(string Name, string Description, LinkedList<string> Categories, string ImageFile, decimal Price);
public record CreateProductResponse(int Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async (CreateProductRequest request, ISender mediator) =>
        {
            var command = request.Adapt<CreateproductCommand>();
            var result = await mediator.Send(command);
            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        }).WithName("CreateProduct")
          .WithSummary("Creates a new product")
          .Produces<CreateProductResponse>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status500InternalServerError);
          

    }
}
