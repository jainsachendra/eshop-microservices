

namespace CatalogAPI.Products.GetProductByid
{
    public record GetProdcutByIdQuery(int Id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    public class GetProductByIdHandler(IDocumentSession session,ILogger<GetProductByIdHandler> logger)
        : IQueryHandler<GetProdcutByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProdcutByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            if (product is null)
            {
                logger.LogWarning("Product with ID {Id} not found", query.Id);
                throw new ProductNotFouundException(query.Id);
            }
            return new GetProductByIdResult(product);
        }
    }
}
