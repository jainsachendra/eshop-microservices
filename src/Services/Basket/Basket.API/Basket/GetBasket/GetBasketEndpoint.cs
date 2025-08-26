using Basket.API.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.GetBasket;

//public record GetBasketRequest(string userName);
public record GetBasketResponse(ShoppingCart ShoppingCart );

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}",
            async ( string userName, ISender mediator) =>
            {
                var query = new GetbasketQuery(userName);
                var result = await mediator.Send(query);
                var response =result.Adapt<GetBasketResponse>();
                return result is not null
                    ? Results.Ok(response)
                    : Results.NotFound();
            }).WithName("GetBasket")
              .WithSummary("Retrieves a shopping cart by user name")
              .Produces<GetBasketResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
