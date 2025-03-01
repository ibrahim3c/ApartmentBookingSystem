﻿using ApartmentBooking.Application.Abstractions.Messaging;
using ApartmentBooking.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace ApartmentBooking.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        // install package => FluentValidation.DependencyInjectionExtensions
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>>validators)
        {
            this.validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // if there is no fluent validtion of this command don't do anything
            if(!validators.Any())
            {
                return await next();
            }

            // eles => get all errors and display them in custom exception if there

            /*
             *ValidationContext<T> is just a wrapper around an object that you want to validate.
                It helps FluentValidation know which object it is working on.
                Think of it as a box 📦 that holds the object inside.
                Validators open the box, take out the object, and check if it meets the rules.
             */

            var context = new ValidationContext<TRequest>(request);

            var validationErrors = validators
                .Select(validator => validator.Validate(context))
                .Where(validationResult => validationResult.Errors.Any())
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationFailure => new ValidationError(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage))
                .ToList();

            if (validationErrors.Any())
                //custom exception
                throw new Exceptions.ValidationException(validationErrors);

            return await next();

        }
    }
}
