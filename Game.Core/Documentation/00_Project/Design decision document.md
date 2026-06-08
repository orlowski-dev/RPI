## Cel projektu

Celem projektu jest wykonanie gry RPG typu roguelite w technologii Godot 4.6 oraz .NET 8.0.

Projekt realizowany jest głównie przez jednego programistę i stanowi projekt zaliczeniowy przedmiotu.

Założeniem projektu jest osiągnięcie poziomu funkcjonalnego odpowiadającego ocenie 5.0 przy zachowaniu możliwości ograniczenia zakresu w przypadku ryzyka niedostarczenia rozwiązania.

Do produkcji wykorzystywane będą gotowe zasoby:

- UI,
- sprites,
- tilesety,
- efekty dźwiękowe,
- elementy graficzne.

Dokument Game Design Document pozostaje źródłem wymagań funkcjonalnych.

# Stack technologiczny

## Silnik

Godot 4.6

## Backend logiki

.NET 8.0

## Język

C#

## Testy

xUnit

## Dokumentacja

Markdown w repozytorium projektu

## Zarządzanie projektem

Git + short-lived branches

Model pracy:

```
main
devel
feature/*
```

Przykład:
`feature/32-example -> devel -> main`

# Architektura rozwiązania

Projekt oparty jest o rozdzielenie logiki gry od warstwy prezentacji.

## Projekty

### Game.Core

Czysta logika gry.

Brak zależności od Godot.

Odpowiedzialności:

- domena,
- reguły gry,
- walka,
- loot,
- progresja,
- zapis,
- generacja,
- system questów.

### Game.Node

Warstwa integracyjna i prezentacyjna.

Odpowiedzialności:

- sceny,
- UI,
- assety,
- integracja z Godot,
- prezentery.

### Game.Tests

Testy jednostkowe.

Zakres:

- wyłącznie Game.Core.

# Założenia architektoniczne

## Separacja

Game.Core nie może zawierać:

- Node,
- SceneTree,
- Resource,
- sygnałów Godota,
- klas silnika.

## Komunikacja

UI

Presenter

Application

Domain

EventBus

UI

Singletony nie są używane jako główny mechanizm komunikacji.

Dopuszczony jest prosty kontener usług.

# Wzorce projektowe

## Factory

Tworzenie:

- przeciwników,
- przedmiotów,
- postaci.

## Strategy

Implementacja:

- efektów statusowych.

## Observer

Obsługa:

- zdarzeń domenowych.

## State

Obsługa:

- przebiegu walki.

# Model danych

## Statystyki

Statystyki liczone dynamicznie.

Wzór:

FinalStats =  
Base +  
Level +  
Equipment +  
Effects

Nie przechowujemy aktualnych statystyk.

## Przedmioty

Przedmiot po wygenerowaniu jest niezmienny.

Proces:

Drop

Generate

Freeze

Inventory

Poziom postaci nie modyfikuje istniejących przedmiotów.

# Walka

Model:  
oddzielna scena walki.

Przepływ:

SafeHouse

Dungeon

Encounter

CombatSession

CombatResult

Dungeon

Założenia:

- gracz zawsze rozpoczyna walkę,
- użycie przedmiotu działa natychmiast,
- jedna akcja na turę,
- wynik walki aktualizuje stan świata.

# Dungeon

Model:  
jeden duży dungeon.

Struktura:

- pokoje standardowe,
- jeden boss.

Założenia:

- po pokonaniu bossa możliwy powrót do miasta,
- przejście dalej generuje nowy dungeon,
- nie można opuścić dungeonu wcześniej.

Po śmierci:

- zapisujemy aktualny stan wygenerowanego świata,
- zapisujemy stan pokojów.

Przykładowe dane pokoju:

- przeciwnicy,
- loot,
- beczki,
- skrzynie,
- interakcje.

# System śmierci

Po śmierci gracz:

- wraca do miasta,
- traci postęp bieżącej wyprawy,
- zachowuje część złota,
- zachowuje losowe przedmioty.

# Zapis gry

Model:  
podział na pliki.

Struktura:

save/

slot1/

player.json

world.json

inventory.json

meta.json

Zakres:

player

- level
- xp
- statystyki

world

- dungeon
- pokoje
- questy

inventory

- przedmioty

meta

- czas
- zgony
- wersja

Nie wykorzystujemy seedów.

Zapisujemy pełny stan świata.

# Testowanie

Testowany jest wyłącznie Game.Core.

Zakres:

- Combat,
- Loot,
- Inventory,
- Progression,
- Save,
- Dungeon.

Nie testujemy:

- UI,
- scen,
- Godot lifecycle.

# Dokumentacja

Dokumentacja przechowywana w repozytorium jako vault Obsidiana.

Struktura:

```
docs/

00_Project

01_Architecture

02_GameDesign

03_Development

04_Testing

05_Final

assets
```

Dokumentacja obejmuje:

- ADR,
- changelog,
- sprinty,
- UML,
- screeny,
- opis systemów.

