namespace Domain.Entities.Cards;

public class CardEntity
{
    public TokenType TokenId { get; set; }
    public CardLevel Level { get; set; }
    public int Points { get; set; } = 0;

    public ICollection<CardCostEntity> Costs { get; set; } = default!;

    #region Methods
    public static CardEntity Create(TokenType tokenId, CardLevel level, int points = 0)
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