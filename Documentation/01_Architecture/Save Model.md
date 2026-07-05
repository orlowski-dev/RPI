# Cel dokumentu

Dokument definiuje strukturę zapisu gry, zakres danych przechowywanych w plikach JSON oraz zasady zapisu i odczytu stanu gry.

Model zapisu stanowi źródło prawdy dla:

- zapisu i wczytywania gry,
- kontynuacji ostatniej sesji,
- powrotu po śmierci,
- statystyk sesji,
- historii zdarzeń.

# Założenia

System zapisu opiera się na następujących decyzjach:

- zapis dostępny jest wyłącznie w safe house,
- po śmierci gracza kara jest liczona przed wykonaniem zapisu,
- zapisy przechowywane są w wielu slotach,
- kontynuacja ładuje ostatnio zapisany slot,
- historia eventów zapisywana jest osobno,
- ustawienia aplikacji przechowywane są poza save,
- save nie przechowuje nazw jako źródła prawdy, tylko identyfikatory oraz stan runtime,
- definicje klas, umiejętności, przedmiotów i przeciwników znajdują się poza save.

# player.json

## Cel

Przechowuje stan postaci gracza oraz dane potrzebne do odtworzenia jej progresji.

Przykład:

```json
{
	"playerId": "player_001",
	"name": "Patryk",
	"classId": "class_warrior",
	"progression": {
		"level": 8,
		"xp": 1820,
		"skillPoints": 3
	},
	"resources": {
		"currentHp": 210,
		"gold": 1800
	},
	"baseStats": {
		"hp": 280,
		"attack": 33,
		"defense": 31,
		"criticalChance": 5,
		"luck": 2
	},
	"skillTree": {
		"selectedPathId": "path_tank",
		"unlockedSkillIds": [
			"skill_warrior_tank_hard_skin",
			"skill_warrior_tank_iron_stance"
		],
		"cooldowns": {
			"skill_warrior_tank_hard_skin": 0
		}
	}
}
```

## Zakres danych

- identyfikator postaci,
- nazwa gracza,
- klasa postaci jako `classId`,
- poziom, XP i punkty umiejętności,
- bieżące HP,
- gold,
- bazowe statystyki,
- aktywna ścieżka rozwoju,
- odblokowane umiejętności jako `skillId`,
- cooldowny umiejętności.

# world.json

## Cel

Przechowuje stan bieżącego dungeonu oraz postęp eksploracji.

Przykład:

```json
{
	"currentDungeon": {
		"dungeonId": "dungeon_03",
		"level": 3,
		"enemiesKilled": 18,
		"bossThreshold": 20,
		"bossUnlocked": false,
		"bossDefeated": false,
		"entities": [
			{
				"entityId": "enemy_101",
				"enemyTypeId": "enemy_elite",
				"level": 10,
				"alive": true,
				"currentHp": 180
			}
		],
		"interactions": [
			{
				"interactionId": "barrel_001",
				"interactionType": "barrel",
				"used": true
			},
			{
				"interactionId": "trap_001",
				"interactionType": "trap",
				"activated": false
			}
		]
	},
	"continueAvailable": true
}
```

## Zakres danych

- identyfikator dungeonu,
- poziom wyprawy,
- liczba pokonanych przeciwników,
- próg odblokowania bossa,
- informacja czy boss jest odblokowany,
- informacja czy boss został pokonany,
- lista aktywnych przeciwników,
- stan interakcji środowiskowych,
- flaga kontynuacji.

# ItemDefinition

## Cel

Statyczna definicja.

Przykład:

```json
{
	"definitionId": "weapon_warrior_demacia_axe",

	"category": "MeleeWeapon",

	"allowedClasses": ["class_warrior"],

	"baseName": "Topor Demacjanina",

	"baseStats": {
		"hp": 0,
		"attack": 20,
		"defense": -3,
		"criticalChance": 8,
		"luck": 1
	},

	"generated": true
}
```

## Kategorie

- MeleeWeapon
- RangedWeapon
- MagicWeapon
- Armor
- Potion

## Rarity

```json
{
	"Common": {
		"multiplier": 1.0,
		"chance": 60
	},

	"Rare": {
		"multiplier": 1.5,
		"chance": 25
	},

	"Epic": {
		"multiplier": 2.2,
		"chance": 10
	},

	"Legendary": {
		"multiplier": 3.5,
		"chance": 5
	}
}
```

# ItemInstance

## Cel

Stan wygenerowanego przedmiotu.

Przykład:

```json
{
	"instanceId": "itm_inst_001",

	"definitionId": "weapon_warrior_demacia_axe",

	"rarity": "Epic",

	"levelGenerated": 12,

	"source": {
		"type": "EnemyDrop",

		"sourceId": "enemy_elite"
	},

	"finalStats": {
		"hp": 0,
		"attack": 53,
		"defense": -8,
		"criticalChance": 17,
		"luck": 2
	}
}
```

