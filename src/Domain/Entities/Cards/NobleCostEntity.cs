namespace Domain.Entities.Cards;

public class NobleCostEntity : BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public GemType GemId { get; set; }
    public int Amount { get; set; }
}