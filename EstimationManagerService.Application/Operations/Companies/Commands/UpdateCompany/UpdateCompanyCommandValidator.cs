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
                        .MinimumLength(EntityConfigurationValues.DisplayNameMinimumLenth)
                        .MaximumLength(EntityConfigurationValues.DisplayNameMaximumLenth)
                        .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DisplayNameMinimumLenth, EntityConfigurationValues.DisplayNameMaximumLenth));

            RuleFor(x => x.OwnerUserExternalId)
                .NotEmpty()
                .WithMessage(ValidationMessages.InvalidEmptyValue);

            RuleFor(x => x.CompanyExternalId)
                .NotEmpty()
                .WithMessage(ValidationMessages.InvalidEmptyValue);
        }
    }
}
