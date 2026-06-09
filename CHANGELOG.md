# Changelog

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
