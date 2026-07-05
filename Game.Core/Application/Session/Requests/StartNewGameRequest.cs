// przesyłane z UI przy tworzeniu postaci
public record StartNewGameRequest(string PlayerName, PlayerType PlayerType)
    : ARCreatePlayer(PlayerName, PlayerType);
