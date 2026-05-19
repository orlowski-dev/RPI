# Dziennik zmian

## 19.05.2026 Przeniesienie CharacterClasses do CoreService

### Zmiany

- `.Core:CoreService` jest teraz magazynem danych i konfigów.
- Przyciski w CharacterCreatorHUD są teraz pobierane przez skrypt poprzez unikatowe nazwy (`%nazwa`).

## 17.05.2026 Dodanie statycznej klasy CoreService jako centralnego magazynu danych

### Dlaczego

Potrzebny był globalny dostęp do konfiguracji generatora lochu oraz mapy tekstur kafelków bez konieczności tworzenia instancji czy dodawania do autoload jako Node.

### Zmiany

- utworzony plik `.Core:CoreService` - statyczna klasa przechowująca:
  - `DungeonGeneratorConfig` - domyślna konfiguracja generatora
  - `DungeonTiles` - mapa typów kafelków na listy tekstur
  - `GetRandomDungTile()` - metoda pomocnicza do losowania wariantów
- usunięto potrzebę tworzenia instancji dla danych konfiguracyjnych
- zapewniono dostęp do danych przez wywołanie `CoreService.PropertyName`

## 17.05.2026 Dodanie systemu generowania lochów (Dungeon Generator)

### Dlaczego

Potrzebny był moduł do proceduralnego generowania lochów składających się z pokoi połączonych korytarzami, z zachowaniem podziału na warstwy `Game.Core` (logika) i `Game.Node` (integracja z Godot).

### Zmiany

- utworzony plik `.Core:DungGeneratorService` - logika generowania pokoi, korytarzy i mapy kafelków
- utworzony plik `.Node:DungGeneratorController` - kontroler podpięty do sceny `GeneratedDungeonScene`, rysujący kafelki na `TileMapLayer`
- utworzony plik `.Core:DungGeneratorConfig` - struktura konfiguracyjna generatora
- utworzony plik `.Core:DungRoomData` - dane wygenerowanego pokoju
- utworzony plik `.Core:DungTileType` - wylicznik typów kafelków lochu
- utworzony plik `.Core:Point` - struktura współrzędnych
- utworzony plik `.Core:Size` - struktura wymiarów
- utworzony plik `.Core:CoordMapper` - narzędzie mapowania `Point` ↔ `Vector2I`
- rozszerzony plik `.Core:CoreService` o `DungeonGeneratorConfig`, `DungeonTiles` i `GetRandomDungTile()`
- dodana dokumentacja systemu do `Dokumentacja projektu.md`

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
