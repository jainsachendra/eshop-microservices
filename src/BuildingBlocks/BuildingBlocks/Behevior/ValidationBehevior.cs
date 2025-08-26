using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuildingBlocks.Behevior;

public class ValidationBehevior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<IRequest>
   
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context=new ValidationContext<TRequest>(request);
        var validationresults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
     var failure=validationresults
            .Where(x => x.Errors.Any())
            .SelectMany(x => x.Errors)
            .ToList();
        if (failure.Any())
            throw new ValidationException(failure.FirstOrDefault()?.ErrorMessage ?? "Validation failed");
        return await next();
    }
}
