namespace EstimationManagerService.Application.Common.Exceptions;

public class NotFoundException : CustomException
{
    public override int StatusCode { get; set; } = 500;

    public NotFoundException(int id) : base($"Object of id: '{id}' not found")
    {
    }

    public NotFoundException(Guid id) : base($"Object of id: '{id}' not found")
    {
    }

    public NotFoundException(int id, Type type) : base($"Object of id: '{id}' and type of: {type} not found")
    {
    }

    public NotFoundException(Guid id, Type type) : base($"Object of id: '{id}' and type of: {type} not found")
    {
    }
}