namespace Domain.Entities.Games;

public class BankEntity : BaseEntity
{
    private readonly Dictionary<GemType, int> _gems = new();

    public BankEntity()
    {
        foreach (GemType gem in Enum.GetValues(typeof(GemType)))
        {
            _gems[gem] = 0;
        }
    }

    public void InitializeForPlayerCount(int playerCount)
    {
        _gems[GemType.Ruby] = playerCount == 2 ? 4 : 5;
        _gems[GemType.Emerald] = playerCount == 2 ? 4 : 5;
        _gems[GemType.Sapphire] = playerCount == 2 ? 4 : 5;
        _gems[GemType.Diamond] = playerCount == 2 ? 4 : 5;
        _gems[GemType.Onyx] = playerCount == 2 ? 4 : 5;
        _gems[GemType.Gold] = 5;
    }

    public bool HasGem(GemType gem) => _gems[gem] > 0;

    public void Withdraw(GemType[] gems)
    {
        foreach (var gem in gems)
        {
            if (_gems[gem] <= 0)
                throw new InvalidOperationException($"Bank does not have {gem}");

            _gems[gem]--;
        }
    }

    public void Deposit(GemType gem, int count = 1)
    {
        _gems[gem] += count;
    }

    public int GetCount(GemType gem) => _gems[gem];
}