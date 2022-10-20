using EstimationManagerService.Application.Common.Validations;
using EstimationManagerService.Domain.EntitiesConfigurations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.UserTasks.Commands.UpdateUserTask;

public class UpdateUserTaskCommandValidator : AbstractValidator<UpdateUserTaskCommand>
{
    public UpdateUserTaskCommandValidator()
    {
        RuleFor(x => x.DisplayName)
            .MinimumLength(EntityConfigurationValues.DisplayNameMinimumLength)
            .MaximumLength(EntityConfigurationValues.DisplayNameMaximumLength)
            .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DisplayNameMinimumLength,
                EntityConfigurationValues.DisplayNameMaximumLength));

        RuleFor(x => x.Description)
            .MinimumLength(EntityConfigurationValues.DescriptionMinimumLength)
            .MaximumLength(EntityConfigurationValues.DescriptionMaximumLength)
            .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DescriptionMinimumLength,
                EntityConfigurationValues.DescriptionMaximumLength));
    }
}