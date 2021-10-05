using FluentValidation;
using ValidationService.LogicApp.Abstractions;
using Secutrity;
using ValidationService.Interfaces;
using datalayer.Responses;

namespace ValidationService.LogicApp.EntityValServices
{
    public sealed class SecurityValidationService : FluentValidationService<LoginPassword>, ISecurityValidationService
    {

        public SecurityValidationService()
        {

            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login не должно быть пустым")
                .WithErrorCode("BRL-104.1");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password не должен быть пустым")
                .WithErrorCode("BRL-104.2.1");

            RuleFor(x => x.Password)
                .MinimumLength <LoginPassword>(4)
                .WithMessage("Password не должен быть короче 4 символов")
                .WithErrorCode("BRL-104.2.2");

        }
    }
}

