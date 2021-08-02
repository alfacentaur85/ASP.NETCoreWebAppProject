using FluentValidation;
using ValidationService.LogicApp.Abstractions;
using System;
using ValidationService.Interfaces;
using datalayer.Requests;

namespace ValidationService.LogicApp.EntityValServices
{
    public sealed class PersonValidationService : FluentValidationService<PersonRequest>, IPersonValidationService
    {

        public PersonValidationService()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName не должно быть пустым")
                .WithErrorCode("BRL-103.1");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("LastName не должно быть пустым")
                .WithErrorCode("BRL-103.2");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email не должен быть пустым")
                .WithErrorCode("BRL-103.3.4");

            RuleFor(x => x.Email)
                .MinimumLength <PersonRequest>(5)
                .WithMessage("Email не должен быть короче 5 символов")
                .WithErrorCode("BRL-100.3.1");

            RuleFor(x => x.Email)
                .Must(x => x.Contains("."))
                .WithMessage("Email должен содержать не менее одной точки")
                .WithErrorCode("BRL-100.3.3");

            RuleFor(x => x.Email)
                .Must(x => x.Contains("@"))
                .WithMessage("Email должен содержать символ @")
                .WithErrorCode("BRL-100.3.2");

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 150)
                .WithMessage("Age должен быть в диапазоне от 1 до 150")
                .WithErrorCode("BRL-103.4.1");


        }
    }
}

