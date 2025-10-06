namespace Domain.Entities.Cards;

public class NobleTileEntity : BaseEntity
{
    public int Points { get; set; } = 3;
    public virtual ICollection<CardCostEntity> Costs { get; set; }
        = new List<CardCostEntity>();
}