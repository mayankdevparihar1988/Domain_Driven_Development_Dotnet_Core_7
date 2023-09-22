

using System;
using Application.Exceptions;
using Application.PipelineBehaviours.Contract;
using FluentValidation;
using MediatR;
namespace Application.PipelineBehaviours
{
    public class ValidationPipelineBehaviour< TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: IRequest<TResponse>, IValidateable
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
		{
            _validators = validators;
		}

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Check if it contains any sequence of validator
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                // Make a new list of error
                List<string> errors = new();

                // Perform validation and check if there is any result
                var validationResults = await Task
                    .WhenAll(
                        // Iterate all the validation
                        _validators.Select(x => x.ValidateAsync(context, cancellationToken))
                    );
                var failuers = validationResults.SelectMany(result => result.Errors)
                        .Where(failuer => failuer != null)
                        .ToList();

                if(failuers.Count != 0)
                {
                    foreach(var failuer in failuers)
                    {
                        errors.Add(failuer.ErrorMessage);
                    }

                    throw new CustomValidationException(errors, "One or More Validation failuer occured.");
                }
            }

            return await next();
        }
    }
}

