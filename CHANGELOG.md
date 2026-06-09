# Changelog

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
