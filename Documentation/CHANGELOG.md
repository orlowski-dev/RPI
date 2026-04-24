# Dziennik zmian

## 24.04.2026 Dodanie Heal(999) w levelUp po dodaniu statystyk

### Dlaczego

Dlatego aby gracz po levelUp miał Hp = MaxHp.

## 18.04.2026 Rozszerzenie klasy CharacterClass

### Dlaczego

Potrzebne były bazowe statystyki klas postaci.

## 12.04.2026 GameController to teraz GameManager

### Dlaczego

Głównie żeby zachować spójność w nazewnictwie i rozdzielić logikę od wartwy `.Node`.

### Zmiany

- plik `.Node:GameController` usunięty
- plik `.Node:GameManager` utworzony jako `Singleton` - dodany do autoload
- utworzony został interfejs `.Core:IGameManagerData` potrzebny do definicji typu danych przesyłanych przez `.Node:Signals` w interfejsie `.Core:ISignals` (bo `.Core` nie zna `.Node`, a przesyłane dane przez sygnały muszą dziedziczyć po `Godot:GodotObject`)
- zmiany w plikach, które korzystały ze starego `.Node:GameController`

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
