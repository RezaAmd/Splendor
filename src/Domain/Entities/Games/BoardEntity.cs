using Domain.Entities.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Games;

public class BoardEntity
{
    public List<CardEntity> Level1Cards { get; private set; } = new();
    public List<CardEntity> Level2Cards { get; private set; } = new();
    public List<CardEntity> Level3Cards { get; private set; } = new();
    public List<NobleEntity> Nobles { get; private set; } = new();
    public static void Initialize()
    {
        // اینجا کارت‌ها و اشراف‌زاده‌ها باید لود یا شفل شوند
        // برای تست می‌تونیم کارت نمونه بسازیم
    }
    public CardEntity GetCardById(Guid cardId)
    {
        return Level1Cards.Concat(Level2Cards).Concat(Level3Cards).FirstOrDefault(c => c.Id == cardId)
               ?? throw new InvalidOperationException("Card not found");
    }

    public void RemoveCard(CardEntity card)
    {
        if (!Level1Cards.Remove(card))
            if (!Level2Cards.Remove(card))
                Level3Cards.Remove(card);
    }

    public void RemoveNoble(NobleEntity noble) => Nobles.Remove(noble);

}