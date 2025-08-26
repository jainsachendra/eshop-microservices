using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(bool IsSuccess);


public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapDelete("/basket",
            async (string userName, ISender send) =>
            {
                var result = await send.Send(new DeleteBasketCommand(userName));
                var response=result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            }).WithName("DeleteBasket")
              .WithSummary("Deletes a user's basket")
              .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
