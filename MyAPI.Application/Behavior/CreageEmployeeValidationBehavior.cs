using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Behavior
{
    public class CreageEmployeeValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;

        public CreageEmployeeValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var failure = _validator
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f is not null)
                .ToList();

                if (failure.Any())
                {
                    throw new ValidationException(failure);
                }
            }

            return await next();
        }
    }
}
