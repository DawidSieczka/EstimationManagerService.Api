using System;
using EstimationManagerService.Application.Common.Validations;
using EstimationManagerService.Domain.EntitiesConfigurations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Companies.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .MinimumLength(EntityConfigurationValues.DisplayNameMinimumLenth)
            .MaximumLength(EntityConfigurationValues.DisplayNameMaximumLenth)
            .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DisplayNameMinimumLenth, EntityConfigurationValues.DisplayNameMaximumLenth));

        RuleFor(x => x.OwnerUserExternalId)
            .NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}
