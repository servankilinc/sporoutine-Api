using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Pipelines.Validation;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
        List<ValidationFailure> failures = _validators
                                       .Select(validator => validator.Validate(context))
                                       .Where(result => result.IsValid == false)
                                       .SelectMany(result => result.Errors)
                                       .ToList();
        
        if (failures.Any()) throw new ValidationException(failures);

        return await next();
    }
}
