using Basket.API.Models;
using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record  DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
    }
}
public class DeleteBasketHandler(IBasketRepository basketRepository) : 
    ICommandHandler<DeleteBasketCommand,DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        basketRepository.DeleteBasket(request.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}
