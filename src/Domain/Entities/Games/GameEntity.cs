namespace Domain.Entities.Games;

public class GameEntity : BaseEntity
{
    public int RoundNumber { get; private set; } = 1;
    public bool IsStarted { get; private set; } = false;
    public bool IsFinished { get; private set; } = false;
    public long CreatedOn { get; set; }
    public Guid? WinnerId { get; private set; }
}
