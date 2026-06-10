# Changelog

## Combat State Machine Foundation

### Added

#### Combat Domain

- dodano model `CombatSession`
- dodano model `CombatContext`
- dodano model `CombatParticipant`
- dodano typ `CombatParticipantType`
- dodano model statystyk `CombatStats`
- dodano model wyniku walki `CombatReward`
- dodano abstrakcję `CombatAction`

#### State Machine

- dodano interfejs `ICombatState`
- dodano `CombatStateMachine`
- dodano `CombatStateTransition`
- dodano `CombatStateType`
- przygotowano obsługę przejść stanów:
  - Enter
  - Update
  - Exit

- dodano rejestr stanów oparty o `Dictionary<CombatStateType, ICombatState>`

#### Combat Flow

- przygotowano przepływ walki oparty o State Pattern
- rozdzielono odpowiedzialności:
  - CombatSession
  - CombatStateMachine
  - CombatAction

- dodano obsługę:
  - aktywnego uczestnika
  - wyboru akcji
  - wykonania akcji
  - zakończenia walki

#### Results

- dodano model `Result`
- dodano `Result<T>`
- dodano `Error`
- dodano `ErrorType`

#### Tests

- dodano testy jednostkowe `CombatSession`
- dodano fixture tworzącą przykładową sesję walki
- dodano scenariusze:
  - inicjalizacji sesji
  - wyboru akcji
  - wykonania akcji
  - zmiany aktywnego uczestnika
  - zakończenia walki
  - walidacji pustej akcji

### Changed

- `CombatSession` przestał odpowiadać za przejścia stanów
- wykonanie akcji zostało zdelegowane do `CombatAction`
- przygotowano architekturę pod implementację:
  - PlayerTurnState
  - EnemyTurnState
  - RewardState

### Architecture

- zachowano podział:
  - Domain
  - Application
  - Infrastructure

- logika walki pozostała w `Game.Core`
- brak zależności od Godot
- przygotowano fundament pod UseCase i EventBus

### Notes

- brak integracji z EventBus
- brak integracji z Save
- brak implementacji reward calculation
- UseCase pozostają w warstwie Application
- AI przeciwników niezaimplementowane
- przejścia stanów przygotowane pod dalszą implementację

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

## Gameplay Loop

Dodano projekt głównej pętli rozgrywki.

Zakres:

- zdefiniowano stany gry,
- dodano podstany SafeHouse,
- dodano podstany Combat,
- opisano przepływ śmierci,
- opisano Continue Mode,
- przygotowano diagram UML.

## Init

Dodano pierwsze decyzje architektoniczne:

- separacja Core
- Combat Scene
- JSON Save
