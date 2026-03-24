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

### Opis

`BaseCharacter` jest klasą abstrakcyjną, która przechowuje wspólne właściwości postaci.

### Właściwości

| Właściwość | Typ    | Opis                                     |
| ---------- | ------ | ---------------------------------------- |
| Name       | string | Nazwa/imię postaci                       |
| HP         | int    | Punkty życia postaci                     |
| Attack     | int    | Siła ataku                               |
| Defense    | int    | Obrona postaci                           |
| Luck       | int    | Szczęście wpływające na losowe zdarzenia |
| CritChance | int    | Szansa na trafienie krytyczne (w %)      |

### Konstruktor

| Parametr   | Typ    | Opis                          |
| ---------- | ------ | ----------------------------- |
| name       | string | Nazwa postaci                 |
| hp         | int    | Punkty życia                  |
| attack     | int    | Siła ataku                    |
| defense    | int    | Obrona                        |
| luck       | int    | Szczęście                     |
| critChance | int    | Szansa na trafienie krytyczne |

---

## PlayerCharacter

Reprezentuje postać gracza w grze.  
Dziedziczy po klasie `BaseCharacter` i rozszerza ją o funkcjonalności specyficzne dla gracza.

### Opis

`PlayerCharacter` przechowuje podstawowe statystyki gracza i w przyszłości będzie
rozszerzona o dodatkowe elementy, takie jak:

- umiejętności,
- ekwipunek,
- rozwój postaci.

### Dziedziczenie

![[Pasted image 20260324091512.png]]

### Statystyki bazowe (dziedziczone)

| Właściwość | Typ | Opis                          |
| ---------- | --- | ----------------------------- |
| HP         | int | Punkty życia                  |
| Attack     | int | Siła ataku                    |
| Defense    | int | Obrona                        |
| Luck       | int | Szczęście                     |
| CritChance | int | Szansa na trafienie krytyczne |

### Statystyki gracza

| Właściwość | Typ | Opis                                     |
| ---------- | --- | ---------------------------------------- |
| Level      | int | Poziom postaci gracza                    |
| Exp        | int | Aktualne punkty doświadczenia            |
| ExpNextLvl | int | Wymagana ilość EXP do następnego poziomu |

### Konstruktor

| Parametr   | Typ    | Opis                          |
| ---------- | ------ | ----------------------------- |
| name       | string | Nazwa postaci                 |
| hp         | int    | Punkty życia                  |
| attack     | int    | Siła ataku                    |
| defense    | int    | Obrona                        |
| luck       | int    | Szczęście                     |
| critChance | int    | Szansa na trafienie krytyczne |

### Metody

#### CalculateExpToNextLevel

Oblicza ilość doświadczenia wymaganą do osiągnięcia następnego poziomu.

##### Zwraca

`int` — ilość doświadczenia wymagana do następnego poziomu

#### LevelUp

Zwiększa poziom postaci oraz aktualizuje wymagane doświadczenie do następnego poziomu.

#### AddExp

Dodaje punkty doświadczenia i zwiększa Level jeśli potrzeba.

##### Parametry

| Parametr | Typ | Opis                 |
| -------- | --- | -------------------- |
| amount   | int | Ilość Exp do dodania |

---
