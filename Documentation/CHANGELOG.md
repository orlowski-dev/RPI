# Dziennik zmian

## 04.06.2026 Refaktoryzacja definicji przeciwników i porządkowanie konfiguracji CoreService

### Co zmieniono

- Przeniesiono definicje typów przeciwników (`EnemyType`) z `EnemyCharacter` do dedykowanego pliku `.Core:Enums/EnemyType`.
- Rozszerzono enum `EnemyType` o konkretne typy przeciwników:
  - `Zombie`
  - `Biegacz`
  - `Zboj`
  - `Brutal`

- Przeniesiono statyczny słownik `EnemyTypes` z klasy `EnemyCharacter` do `.Core:CoreService`.
- `CoreService` pełni teraz również rolę centralnego magazynu konfiguracji przeciwników obok istniejących konfiguracji klas postaci i generatora lochów.
- Uporządkowano strukturę inicjalizacji danych w `CharacterClasses`.
- Usunięto nieużywane pliki projektowe:
  - `Game.Node/RPI.csproj.old`
  - `Game.Node/RPI.csproj.old.1`
  - `Game.Node/RPI.csproj.old.2`

### Dlaczego

- Aby rozdzielić definicje danych od logiki encji i poprawić organizację kodu.
- Przeniesienie `EnemyType` do osobnego pliku upraszcza ponowne użycie typu w innych modułach bez zależności od klasy `EnemyCharacter`.
- Centralizacja konfiguracji przeciwników w `CoreService` utrzymuje spójny sposób zarządzania danymi gry (analogicznie do `CharacterClasses` i konfiguracji generatora lochów).
- Usunięcie starych plików projektowych ogranicza ilość nieaktualnych artefaktów w repozytorium.

### Zmiany w plikach

- `Game.Core/Characters/EnemyCharacter.cs` – usunięcie definicji `EnemyType` i słownika `EnemyTypes`.
- `Game.Core/Enums/EnemyType.cs` – wydzielenie oraz rozszerzenie enum przeciwników.
- `Game.Core/Services/CoreService.cs` – dodanie centralnej konfiguracji `EnemyTypes` i uporządkowanie danych.
- `Game.Node/RPI.csproj.old*` – usunięcie nieużywanych plików projektu.

## 21.05.2026 Integracja generatora lochu z systemem spawnu gracza

### Co zmieniono

- Zaktualizowano ścieżkę sceny lochu w `GameService` na nową wersję z generowanym lochem (`GeneratedDungeonScene.tscn`).
- Rozszerzono `DungGeneratorController` o funkcjonalność automatycznego spawnu gracza i kamery po wygenerowaniu mapy.
- Dodano konfigurację punktu startowego (`_playerSpawnPointPos`) w kontrolerze.
- Zaimplementowano metodę `SpawnPlayer()` wykorzystującą `GameManager` do instancjonowania postaci i kamery.
- Dodano kolizje fizyczne (`physics_layer_0`) do kafelków w `DungeonTilesetTemp.tres` (polygon points dla ścian i podłóg).

### Dlaczego

- Aby umożliwić graczowi natychmiastową rozgrywkę w nowo wygenerowanym lochu bez konieczności ręcznego dodawania obiektów do sceny.
- Zmiana ścieżki sceny w `GameService` zapewnia, że gra ładuje wersję z procedurally generated dungeon zamiast statycznego poziomu.
- Dodanie fizyki do tilesetu jest niezbędne do poprawnego działania kolizji postaci z murami lochu.

### Zmiany w plikach

- `Game.Core/Services/GameService.cs` - aktualizacja mapy scen.
- `Game.Node/Scripts/Controllers/DungGeneratorController.cs` - dodanie logiki spawnu.
- `Game.Node/Assets/Tilesets/DungeonTilesetTemp.tres` - dodanie danych kolizyjnych.

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
