using BuildingBlocks.Exceptions;

namespace CatalogAPI.Exceptions;

public class ProductNotFouundException:NotFoundException
{
    public ProductNotFouundException(int id)
        : base("product")
    {
    }
}