# Item Generation

Algorytm:

```
1. wybór kategorii
2. wybór definicji
3. losowanie rarity
4. obliczenie rarityMultiplier
5. obliczenie levelMultiplier
6. wygenerowanie finalStats
7. utworzenie ItemInstance
```

Formuła:

$$
finalStats =
baseStats
× rarityMultiplier
× levelMultiplier
$$

# inventory.json

## Cel

Stan ekwipunku.

Przykład:

```json
{
	"equipment": {
		"weapon": "itm_inst_001",

		"armor": "itm_inst_002"
	},

	"items": [
		{
			"instanceId": "itm_inst_001",

			"definitionId": "weapon_warrior_demacia_axe",

			"category": "MeleeWeapon",

			"rarity": "Epic",

			"levelGenerated": 12,

			"quantity": 1,

			"finalStats": {
				"hp": 0,
				"attack": 53,
				"defense": -8,
				"criticalChance": 17,
				"luck": 2
			}
		},

		{
			"instanceId": "itm_inst_002",

			"definitionId": "armor_plate",

			"category": "Armor",

			"rarity": "Rare",

			"levelGenerated": 10,

			"quantity": 1,

			"finalStats": {
				"hp": 42,
				"attack": 0,
				"defense": 24,
				"criticalChance": 0,
				"luck": 0
			}
		},

		{
			"instanceId": "itm_inst_100",

			"definitionId": "potion_heal",

			"category": "Potion",

			"quantity": 4,

			"effect": {
				"type": "HealPercent",

				"value": 40
			}
		}
	]
}
```

# events.json

## Cel

Przechowuje historię zdarzeń w trybie append-only.

Przykład:

```json
[
	{
		"timestamp": "2026-06-12T17:40:00Z",
		"eventType": "CombatStarted",
		"payload": {
			"enemyCount": 3
		}
	},
	{
		"timestamp": "2026-06-12T17:42:00Z",
		"eventType": "ItemConsumed",
		"payload": {
			"itemInstanceId": "itm_inst_101"
		}
	},
	{
		"timestamp": "2026-06-12T17:45:00Z",
		"eventType": "SkillUsed",
		"payload": {
			"skillId": "skill_warrior_tank_hard_skin"
		}
	}
]
```

## Zakres danych

- znacznik czasu,
- typ eventu,
- payload eventu.

# settings.json

## Cel

Przechowuje globalne ustawienia aplikacji.

Przykład:

```json
{
	"audio": {
		"master": 80,
		"music": 60,
		"sfx": 85
	},
	"video": {
		"fullscreen": true
	},
	"controls": {
		"inventory": "I",
		"interact": "E",
		"pause": "ESC"
	}
}
```

## Zakres danych

- głośność,
- tryb pełnoekranowy,
- przypisania klawiszy.

# Odczyt zapisu

## Algorytm

1. Odczytaj slot.
2. Wczytaj `meta.json`.
3. Wczytaj `player.json`.
4. Wczytaj `world.json`.
5. Wczytaj `inventory.json`.
6. Wczytaj `events.json`.
7. Odtwórz stan runtime.
8. Przejdź do safe house.

# Zapis gry

## Algorytm

1. Odczytaj aktualny stan runtime.
2. Zbuduj snapshot `player.json`, `world.json`, `inventory.json`, `meta.json`.
3. Dopisz eventy do `events.json`.
4. Zapisz pliki do aktywnego slotu.

## Zasady

- zapis wykonywany jest wyłącznie w safe house,
- po śmierci kara liczona jest przed zapisem,
- save nie zapisuje scen ani UI,
- save przechowuje stan, nie zachowanie.

# Tryb Continue

## Algorytm

1. Odczytaj listę slotów.
2. Wybierz slot z ostatnim zapisem.
3. Wczytaj jego stan.
4. Przejdź do safe house.
5. Umożliw dalszą grę.

# Zasady ogólne

1. Save przechowuje ID, a nie definicje tekstowe.
2. Nazwy nie są źródłem prawdy.
3. Definicje klas, umiejętności, przedmiotów i przeciwników są trzymane poza save.
4. Itemy i skille odwołują się przez `definitionId`, `skillId` i `instanceId`.
5. Save nie zawiera scen Godota.
6. Save nie zawiera UI.
7. Save nie zawiera logiki gry.
8. Save przechowuje stan niezbędny do odtworzenia sesji.

# Powiązania z innymi dokumentami

- [Gameplay Loop](../02_GameDesign/Gameplay%20Loop.md)
- [Combat](Combat.md)
- [Events](Events.md)
- [Modules](Modules.md)
- [ADR-003](ADR/ADR-003.md)
- [ADR-001](ADR/ADR-001.md)
