
namespace CatalogAPI.Products.GetProductByCatagory;

public record GetProductCategoryQuery(string category):IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryHandler(IDocumentSession session,ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GetProductCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Categories.Contains(query.category))
            .ToListAsync(cancellationToken);
     return new GetProductByCategoryResult(products);
    }
}
