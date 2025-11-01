using Domain.Entities.Cards;

namespace Domain.Entities.Players;

public class PlayerEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public Dictionary<GemType, int> Gems { get; private set; } = new();
    public List<CardEntity> PurchasedCards { get; private set; } = new();
    public List<CardEntity> ReservedCards { get; private set; } = new();
    public List<NobleEntity> Nobles { get; private set; } = new();
    public int PrestigePoints => PurchasedCards.Sum(c => c.PrestigePoints) + Nobles.Sum(n => n.PrestigePoints);

    public bool IsBot { get; private set; } = false;

    public PlayerEntity(string name, bool isBot = false)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsBot = isBot;

        foreach (GemType gem in Enum.GetValues(typeof(GemType)))
        {
            Gems[gem] = 0;
        }
    }

    public void AddGems(GemType[] gems)
    {
        foreach (var gem in gems)
            Gems[gem]++;
    }

    public void AddGem(GemType gem, int count = 1) => Gems[gem] += count;

    public void ReserveCard(CardEntity card)
    {
        if (ReservedCards.Count >= 3)
            throw new InvalidOperationException("Cannot reserve more than 3 cards");

        ReservedCards.Add(card);
    }

    public void AddCard(CardEntity card) => PurchasedCards.Add(card);
    public bool CanAfford(CardEntity card)
    {
        foreach (var cost in card.Cost)
        {
            var required = cost.Amount;
            var gemType = cost.GemId;

            var owned = Gems.TryGetValue(gemType, out var g) ? g : 0;
            var bonus = PurchasedCards.Count(c => c.Bonus == gemType);

            if (owned + bonus < required)
                return false;
        }

        return true;
    }

    public void PayFor(CardEntity card)
    {
        foreach (var cost in card.Cost)
        {
            var gemType = cost.GemId;
            var required = cost.Amount;

            var owned = Gems.TryGetValue(gemType, out var g) ? g : 0;
            var bonus = PurchasedCards.Count(c => c.Bonus == gemType);

            var pay = Math.Max(0, required - bonus);

            if (pay > owned)
                throw new InvalidOperationException($"Not enough {gemType} gems to pay");

            Gems[gemType] -= pay;
        }
    }


    public void AddNoble(NobleEntity noble) => Nobles.Add(noble);

    public bool MeetsRequirements(NobleEntity noble)
    {
        foreach (var kv in noble.Requirement)
        {
            var count = PurchasedCards.Count(c => c.Bonus == kv.Key);
            if (count < kv.Value)
                return false;
        }
        return true;
    }
}

}