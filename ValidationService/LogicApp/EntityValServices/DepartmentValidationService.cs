using FluentValidation;
using ValidationService.LogicApp.Abstractions;
using ValidationService.Interfaces;
using datalayer.Requests;

namespace ValidationService.LogicApp.EntityValServices
{
    public sealed class DepartmentValidationService : FluentValidationService<DepartmentRequest>, IDepartmentValidationService
    {

        public DepartmentValidationService()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name не должно быть пустым")
                .WithErrorCode("BRL-102.1");
        }
    }
}

