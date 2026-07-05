![](assets/CombatDomain.jpeg)

## CombatSession

Agregat odpowiedzialny za przebieg walki.

Zawiera:

- uczestników,
- kolejność tur,
- aktualny stan,
- wynik.

Nie zna UI.

## CombatParticipant

Abstrakcja jednostki.

Implementacje:

- PlayerCombatant
- EnemyCombatant

## CombatAction

Akcja wykonywana w turze.

Typy:

- Attack
- Skill
- Item
- Defend
- Escape

## StatusEffect

Efekt wykonywany po turze.

Implementacje:

- Burn
- Poison
- Bleed
- Stun

Strategia: Strategy Pattern

## CombatReward

Wynik walki.

Zawiera:

- exp
- gold
- items


# Game.Core / Domain / Combat

Aktualny stan
// todo: przerobić potem na gaphora

```mermaid
classDiagram
direction TB

class CombatSession {
    State
    Participants
    ActiveParticipant
    Target
    TurnNumber
    Reward

    SelectAction()
    ExecuteSelectedAction()
    SetTarget()
    Finish()
}

class CombatParticipant {
    Id
    Type
    SourceId
    CurrentHp
    Stats
    IsAlive

    ReceiveDamage()
    Heal()
}

class CombatStats {
    MaxHp
    Attack
    Defense
    CriticalChance
    Luck
}

class CombatParticipantType {
<<enumeration>>
Player
Enemy
}

class CombatReward {
    Experience
    Gold
    ItemInstanceIds
}

class CombatAction {
<<abstract>>
Execute(session)
}

class AttackAction {
Execute(session)
}

class CombatStateMachine {
Current
Start()
Update()
}

class ICombatState {
<<interface>>
Type
Enter()
Update()
Exit()
}

class PlayerTurnState

class CombatContext {
Session
}

class CombatStateTransition {
ShouldChange
NextState
}

class CombatStateType {
<<enumeration>>
Start
PlayerTurn
PlayerStatus
EnemyTurn
EnemyStatus
Reward
End
}


CombatSession --> CombatParticipant
CombatSession --> CombatReward
CombatSession --> CombatAction

CombatParticipant --> CombatStats
CombatParticipant --> CombatParticipantType

CombatAction <|-- AttackAction

CombatSession --> CombatStateMachine

CombatStateMachine --> ICombatState

PlayerTurnState ..|> ICombatState

ICombatState --> CombatContext

CombatContext --> CombatSession

ICombatState --> CombatStateTransition

CombatStateMachine --> CombatStateType
```


## Combat Flow

```mermaid
stateDiagram-v2

[*] --> Start

Start --> PlayerTurn

PlayerTurn --> PlayerStatus

PlayerStatus --> EnemyTurn

EnemyTurn --> EnemyStatus

EnemyStatus --> Reward

Reward --> End

End --> [*]
```


## Execute Action

```mermaid
sequenceDiagram

participant State as PlayerTurnState
participant Context
participant Session
participant Action

State->>Context: Session

State->>Session: ExecuteSelectedAction()

Session->>Action: Execute(session)

Action-->>Session: Updated state

Session-->>State: Continue
```
