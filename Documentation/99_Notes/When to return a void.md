# Kiedy zwracać `void`, `Result<T>` i DTO

```text
Domain → najczęściej void + wyjątek
Application → Result<T>
Presenter → DTO / ViewModel
```

# Zasada odpowiedzialności

Każda warstwa komunikuje się inaczej.

| Warstwa     | Odpowiedzialność              | Zwraca           |
| ----------- | ----------------------------- | ---------------- |
| Domain      | reguły biznesowe              | `void` + wyjątek |
| Application | koordynacja przypadków użycia | `Result<T>`      |
| Presenter   | przygotowanie danych dla UI   | DTO / ViewModel  |

# Domain wykonuje reguły

Przykłady:

```text
Encounter
Player
CombatSession
Dungeon
```

Domena nie negocjuje.

Nie mówi:

```text
może się uda
```

tylko:

```text
operacja jest poprawna
```

albo:

```text
operacja łamie reguły
```

## Dobra praktyka

Domena:

```csharp
encounter.Start();

player.AddExperience(100);

combat.Finish();
```

Błąd:

```csharp
throw InvalidOperationException
```

## Przykład poprawny

```csharp
public void Start()
{
    if (State != EncounterState.Available)
        throw new InvalidOperationException();

    State = EncounterState.InProgress;
}
```

## Przykład niezalecany

```csharp
public Result Start()
{
    if (...)
        return Result.Fail();

    return Result.Success();
}
```

Dlaczego?

Bo każda encja zaczyna zachowywać się jak UseCase.

# Application koordynuje przepływ

Przykłady:

```text
StartCombatUseCase
ClaimCombatRewardUseCase
ResolveTurnUseCase
```

Application:

- pobiera dane,
- wywołuje domenę,
- obsługuje błędy,
- zwraca wynik.

## Dobra praktyka

```csharp
public Result<ClaimRewardResponse> Execute(...)
{
    try
    {
        encounter.MarkRewardClaimed();

        return Result.Success(...);
    }
    catch (...)
    {
        return Result.Fail(...);
    }
}
```

# Presenter tłumaczy wynik na UI

Przykłady:

```text
CombatPresenter
DungeonPresenter
```

Presenter:

- wywołuje UseCase,
- interpretuje wynik,
- aktualizuje widok.

## Przykład

```text
UI
↓
Presenter
↓
UseCase
↓
Result<T>
↓
Presenter
↓
View
```

# Nie duplikuj stanu

Unikaj:

```csharp
bool CanEnter;
EncounterState State;
```

bo:

```text
Locked
+
CanEnter=false
```

to dwa źródła prawdy.

Lepsze:

```csharp
public bool CanEnter =>
    State == EncounterState.Available;
```

# Nie przechowuj flag które wynikają z innych danych

Złe:

```csharp
RewardClaimed
+
State == RewardClaimed
```

Lepsze:

```csharp
public bool IsFinished =>
    State == EncounterState.RewardClaimed;
```

# Projektuj przejścia stanu, nie flagi

Zamiast:

```text
Completed=true
RewardClaimed=true
CanEnter=false
```

projektuj:

```text
Available
↓
InProgress
↓
Completed
↓
RewardClaimed
```

Każdy stan powinien wynikać z poprzedniego.

# Reguła do zapamiętania

```text
Domain wykonuje.

Application decyduje.

Presenter pokazuje.
```

Nie:

```text
Domain zarządza błędami.
```

Tylko:

```text
Domain pilnuje reguł.
Application reaguje.
```

