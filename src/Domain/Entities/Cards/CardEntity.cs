namespace Domain.Entities.Cards;

public class CardEntity : BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public GemType TokenId { get; set; }
    public CardLevel Level { get; set; }
    public GemType Bonus { get; private set; }
    public int PrestigePoints { get; set; } = 0;

    public ICollection<CardCostEntity> Cost { get; set; } = new List<CardCostEntity>();

    #region Methods
    public static CardEntity Create(GemType tokenId, CardLevel level, int points = 0)
    {
        var card = new CardEntity();

        card.TokenId = tokenId;
        card.Level = level;

        if (points < 0)
            throw new ArgumentOutOfRangeException("Points cannot be less than 0.");

        card.Points = points;

        return card;
    }
    #endregion
}