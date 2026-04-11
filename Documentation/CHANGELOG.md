# Dziennik zmian

## 11.04.2026 Zmiany w loggerze

- `Logger` jest teraz Singletonem (`Game.Node/Signletons/Logger.cs`)

```cs
public partial class Logger : BaseSingleton<Logger>, ILogger
```

- `Logger` wykorzystuje interfejs `ILogger` (`Game.Core/Interfaces/ILogger.cs`)

```cs
public interface ILogger
```

- `Logger` do `.Core` przekazywany jest przez `dependency injection`

```cs
// Klasa w Game.Core
public partial class Example {
  private readonly ILogger? _loggeer;

  public Example(ILogger? logger=null) {
    _logger = logger;
  }

  public void DoStaff() {
    _logger?.Write(...);
  }
}

// Wywołanie metody ze skryptu z Game.Core w Game.Node
public partial class ExampleNode {
  private Logger _logger => Logger.Instance;

  private Example _example = new Example(logger: _logger);
}

// Wywołanie metody Logger w skrypcie w Game.Node
public partial class ExampleNode {
  private Logger _logger => Logger.Instance;

  private void DoStaff() {
    _logger.Write(...);
  }
}
```

---
