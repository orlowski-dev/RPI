- dodać logo
- spis osób

---

## Wprowadzenie

Niniejszy dokument jest dokumentacją gry RPG typu roguelite, która jest produkcją 2D z widokiem top-down, inspirowaną mechanikami znanymi z gier.

## Opis gry

Gracz eksploruje losowo generowane lochy, walczy z przeciwnikami w systemie turowym, zdobywa loot, złoto i rozwija swoją postać. Po śmierci postaci, gracz wraca do miasta (safe house), zachowując część postępów. Gracz kończy grę po ukończeniu pięciu lochów.

## Zespół

- dodać osoby i role tabelkę

---

## Założenia ogólne i stack techniczny

- **Język programowania i technologia**: C# .NET 8.0
- **Silnik gry**: Godot Engine v4.6.1 (Mono)
- **Edytor markdown**: Obsidian

## Zarządzanie projektem

- **system kontroli wersji**: git
- **repozytorium**: [GitHub](https://github.com/orlowski-dev/RPI)
- **zarządzanie zadaniami zespołu**: [Trello](https://trello.com/b/MF4QbxrJ/rpi)

---

## BaseCharacter

Bazowa klasa dla wszystkich postaci w grze.  
Definiuje podstawowe statystyki używane w systemie walki.

```csharp
public abstract partial class BaseCharacter : Resource
```

### Opis

`BaseCharacter` jest klasą abstrakcyjną przechowującą wspólne właściwości postaci oraz  
podstawową logikę związaną z:

- obrażeniami
- leczeniem
- aktualizacją HP
- sygnałami zmian HP
### Dziedziczenie


```bash
Resource <-- PlayerCharacter
```


### Sygnały

| Nazwa     | Parametr | Opis                                  |
| --------- | -------- | ------------------------------------- |
| HpChanged | int hp   | Wywoływany przy zmianie punktów życia |

### Właściwości

| Właściwość | Typ    | Dostęp   | Opis                              |
| ---------- | ------ | -------- | --------------------------------- |
| Name       | string | readonly | Nazwa postaci                     |
| MaxHP      | int    | readonly | Maksymalne punkty życia           |
| HP         | int    | readonly | Aktualne punkty życia             |
| Attack     | int    | readonly | Siła ataku                        |
| Defense    | int    | readonly | Obrona                            |
| Luck       | int    | readonly | Szczęście                         |
| CritChance | int    | readonly | Szansa na trafienie krytyczne (%) |

### Konstruktory

| Parametr   | Typ    | Opis                          |
| ---------- | ------ | ----------------------------- |
| name       | string | Nazwa postaci                 |
| maxHp      | int    | Maksymalne HP                 |
| attack     | int    | Siła ataku                    |
| defense    | int    | Obrona                        |
| luck       | int    | Szczęście                     |
| critChance | int    | Szansa na trafienie krytyczne |

### Metody

#### TakeDamage

```csharp
public virtual void TakeDamage(int amount)
```

Zmniejsza HP postaci o podaną wartość.

##### Parametry

| Parametr | Typ | Opis          |
| -------- | --- | ------------- |
| amount   | int | Ilość obrażeń |

#### Heal

```csharp
public virtual void Heal(int amount)
```

Leczy postać o podaną wartość.

##### Parametry

| Parametr | Typ | Opis           |
| -------- | --- | -------------- |
| amount   | int | Ilość leczenia |

---

## PlayerCharacter

Reprezentuje postać gracza w grze.

```csharp
public partial class PlayerCharacter : BaseCharacter
```

### Opis

`PlayerCharacter` przechowuje podstawowe statystyki gracza.

### Dziedziczenie


```bash
Resource <-- BaseCharacter <-- PlayerCharacter
```

### Statystyki bazowe (dziedziczone)

| Właściwość | Typ    | Dostęp   | Opis                              |
| ---------- | ------ | -------- | --------------------------------- |
| Name       | string | readonly | Nazwa postaci                     |
| MaxHP      | int    | readonly | Maksymalne punkty życia           |
| HP         | int    | readonly | Aktualne punkty życia             |
| Attack     | int    | readonly | Siła ataku                        |
| Defense    | int    | readonly | Obrona                            |
| Luck       | int    | readonly | Szczęście                         |
| CritChance | int    | readonly | Szansa na trafienie krytyczne (%) |

### Statystyki gracza

| Właściwość | Typ | Dostęp   | Opis                                     |
| ---------- | --- | -------- | ---------------------------------------- |
| Level      | int | readonly | Poziom postaci gracza                    |
| Exp        | int | readonly | Aktualne punkty doświadczenia            |
| ExpNextLvl | int | readonly | Wymagana ilość EXP do następnego poziomu |

### Konstruktor

| Parametr   | Typ    | Opis                          |
| ---------- | ------ | ----------------------------- |
| name       | string | Nazwa postaci                 |
| maxHp      | int    | Maksymalne HP                 |
| attack     | int    | Siła ataku                    |
| defense    | int    | Obrona                        |
| luck       | int    | Szczęście                     |
| critChance | int    | Szansa na trafienie krytyczne |

### Metody

#### CalculateExpToNextLevel

```csharp
private int CalculateExpToNextLevel()
```

Oblicza ilość doświadczenia wymaganą do osiągnięcia następnego poziomu.

#### LevelUp

```csharp
private void LevelUp()
```

Zwiększa poziom postaci oraz aktualizuje wymagane doświadczenie do następnego poziomu.

#### AddExp

```csharp
public void AddExp(int amount)
```

Dodaje punkty doświadczenia i zwiększa Level jeśli potrzeba.

##### Parametry

| Parametr | Typ | Opis                 |
| -------- | --- | -------------------- |
| amount   | int | Ilość Exp do dodania |

---

## PlayerController

Odpowiada za sterowanie postacią gracza.

```csharp
public partial class PlayerController : CharacterBody2D
```

### Dziedziczenie

```bash
CharacterBody2D <-- PlayerController
```

Klasa dziedziczy po `CharacterBody2D`. Wykorzystuje system fizyki.
Obsługuje:

- ruch gracza
- rotację
- fizykę ruchu
- pobieranie inputu

### Parametry

| Parametr      | Typ | Opis             |
| ------------- | --- | ---------------- |
| Speed         | int | Prędkość ruchu   |
| RotationSpeed | int | Prędkość rotacji |

### Metody

#### MovePlayer()

```csharp
private void MovePlayer(ref double delta)
```

Metoda odpowiedzialna za:

- pobieranie wejścia gracza,
- poruszanie się postacią,
- płynną rotację postaci,
- wywołanie fizyki ruchu.

Pobiera kierunek z `InputMap`. Akcje:

- moveLeft
- moveRight
- moveUp
- moveDown

Oblicza rotację dla domyślnej orientacji sprajta w górę.

---

## PlayerRunCamera

Odpowiada za śledzenie gracza w lochach. Kamera płynnie podąża za postacią gracza.

```csharp
public partial class PlayerRunCamera : Camera2D
```

### Opis

Kamera automatycznie wyszukuje obiekt `Player` w drzewie sceny.

### Dziedziczenie

```bash
Camera2D <-- PlayerRunCamera
```

Klasa dziedziczy po `Camera2D` i automatycznie wyszukuje obiekt `Player` w drzewie sceny.

> [!CAUTION]
> Jeżeli Player nie zostanie znaleziony to wypisze błąd w konsoli i skrypt zostanie przerwany.
>
> -   player node musi mieć nazwę "Player"
> -   kamera musi być w tym samym `RootNode`

### Parametry

| Parametr        | Typ    | Dostęp     | Opis                         |
| --------------- | ------ | ---------- | ---------------------------- |
| [E] CameraDelay | int    | public set | Płynność ruchu kamery        |
| \_playerNode    | Node2D | private    | Referencja do obiektu gracza |

### Metody

#### MoveCamera()

```csharp
private void MoveCamera(ref double delta)
```

Metoda odpowiada za:

- śledzenie gracza,
- płynny ruch kamery.

---

## BaseSingleton

Bazowa klasa singleton dla systemów globalnych w grze.

```csharp
public abstract partial class BaseSingleton<T> : Node where T : Node
```

### Dziedziczenie

```bash
Node <-- BaseSingleton
```

### Opis

`BaseSingleton<T>` to generyczna klasa abstrakcyjna implementująca wzorzec **Singleton**  
dla klas dziedziczących po `Node`.

Klasa automatycznie:

- pilnuje jednej instancji
- ustawia `Instance`
- usuwa duplikaty
- czyści referencję przy usuwaniu node

Klasa implementuje:

- Singleton Pattern
- Generic Singleton

### Właściwości

| Właściwość | Typ | Dostęp | Opis                          |
| ---------- | --- | ------ | ----------------------------- |
| Instance   | T   | static | Globalna instancja singletona |

### Przykład użycia

```csharp
public partial class GameController : BaseSingleton<GameController>
{
	public override void _EnterTree()
    {
        base._EnterTree();
        // dalsza implementacja
    }
}
```

---

## GameController

Główny kontroler gry.

```csharp
public partial class GameController : BaseSingleton<GameController>
```

### Dziedziczenie

```bash
Node <-- BaseSingleton <-- GameController
```

### Opis

Singleton zarządzający logiką gry. Zapewnia globalny dostęp do kontrolera gry.

Klasa wykorzystuje:

- Singleton Pattern
- EventBus (`Signals`)
- Globalny stan gry (`GameState`)

Odpowiada za:

- zarządzanie stanem gry
- przechowywanie danych gracza
- start nowej gry
- komunikację globalną

### Właściwości

| Właściwość      | Typ             | Dostęp   | Opis                   |
| --------------- | --------------- | -------- | ---------------------- |
| Instance        | GameController  | readonly | Singleton              |
| PlayerCharacter | PlayerCharacter | readonly | Aktualna postać gracza |
| GameState       | GameState       | readonly | Aktualny stan gry      |
| \_signals       | Signals         | private  | Referencja do EventBus |

### Metody

#### OnSetGameState

```csharp
private void OnSetGameState(GameState newState)
```

Obsługuje zmianę stanu gry.

---

## Signals

Globalny EventBus aplikacji.

```csharp
public partial class Signals : BaseSingleton<Signals>
```

### Opis

Centralny system komunikacji pomiędzy modelami i kontrolerami.

### Dziedziczenie

```bash
Node <-- BaseSingleton <-- Signals
```


### Sygnały

#### SetGameState

```csharp
[Signal]
public delegate void SetGameStateEventHandler(GameState newState);
```

Zmienia stan gry.

### Metody

#### EmitSetGameState

```csharp
public void EmitSetGameState(GameState newState)
```

Emituje sygnał zmiany stanu gry.

---

## Enums

### GameState

Enum określający aktualny stan gry.
