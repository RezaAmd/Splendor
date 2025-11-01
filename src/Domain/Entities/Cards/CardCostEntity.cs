namespace Domain.Entities.Cards;

public class CardCostEntity : BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required Guid CardId { get; set; }
    public GemType GemId { get; set; }
    public int Amount { get; set; }

    public virtual CardEntity? DevelopmentCard { get; set; }

    #region Methods
    public static CardCostEntity Create(GemType tokenId, int count, Guid cardId)
    {
        var cardCost = new CardCostEntity();

        cardCost.TokenId = tokenId;

        if (count < 1)
            throw new ArgumentOutOfRangeException("Count cannot less than 1.");
        if (count > 7)
            throw new ArgumentOutOfRangeException("Count cannot greater than 7.");
        cardCost.Count = count;

        if (cardId == Guid.Empty)
            throw new ArgumentNullException("Card id cannot be null or empty.");
        cardCost.CardId = cardId;

        return cardCost;
    }
    #endregion
}