using System.Xml.Linq;

namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommmand(int Id,string Name, string Description, List<string> Categories, string ImageFile, decimal Price)
    :ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommmand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommmand request, CancellationToken cancellationToken)
    {
      var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFouundException(product.Id);
        }
        product.Name = request.Name;
        product.Description = request.Description;
        product.Categories = request.Categories;
        product.ImageFile = request.ImageFile;
        product.Price = request.Price;
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);

    }
}
