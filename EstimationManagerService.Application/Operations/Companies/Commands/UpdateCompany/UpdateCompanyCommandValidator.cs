using System;
using EstimationManagerService.Application.Common.Validations;
using EstimationManagerService.Domain.EntitiesConfigurations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.DisplayName)
                        .NotEmpty()
                        .MinimumLength(EntityConfigurationValues.DisplayNameMinimumLength)
                        .MaximumLength(EntityConfigurationValues.DisplayNameMaximumLength)
                        .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DisplayNameMinimumLength, EntityConfigurationValues.DisplayNameMaximumLength));

            RuleFor(x => x.OwnerUserExternalId)
                .NotEmpty()
                .WithMessage(ValidationMessages.InvalidEmptyValue);

            RuleFor(x => x.CompanyExternalId)
                .NotEmpty()
                .WithMessage(ValidationMessages.InvalidEmptyValue);
        }
    }
}
