# Changelog

## CombatStateMachine (Unfinished)

### Added

#### Combat

- zaimplementowano model `CombatParticipant`
- dodano identyfikację uczestnika (`Id`, `SourceId`)
- dodano typ uczestnika (`CombatParticipantType`)
- dodano obsługę stanu życia (`CurrentHp`, `IsAlive`)
- dodano model statystyk walki (`CombatStats`)
- dodano operacje:
  - `ReceiveDamage`
  - `Heal`

- rozszerzono `CombatReward`
- dodano obsługę:
  - doświadczenia (`Experience`)
  - złota (`Gold`)
  - listy nagród (`ItemInstanceIds`)

#### State Machine

- dodano rejestr stanów oparty o `Dictionary<CombatStateType, ICombatState>`
- dodano śledzenie aktywnego stanu (`Current`)
- dodano mechanizm zmiany stanu (`Change`)
- dodano obsługę wejścia i wyjścia ze stanu:
  - `Enter`
  - `Exit`
- przygotowano mechanizm aktualizacji stanu (`Update`)
- dodano walidację uruchomienia maszyny stanów

#### Tests

- dodano testy jednostkowe `CombatSession`
- dodano fixture tworzącą przykładową sesję walki
- dodano testy:
  - inicjalizacji sesji
  - wyboru akcji
  - wykonania akcji
  - przełączania tur
  - zakończenia walki
  - walidacji wykonania pustej akcji

### Changed

- `CombatParticipant` stał się pełnoprawną encją domenową
- `CombatReward` przechowuje rzeczywiste dane nagród
- `CombatStateMachine` otrzymał szkielet obsługi przejść stanów
- `CombatSession` posiada pokrycie testami scenariuszy podstawowych

### Notes

- `CombatStateMachine.Start()` nadal nie posiada implementacji
- `CombatStateMachine.Update()` nie wykonuje jeszcze faktycznego przejścia po otrzymaniu `CombatStateTransition`
- brak integracji State Machine z EventBus
- brak rozliczania rewardów w Application

## CombatStateMachine (Unfinished)

### Added

#### Combat

- dodano `Combat State Machine`
- dodano `ICombatState`
- dodano `CombatStateTransition`
- dodano `CombatContext`
- dodano `CombatSession`
- dodano `CombatParticipant`
- dodano `CombatReward`
- dodano obsługę wyboru i wykonywania akcji

#### Results

- dodano `Result`
- dodano `Error`
- dodano `ErrorType`

#### Architecture

- rozdzielono odpowiedzialności:
  - `CombatSession`
  - `CombatStateMachine`
  - `CombatAction`

- przygotowano model pod:
  - status effects
  - reward calculation
  - event integration

#### Documentation

- dodano dokument przepływu walki
- opisano wzorzec State
- opisano przepływ sesji walki

### Changed

- sesja walki nie zarządza przejściami stanów
- wykonanie akcji delegowane do `CombatAction`
- walka przygotowana pod rozszerzalne typy uczestników

### Notes

- brak integracji z `EventBus`
- brak integracji z `Save`
- brak integracji z `UI`

## Combat Domain

dodano:
-opis agregatu `CombatSession`

- definicja `CombatParticipant`
- definicja `CombatAction`
- definicja `CombatReward`
- model `StatusEffect`

Zdefiniowano:

- granice odpowiedzialności pomiędzy Domain i Presentation
- brak zależności od Godot w warstwie Core
- przygotowanie pod implementację Combat State Machine

architektura:

- zgodne z architekturą Game.Core -> Application -> Presenter
- przygotowanie pod testy jednostkowe
- wydzielenie odpowiedzialności Combat
- przygotowanie pod State Pattern
- przygotowanie pod testy Game.Tests

## Application Model

Zaprojektowano warstwę Application.

Zakres:

- use case’y,
- queries,
- DTO,
- Result,
- Presenter,
- nawigacja,
- DI,
- integracja z eventami.

## Domain Model

Zaprojektowano model domenowy.

Zakres:

- agregaty,
- encje,
- statystyki dynamiczne,
- model przedmiotów,
- model walki.

## Save Model

Dodano model zapisu.

Zakres:

- player.json,
- world.json,
- itemDefinition.json,
- itemInstacne.json,
- algorytm item generation
- inventory.json
- events.json
- settings.json
- algorytm odczyt zapisu,
- algorytm zapis gry
- algorytm trybu continue

## Event Architecture

Zaprojektowano komunikację systemową.

Zakres:

- event flow,
- event history,
- model pub/sub,
- rozdzielenie Domain i Application,
- zasady obsługi błędów.

## Combat System Design

Zaprojektowano przepływ walki.

Zakres:

- tury,
- statusy,
- itemy,
- obrona,
- ucieczka,
- ekran nagród,
- ekran śmierci.

---

## Gameplay Loop

Dodano projekt głównej pętli rozgrywki.

Zakres:

- zdefiniowano stany gry,
- dodano podstany SafeHouse,
- dodano podstany Combat,
- opisano przepływ śmierci,
- opisano Continue Mode,
- przygotowano diagram UML.

---

## Init

Dodano pierwsze decyzje architektoniczne:

- separacja Core
- Combat Scene
- JSON Save
