using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;

public class GetUserTasksQueryValidator : AbstractValidator<GetUserTasksQuery>
{
    public GetUserTasksQueryValidator()
    {
        RuleFor(x=>x.UserExternalId).NotEmpty().WithMessage("Value can not be null");
    }
}
