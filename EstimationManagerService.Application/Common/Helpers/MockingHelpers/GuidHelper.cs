using System;
using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;

namespace EstimationManagerService.Application.Common.Helpers.MockingHelpers;

public class GuidHelper : IGuidHelper
{
    public Guid CreateGuid() => Guid.NewGuid();
}
