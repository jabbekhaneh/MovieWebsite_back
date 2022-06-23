﻿using FluentValidation;
using MediatR;
using System.Text;

namespace Portal.Application.Common;

public class ValidateCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IList<IValidator<TRequest>> _validators;
    
    public ValidateCommandBehavior(IList<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {

        var errors = _validators
             .Select(v => v.Validate(request))
             .SelectMany(result => result.Errors)
             .Where(error => error != null)
             .ToList();
        if (errors.Any())
        {
            var errorBuilder = new StringBuilder();

            errorBuilder.AppendLine("Invalid Dto ");

            foreach (var error in errors)
            {
                errorBuilder.AppendLine(error.ErrorMessage);
            }

            throw new Exception(errorBuilder.ToString());

        }

        return next();
    }

}
