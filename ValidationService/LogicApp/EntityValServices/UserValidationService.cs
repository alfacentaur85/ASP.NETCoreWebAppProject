using FluentValidation;
using ValidationService.LogicApp.Abstractions;
using Secutrity;
using ValidationService.Interfaces;
using datalayer.Requests;

namespace ValidationService.LogicApp.EntityValServices
{
    public sealed class UserValidationService : FluentValidationService<UserRequest>, IUserValidationService
    {

        public UserValidationService()
        {

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserName не должно быть пустым")
                .WithErrorCode("BRL-100.1");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password не должен быть пустым")
                .WithErrorCode("BRL-100.2.1");

            RuleFor(x => x.Password)
                .MinimumLength <UserRequest>(4)
                .WithMessage("Password не должен быть короче 4 символов")
                .WithErrorCode("BRL-100.2.2");

        }
    }
}

