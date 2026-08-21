using FluentValidation;
using MediatR;

namespace Department.Application.Behavior
{
    public class ValidateDepartmentBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {

        private readonly IEnumerable<IValidator<TRequest>> _validator;

        public ValidateDepartmentBehavior(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var failures = _validator
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f is not null)
                .ToList();

                if (failures.Any())
                {
                     throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
