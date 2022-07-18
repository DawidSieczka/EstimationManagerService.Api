using System;
using EstimationManagerService.Application.Common.Validations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    public DeleteCompanyCommandValidator()
    {
        RuleFor(x => x.OwnerUserExternalId)
                .NotEmpty()
                .WithMessage(ValidationMessages.InvalidEmptyValue);

        RuleFor(x => x.CompanyExternalId)
            .NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}
