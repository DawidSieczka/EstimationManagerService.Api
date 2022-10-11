using EstimationManagerService.Application.Common.Validations;
using EstimationManagerService.Domain.EntitiesConfigurations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Groups.Commands.CreateGroup;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.DisplayName).NotEmpty()
            .MinimumLength(EntityConfigurationValues.DisplayNameMinimumLenth)
            .MaximumLength(EntityConfigurationValues.DisplayNameMaximumLenth)
            .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DisplayNameMinimumLenth,
                EntityConfigurationValues.DisplayNameMaximumLenth));
        
        RuleFor(x=>x.ExternalCompanyId).NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}