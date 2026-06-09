## Cel dokumentu

Dokument definiuje stany gry oraz dozwolone przejścia pomiędzy nimi.

Gameplay State Machine stanowi źródło prawdy dla:

- scen,
- przepływu danych,
- zapisu gry,
- obsługi eventów,
- przełączania ekranów.

# Założenia

Gra działa jako zestaw stanów.

W danym momencie aktywny jest dokładnie jeden stan główny.

Wyjątek:  
stan Pause może zostać nałożony na wybrane stany.

# Stany

## MainMenu

Stan początkowy.

Odpowiedzialność:

- uruchomienie gry,
- wybór zapisu,
- przejście do tworzenia postaci.

Dozwolone przejścia:

- CharacterSelection

## CharacterSelection

Tworzenie postaci.

Odpowiedzialność:

- wybór klasy,
- rozpoczęcie nowej gry.

Dozwolone przejścia:

- SafeHouse

## SafeHouse

Miasto.

Odpowiedzialność:

- zapis gry,
- sklep,
- zarządzanie ekwipunkiem,
- rozwój postaci,
- odbiór nagród.

Dozwolone przejścia:

- DungeonGeneration
- Pause

Operacje:

- auto-save przy wejściu
- manual save

## DungeonGeneration

Przygotowanie wyprawy.

Odpowiedzialność:

- wygenerowanie nowego dungeonu,
- przygotowanie pokojów,
- inicjalizacja wydarzeń.

Dozwolone przejścia:

- Dungeon

Stan techniczny.

Gracz nie wykonuje akcji.

## Dungeon

Eksploracja.

Odpowiedzialność:

- poruszanie,
- interakcje,
- pułapki,
- skrzynie,
- wybór pokoju.

Dozwolone przejścia:

- CombatPreparation
- Pause

## CombatPreparation

Przejście do walki.

Odpowiedzialność:

- zapis stanu pokoju,
- przygotowanie przeciwników,
- utworzenie sesji walki,
- załadowanie sceny.

Dozwolone przejścia:

- Combat

Stan techniczny.

## Combat

Turowa walka.

Odpowiedzialność:

- wykonywanie tur,
- użycie umiejętności,
- użycie przedmiotów,
- efekty statusowe.

Dozwolone przejścia:

- Reward
- Death
- Pause

Założenia:

- gracz zaczyna,
- jedna akcja na turę,
- item działa natychmiast.

## Reward

Ekran podsumowania walki.

Odpowiedzialność:

- pokazanie nagród,
- przyznanie EXP,
- przyznanie złota,
- przyznanie przedmiotów.

Dozwolone przejścia:

- Dungeon
- Victory

## Death

Ekran śmierci.

Odpowiedzialność:

- podsumowanie,
- utrata postępu,
- wybór zachowanych nagród.

Operacje:

- powrót do miasta,
- auto-save.

Dozwolone przejścia:

- SafeHouse

## Victory

Zakończenie etapu.

Odpowiedzialność:

- podsumowanie wyprawy,
- decyzja o kontynuacji.

Dozwolone przejścia:

- SafeHouse
- ContinueMode

Warunek:  
boss pokonany.

## ContinueMode

Kontynuacja gry.

Odpowiedzialność:

- wygenerowanie kolejnego dungeonu.

Dozwolone przejścia:

- DungeonGeneration

## Pause

Stan pomocniczy.

Odpowiedzialność:

- zatrzymanie gry.

Dostępny z:

- SafeHouse
- Dungeon
- Combat

Dozwolone przejścia:

- poprzedni stan

# Hierarchiczne stany

Niektóre stany posiadają podstany.

Podstan nie zmienia głównego przebiegu gry.

Po zamknięciu następuje powrót do stanu nadrzędnego.

## SafeHouse

Podstany:

### Inventory

Odpowiedzialność:

- przeglądanie ekwipunku,
- zakładanie przedmiotów,
- zdejmowanie przedmiotów,
- używanie przedmiotów.

Dozwolone przejścia:

- SafeHouse

Uwagi:

- nie uruchamia save,
- nie zmienia sceny.

### Character

Odpowiedzialność:

- rozwój postaci,
- wydawanie punktów,
- przegląd statystyk.

Dozwolone przejścia:

- SafeHouse

### Shop

Odpowiedzialność:

- kupno przedmiotów,
- sprzedaż przedmiotów,
- generacja oferty.

Dozwolone przejścia:

- SafeHouse

Uwagi:

- oferta aktualizowana po wejściu.

## Dungeon

Podstany:

### Inventory

Odpowiedzialność:

- przegląd przedmiotów,
- użycie mikstur.

Dozwolone przejścia:

- Dungeon

Ograniczenia:

- brak zmiany ekwipunku podczas walki.

### Character

Odpowiedzialność:

- wyłącznie podgląd.

Dozwolone przejścia:

- Dungeon

### PauseMenu

Odpowiedzialność:

- wznowienie,
- wyjście do menu.

Dozwolone przejścia:

- poprzedni stan

## Combat

Podstany:

### CombatInventory

Odpowiedzialność:

- wybór przedmiotu.

Dozwolone przejścia:

- Combat

Uwagi:

- użycie kończy turę.

### TargetSelection

Odpowiedzialność:

- wybór celu.

Dozwolone przejścia:

- Combat

### SkillSelection

Odpowiedzialność:

- wybór umiejętności.

Dozwolone przejścia:

- Combat

# Zasady

1. Inventory nie istnieje samodzielnie.
2. Shop istnieje wyłącznie w SafeHouse.
3. Character działa jako ekran.
4. Combat pozostaje nieprzerywalny poza Pause.
5. Podstany nie zapisują gry.

