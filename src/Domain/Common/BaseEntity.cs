namespace Domain.Common;

public class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}