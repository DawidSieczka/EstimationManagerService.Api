using System;

namespace EstimationManagerService.Application.Common.Options;

public class ConnectionStringsOptions
{
    public const string ConnectionStrings = "ConnectionStrings";

    public string SqlDatabase { get; set; } = string.Empty;
}
