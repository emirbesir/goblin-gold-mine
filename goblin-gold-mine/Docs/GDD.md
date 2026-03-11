# Goblin Gold Mine - Game Design Document

## Overview

**Genre:** Casual Idle Mining
**Platform:** Mobile (Android)
**Engine:** Unity (URP)
**Theme:** A goblin king mines increasingly rare and valuable resources in a fantasy mine

### Elevator Pitch

Walk, mine, collect, repeat. Control the Goblin King with a joystick, approach resource nodes to automatically mine them, and watch your inventory fill with riches. Nodes deplete and respawn, creating a satisfying loop of exploration and resource management.

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
  │ Collect  │ ◄── Chunks fly to player, inventory updates
  └────┬─────┘
       ▼
  ┌──────────┐
  │ Deplete  │ ◄── Durability reaches 0, node goes dormant
  └────┬─────┘
       ▼
  ┌──────────┐
  │ Respawn  │ ◄── After delay, node reappears ready to mine
  └────┬─────┘
       │
       └──────► Back to Walk
```

---

## Player

- **Character:** The Goblin King
- **Movement:** Virtual joystick (dynamic, appears on touch)
- **Mining:** Automatic when within trigger radius of a resource node
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

---

## Inventory System

- Displays all 9 resource types in a scrollable UI panel
- Each entry shows the resource **icon** and **current count**
- Inventory updates in real-time as resources are collected
- Resources are tracked per-type with no capacity limit

---

## Economy & Progression (Future)

### Planned Features
- **Shop:** Spend resources to buy upgrades
- **Mining Tools:** Faster collection intervals, more chunks per hit
- **Pickaxe Upgrades:** Reduce mining interval for specific tiers
- **Mine Expansion:** Unlock new areas with higher-tier nodes
- **Prestige System:** Reset progress for permanent multipliers
- **Auto-Miners:** Idle collection while offline

---

## Technical Architecture

- **Zenject** for dependency injection
- **Command pattern** for all game actions
- **Repository pattern** for state management
- **Signal bus** for decoupled system communication
- **UniTask** for async operations
- **DOTween** for visual animations
- **Memory pools** for chunk spawning

See `CLAUDE.md` for full architecture documentation.
