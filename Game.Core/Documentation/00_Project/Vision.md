## Nazwa robocza projektu

Dungeon Crawler RPG Roguelite - Abyss of Echoes

# Cel projektu

Celem projektu jest stworzenie gry RPG 2D typu roguelite z widokiem top-down, wykorzystującej turowy system walki, eksplorację lochu, rozwój postaci oraz system przedmiotów.

Projekt realizowany jest jako zaliczenie przedmiotu i ma demonstrować poprawne zastosowanie praktyk inżynierii oprogramowania, w szczególności:

- separację logiki biznesowej od warstwy prezentacji,
- wykorzystanie wzorców projektowych,
- testowanie logiki aplikacji,
- dokumentowanie procesu projektowego.

# Wizja produktu

Gra ma zapewniać krótkie, zamknięte sesje rozgrywki opierające się na cyklu:

- miasto,
- eksploracja lochu ,
- walka,
- zdobywanie nagród,
- rozwój postaci,
- kolejna wyprawa.

Rozgrywka ma być prosta do zrozumienia, ale pozwalać na budowanie różnych stylów gry poprzez:

- wybór klasy postaci,
- rozwój umiejętności,
- zdobywanie ekwipunku,
- zarządzanie ryzykiem.

# Główne filary projektu

## Prostota implementacji

Priorytetem jest dostarczenie kompletnego rozwiązania w ograniczonym czasie.

Rozwiązania techniczne powinny być możliwie proste, przewidywalne i testowalne.

## Architektura

Całość logiki gry powinna znajdować się poza silnikiem.

Silnik odpowiada wyłącznie za:

- prezentację,
- sceny,
- obsługę wejścia,
- integrację.

## Grywalność

Gracz powinien podejmować decyzje:

- kiedy używać zasobów,
- jak rozwijać postać,
- czy ryzykować dalszą eksplorację.

# Definicja ukończenia projektu

Projekt uznaje się za ukończony, jeżeli:

- możliwe jest rozpoczęcie nowej gry,
- możliwe jest ukończenie pełnego cyklu rozgrywki,
- działa zapis i odczyt,
- logika posiada testy,
- istnieje dokumentacja końcowa,
- projekt przechodzi prezentację i obronę.
