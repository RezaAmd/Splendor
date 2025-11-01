using Domain.Entities.Players;
using System.Numerics;

namespace Domain.Entities.Games;

public class GameEntity : BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public int RoundNumber { get; private set; } = 1;
    public List<PlayerEntity> Players { get; private set; } = new();
    public PlayerEntity CurrentPlayer => Players[_currentPlayerIndex];
    public BankEntity Bank { get; private set; }

    public bool IsStarted { get; private set; } = false;
    public bool IsFinished { get; private set; } = false;
    public long CreatedOn { get; private set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public Guid? WinnerId { get; private set; }

    private int _currentPlayerIndex = 0;

    public void AddPlayer(PlayerEntity player)
    {
        if (IsStarted)
            throw new InvalidOperationException("Cannot join after game has started.");

        if (Players.Count >= 4)
            throw new InvalidOperationException("Maximum 4 players allowed.");

        Players.Add(player);
    }

    public void Start()
    {
        if (Players.Count < 2)
            throw new InvalidOperationException("At least 2 players are required.");

        Bank.InitializeForPlayerCount(Players.Count);
        BoardEntity.Initialize();
        IsStarted = true;
        _currentPlayerIndex = 0;
    }

    // --- Player actions ---
    public void TakeGems(Guid playerId, GemType[] gems)
    {
        EnsureGameInProgress();
        EnsureTurn(playerId);

        Bank.Withdraw(gems);
        var player = GetPlayer(playerId);
        player.AddGems(gems);

        EndTurn();
    }

    public void BuyCard(Guid playerId, Guid cardId)
    {
        EnsureGameInProgress();
        EnsureTurn(playerId);

        var card = Board.GetCardById(cardId);
        var player = GetPlayer(playerId);

        if (!player.CanAfford(card))
            throw new InvalidOperationException("Player cannot afford this card.");

        player.PayFor(card);
        player.AddCard(card);
        Board.RemoveCard(card);

        CheckForNobleVisit(player);
        CheckForVictory(player);

        EndTurn();
    }

    public void ReserveCard(Guid playerId, Guid cardId)
    {
        EnsureGameInProgress();
        EnsureTurn(playerId);

        var card = Board.GetCardById(cardId);
        var player = GetPlayer(playerId);

        player.ReserveCard(card);
        Board.RemoveCard(card);

        // Player receives 1 gold if available
        if (Bank.HasGem(GemType.Gold))
            player.AddGem(GemType.Gold);

        EndTurn();
    }

    // --- Turn management ---
    private void EndTurn()
    {
        if (IsFinished) return;

        _currentPlayerIndex = (_currentPlayerIndex + 1) % Players.Count;

        if (_currentPlayerIndex == 0)
            RoundNumber++;
    }

    // --- Game rules ---
    private void CheckForNobleVisit(PlayerEntity player)
    {
        var nobles = Board.Nobles.Where(n => player.MeetsRequirements(n)).ToList();
        foreach (var noble in nobles)
        {
            player.AddNoble(noble);
            Board.RemoveNoble(noble);
        }
    }

    private void CheckForVictory(PlayerEntity player)
    {
        if (player.PrestigePoints >= 15)
        {
            IsFinished = true;
            WinnerId = player.Id;
        }
    }

    // --- Helpers ---
    private void EnsureGameInProgress()
    {
        if (!IsStarted || IsFinished)
            throw new InvalidOperationException("Game not in progress.");
    }

    private void EnsureTurn(Guid playerId)
    {
        if (CurrentPlayer.Id != playerId)
            throw new InvalidOperationException("Not your turn.");
    }

    private PlayerEntity GetPlayer(Guid playerId)
        => Players.First(p => p.Id == playerId);
}
