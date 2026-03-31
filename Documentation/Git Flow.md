# Cel dokumentu

Celem dokumentu jest ujednolicenie pracy zespołu, zmniejszenie liczby konfliktów oraz przyśpieszenie developmentu.

Stosujemy uproszczony Git Flow oparty o short-lived branches.

# Główne założenia

W projekcie obowiązują następujące zasady:

- branch `main` musi być stabilny
- brach `devel` służy do integracji zmian
- każdy task posiada osobny branch
- brancze robocze są krótkotrwałe
- merge wykonujemy przez pull request na githubie
- używamy stategii `rebase + fast forward`

# Struktura branchy

Wprowadza się następującą strukturę branchy:

- main
- devel
    - feature/\*
    - bug/\*
    - hotfix/\*
    - docs/\*

## Branch main

Branch `main` jest branch'em produkcyjnym. Powinna się na nim znajdować zawsze stabilna wersja gry.

Na branch `main` trafiają wyłącznie sprawdzone zmiany. Zabronione jest bezpośrednie commitowanie do `main`.

Zasady:

- brak bezpośrednich commitów
- merge tylko przez Pull Request
- merge wykonuje Team Leader

> [!WARNING]
> Merge na `main` robimy tylko z `devel` i `hotfix`!

## Branch devel

Branch `devel` jest branch'em developerskim. Służy do integracji wszystkich feature'ów oraz poprawek.

To branch, z którego developerzy tworzą swoje branche robocze.

Zasady:

- branch może być niestabilny
- merge tylko przez Pull Request
- integracja feature i bug fix

# Branch robocze

Branche robocze są krótkotrwałe. Tworzymy je dla konkretnego taska i usuwamy po zakończeniu pracy. Każdy task posiada osobny branch.

# Feature Branch

Feature branch służy do implementacji nowych funkcjonalności.

Format nazwy:

```bash
feature/<nr_task>-<opis>
```

Na przykłąd:

```bash
feature/23-player-movement
feature/45-inventory-ui
```

> [!TIP]
> Numer taska jest dostępny po wejściu w task w adresie url. Np. nr 50: https:// trello .com/c/5rgyQEzU/--->50<---zainstalowa%C4%87-obsydian

Zasady:

- tworzony z `devel`
- jeden task = jeden branch
- merge do `devel`
- usuwany po merge

# Bug Branch

Bug branch służy do naprawy błędów, jak będą na `devel`.

Format:

```bash
bug/<nr_taska>-<opis>
```

Przykłady:

```bash
bug/31-inventory-crash
bug/52-animation-error
```

Zasady:

- tworzony z `devel`
- merge do `devel`
- usuwany po merge

# Hotfix Branch

Hotfix branch służy do szybkiej naprawy błędów w branchu `main`.

Format:

```bash
hotfix/<nr_taska>-<opis>
```

Przykład:

```bash
hotfix/88-save-crash
hotfix/91-build-error
```

Zasady:

- tworzony z `main`
- merge do `main`
- merge do `devel`
- usuwany po merge

> [!CAUTION]
> Ważne żeby zrobić merge do `main` i `devel`!

# Docs branch

Branch służy do osobnej aktualizacji dokumentacji i innych dokumentów.

# Workflow pracy

## Pobieranie aktualnych zmian

Przed rozpoczęciem pracy należy pobrać aktualne zmiany:

```bash
git checkout devel
git pull
```

> [!CAUTION]
> Ważne!

Następnie utworzyć swojego brancha.

## Utworzenie brancha

Tworzymy branch dla taska:

```bash
git checkout -b <branch_name>
```

## Praca i commit

Podczas pracy wykonujemy małe, czytelne commity. Zmiany w jednym w dwóch plikach.

```bash
git add .
git commit -m "typ(zakres): opis krótki"
```

Nie trzeba wypychać kodu po każdym commitcie.

> [!NOTE]
> Im lepiej opisane tym bardziej ułatwi nam pracę. Tytuł commitu powinien być napisany w stylu "Ten commit..." np. dodaje coś => "ft(player): dodaje movement postaci".

## Przesyłanie kodu na GitHub

```bash
git push -u origin <branch_name> --force-with-lease
```

## Aktualizacja branch

Regularnie aktualizujemy branch. Swój o zamiany na `devel`.

```bash
git fetch origin
git rebase origin/devel

# jeśli konflikt:
# - rozwiązaujemy
# potem
git add .
git rebase --continue
```

## Konflikt przy merge

Podobnie jak z aktualizacją brancha, tylko na końcu trzeba wypchnąć na zdalne repo.

```bash
git fetch origin
git rebase origin/devel
# rozwiązujemy konflikt
git add .
git rebase --continue
git push --force-with-lease
```

# Merge workflow

Po zakończeniu pracy:

1. Push branch
2. Utwórz Pull Request
3. Rebase
4. Merge
5. Usuń branch

# Konwencja nazewnictwa commitów

Używamy formatów:

```bash
typ(zakres): opis
```

Typy commitów:

- ft - nowa funkcjonalność
- fix - poprawka błędu
- ref - refaktoryzacja kodu
- cht - zmiany techniczne
- docs - dokumentacja
- art - assety
- ui - UI

Na przykład:

```bash
ft(player): dodano movement postaci
fix(inventory): crash przy włączeniu gry
ref(player): refaktoryzacja klasy BaseCharacter
```

# Code Style

Używamy [Godot C# Style Guide](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html#doc-c-sharp-styleguide).

Lepiej coś popsuć i się czegoś nauczyć niż wkleić syf z AI.
