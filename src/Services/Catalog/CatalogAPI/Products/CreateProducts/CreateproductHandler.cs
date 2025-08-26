

namespace CatalogAPI.Products.CreateProducts;
public record CreateproductCommand(string Name, string Description, List<string> Categories, string ImageFile, decimal Price)
    :ICommand<CreateproductResult>;
public record CreateproductResult(int Id);

public class CreateProductCommandValidator:AbstractValidator<CreateproductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("At least one category is required.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}
internal class CreateproductCommandHandler(IDocumentSession session,IValidator<CreateproductCommand>validator)
    :ICommandHandler<CreateproductCommand, CreateproductResult>

{
    public async Task<CreateproductResult> Handle(CreateproductCommand command, CancellationToken cancellationToken)
    {
        // Simulate product creation logic

        var result = await validator.ValidateAsync(command, cancellationToken);
        var errors = result.Errors.Select(x=>x.ErrorMessage).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors.FirstOrDefault());
        }
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Categories = command.Categories,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateproductResult(product.Id);
         
    }
}
