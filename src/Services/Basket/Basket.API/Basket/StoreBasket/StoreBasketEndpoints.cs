using Basket.API.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.StoreBasket;
public record StoreBaskeRequest(ShoppingCart ShoppingCart);
public record  StoreBasketResponse(string userName);

public class StoreBasketEndpoints: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/storebasket",
            async (StoreBaskeRequest request, ISender mediator) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await mediator.Send(command);
                return Results.Ok(result.Adapt<StoreBasketResponse>());
            }).WithName("StoreBasket")
              .WithSummary("Stores a shopping cart for a user")
              .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}

