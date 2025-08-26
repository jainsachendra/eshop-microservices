using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.GetBasket;

public record GetbasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart ShoppingCart);

public class GetBasketHandler (IBasketRepository basketRepository): IQueryHandler<GetbasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetbasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(request.UserName);
        return new GetBasketResult(basket);
    }
}
