namespace Domain.Entities.Cards;

public class NobleEntity : BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public Dictionary<GemType, int> Requirement { get; private set; } = new();
    public int PrestigePoints { get; private set; } = 3;

    public NobleEntity(string name, Dictionary<GemType, int> requirement)
    {
        Name = name;
        Requirement = requirement;
    }
}