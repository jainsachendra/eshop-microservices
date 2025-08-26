
namespace CatalogAPI.Products.GetProducts;

public record GetProductQuery(int? pageNumber, int? PageSize = 10) : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product>Products);

internal class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger)
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("getproduct");

        var products = await session.Query<Product>()
            .ToPagedListAsync(query.pageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
        return new GetProductResult(products);
    }
}
