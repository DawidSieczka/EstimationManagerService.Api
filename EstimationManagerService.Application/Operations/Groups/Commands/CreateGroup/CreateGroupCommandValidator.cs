using EstimationManagerService.Application.Common.Validations;
using EstimationManagerService.Domain.EntitiesConfigurations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Groups.Commands.CreateGroup;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.DisplayName).NotEmpty()
            .MinimumLength(EntityConfigurationValues.DisplayNameMinimumLength)
            .MaximumLength(EntityConfigurationValues.DisplayNameMaximumLength)
            .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DisplayNameMinimumLength,
                EntityConfigurationValues.DisplayNameMaximumLength));
        
        RuleFor(x=>x.CompanyExternalId).NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}