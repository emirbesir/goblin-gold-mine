# Goblin Gold Mine - Game Design Document

## Overview

**Genre:** Casual Idle Mining
**Platform:** Mobile (Android)
**Engine:** Unity (URP)
**Theme:** A goblin king mines increasingly rare and valuable resources in a fantasy mine

### Elevator Pitch

Walk, mine, carry, deposit, upgrade, expand. Control the Goblin King with a joystick, approach resource nodes to automatically mine them, carry resources back to the depot to earn gold, then invest in upgrades and unlock new regions with rarer resources.

---

## Core Loop

```
  ┌──────────┐
  │   Walk   │ ◄── Joystick movement
  └────┬─────┘
       ▼
  ┌──────────┐
  │   Mine   │ ◄── Enter trigger zone, auto-collect on interval
  └────┬─────┘
       ▼
  ┌──────────┐
  │  Carry   │ ◄── Resources added to inventory (carry limit: 20 base)
  └────┬─────┘
       ▼
  ┌──────────┐
  │ Deposit  │ ◄── Walk to depot, auto-deposit on proximity
  └────┬─────┘
       ▼
  ┌──────────┐
  │Earn Gold │ ◄── Resources converted to gold via economic value
  └────┬─────┘
       ▼
  ┌──────────┐
  │ Invest   │ ◄── Buy upgrades or unlock new regions
  └────┬─────┘
       │
       └──────► Back to Walk
```

---

## Player

- **Character:** The Goblin King
- **Movement:** Virtual joystick (dynamic, appears on touch)
- **Mining:** Automatic when within trigger radius of a resource node
- **Mining Interval:** 0.8s base (upgradeable via Mining Speed)
- **Move Speed:** 5 units/s base (upgradeable, max 15 units/s)
- **Carry Capacity:** 20 base (upgradeable, +5 per level)
- **No health/combat:** Pure resource collection gameplay

---

## Resource Nodes

### Mechanics
- Each node has a **trigger collider** — entering it starts automatic mining
- Mining occurs at a fixed **interval** (seconds between collections)
- Each collection spawns **chunks** (visual resource pieces that fly toward the player)
- Nodes have **durability** — number of successful collections before depletion
- Depleted nodes switch to a "depleted" model and become inactive
- After a **respawn delay**, the node reactivates with full durability

### Visual Feedback
- **Punch scale** on the node when mined
- **Color flash** on collection
- **Chunk particles** flying toward the player
- **Model swap** between ready and depleted states

---

## Resources & Tiers

9 tiers of resources, ordered by rarity and value:

| Tier | Resource    | Description                                          |
|------|-------------|------------------------------------------------------|
| 1    | Bronze      | Common ore, abundant and easy to mine                |
| 2    | Silver      | Slightly rarer, a step up from the basics            |
| 3    | Gold        | The classic treasure — moderately valuable            |
| 4    | Diamond     | Precious gem requiring patience to extract            |
| 5    | Obsidian    | Volcanic glass, hard to find but very rewarding       |
| 6    | Mithril     | Legendary lightweight metal from deep in the mine     |
| 7    | Dragonstone | Crystallized dragon fire — extremely rare             |
| 8    | Ethereal    | Otherworldly material that phases in and out of reality|
| 9    | Goblinite   | The ultimate treasure, unique to this mine             |

Higher-tier resources:
- Have **higher economic value** per collection
- Take **longer intervals** between collections
- Produce **more visual chunks** per collection
- Have **lower durability** (fewer collections before depletion)
- Have **longer respawn delays**
- Yield dramatically **higher income/s** overall

See `BALANCING.md` for the full balance table and economic values.

---

## Inventory System

- Displays all 9 resource types in a scrollable UI panel
- Each entry shows the resource **icon** and **current count**
- Inventory updates in real-time as resources are collected
- Resources are tracked per-type with a **carry capacity limit** (base 20, upgradeable)
- When at capacity, the player must deposit at the depot before mining more
- HUD shows current carried count / max capacity with a fill bar

---

## Depot

- Located at the starting area (position 0, 0.5, -5)
- Walking into the depot **auto-deposits** all carried resources (0.5s cooldown between deposits)
- Each deposited resource is converted to **gold** based on its economic value
- Visual punch feedback on deposit
- See `SYSTEMS.md` for implementation details

---

## Gold Economy

- **Gold** is the universal currency earned by depositing resources
- Economic value per resource ranges from 1 (Bronze) to 2000 (Goblinite)
- Gold is spent on **upgrades** and **region unlocks**
- Workers auto-deposit their mined resources directly to gold
- HUD displays current gold balance

---

## Upgrades

4 upgrade types available at the Upgrade Station (position 5, 0.5, -5):

| Upgrade        | Effect                        |
|----------------|-------------------------------|
| Buy Worker     | Spawns a new auto-mining worker |
| Mining Speed   | Reduces mining interval (-0.1s/level, min 0.2s) |
| Move Speed     | Increases player speed (+1 unit/s/level, max 15) |
| Carry Capacity | Increases carry limit (+5/level) |

All upgrades use **exponential cost scaling** — see `BALANCING.md` for exact numbers.

The upgrade panel UI appears when the player is near the Upgrade Station.

---

## Regions

The mine is divided into regions separated by locked gates. Each region contains higher-tier resource nodes.

| Region          | Gate Position | Unlock Cost |
|-----------------|---------------|-------------|
| Silver Cavern   | z = 15        | 100 gold    |
| Gold Depths     | z = 30        | 500 gold    |
| Diamond Core    | z = 45        | 2,500 gold  |
| Obsidian Abyss  | z = 60        | 10,000 gold |

Walking into a locked gate **auto-unlocks** it if the player has enough gold. Gates animate away (scale to zero) when unlocked.

---

## Workers

- Purchased via the Buy Worker upgrade
- Autonomous NPCs that mine and auto-deposit resources for gold
- Workers wander to nearby resource nodes, mine them, and instantly convert to gold (no carrying)
- Each new worker increases passive income

---

## Haptic Feedback

Haptic vibration triggers on key moments:
- Mining a resource
- Depositing at the depot
- Purchasing an upgrade
- Unlocking a region

---

## Technical Architecture

- **Zenject** for dependency injection
- **Command pattern** for all game actions
- **Repository pattern** for state management
- **Signal bus** for decoupled system communication
- **UniTask** for async operations
- **DOTween** for visual animations
- **Memory pools** for chunk spawning

See `CLAUDE.md` for full architecture documentation and `SYSTEMS.md` for system implementation details.
