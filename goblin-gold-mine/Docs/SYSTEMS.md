# Goblin Gold Mine - Systems Reference

Detailed implementation reference for all gameplay systems added in the Week 3 prototype.

---

## Gold Economy

**Location:** `Assets/_Project/GoblinMine/Game/Gold/`

The gold economy converts mined resources into a single universal currency.

### Components
- **GoldModel** — Holds current gold balance
- **GoldRepository** — Single source of truth for gold state
- **GoldController** — Subscribes to `ResourceCollectedSignal`, routes to earn/spend commands
- **EarnGoldCommand** — Adds gold based on resource's `economicValue`
- **SpendGoldCommand** — Deducts gold for upgrades and region unlocks

### Flow
1. Player mines a resource → `ResourceCollectedSignal` fires
2. If `AutoDeposit` is true (worker-mined): GoldController immediately converts via `economicValue`
3. If player-mined: resources go to inventory, then converted on depot deposit

### Economic Values
| Resource    | Gold Value |
|-------------|-----------|
| Bronze      | 1         |
| Silver      | 3         |
| Gold        | 8         |
| Diamond     | 20        |
| Obsidian    | 50        |
| Mithril     | 120       |
| Dragonstone | 300       |
| Ethereal    | 750       |
| Goblinite   | 2,000     |

---

## Depot System

**Location:** `Assets/_Project/GoblinMine/Game/Depot/`

The depot is where the player deposits carried resources to convert them into gold.

### Components
- **DepotView** — MonoBehaviour with trigger collider at position (0, 0.5, -5)
- **DepotController** — Listens to DepotView trigger events, executes deposit command
- **DepositResourcesCommand** — Iterates player inventory, converts each resource to gold via `economicValue`, clears inventory

### Behavior
- Auto-deposits when player enters/stays in trigger zone
- 0.5s cooldown between deposits to prevent spam
- DOTween punch scale feedback on the depot object when a deposit occurs
- Fires haptic feedback on deposit

---

## Carrying Capacity

**Location:** `Assets/_Project/GoblinMine/Game/Player/`

Players have a limited inventory and must deposit resources before they can carry more.

### Configuration (PlayerConfiguration.asset)
| Parameter             | Value |
|-----------------------|-------|
| moveSpeedUnitsPerSecond | 5   |
| miningIntervalSeconds   | 0.8 |
| maxCarryCapacity        | 20  |

### Behavior
- `PlayerModel` tracks `MaxCarryCapacity` and current carried count
- `MiningResourceController` checks carry capacity before allowing the player to mine
- When at capacity, mining stops until the player deposits at the depot
- **CarryCapacityView** in the HUD shows current/max with a fill bar

---

## Upgrade System

**Location:** `Assets/_Project/GoblinMine/Game/Upgrade/`

Four upgrade types with exponential cost scaling, accessed at the Upgrade Station.

### Configuration (UpgradeConfiguration.asset)

| Upgrade        | Base Cost | Cost Multiplier | Effect per Level         | Cap              |
|----------------|-----------|-----------------|--------------------------|------------------|
| Buy Worker     | 50        | 2.5x            | +1 worker                | —                |
| Mining Speed   | 30        | 2.0x            | -0.1s mining interval    | Min 0.2s         |
| Move Speed     | 25        | 1.8x            | +1 unit/s move speed     | Max 15 units/s   |
| Carry Capacity | 20        | 1.6x            | +5 carry capacity        | —                |

**Cost formula:** `baseCost × multiplier^level`

### Components
- **UpgradeConfiguration** — ScriptableObject with all cost/scaling data
- **UpgradeController** — Handles upgrade purchase logic, checks gold availability
- **UpgradePanelView** — UI panel that appears when player is near the Upgrade Station
- **SpawnWorkerCommand** — Uses `WorkerView.Factory` to instantiate new workers

### Behavior
- Upgrade Station is a Cylinder at position (5, 0.5, -5)
- Panel UI shows/hides based on player proximity
- Each purchase deducts gold and applies the upgrade effect immediately
- Fires haptic feedback on purchase

---

## Region Unlocking

**Location:** `Assets/_Project/GoblinMine/Game/Region/`

The mine is divided into progressively locked regions with gates.

### Configuration (RegionConfiguration.asset)

| Index | Region Name    | Gate Position (z) | Unlock Cost |
|-------|----------------|-------------------|-------------|
| 0     | Silver Cavern  | 15                | 100         |
| 1     | Gold Depths    | 30                | 500         |
| 2     | Diamond Core   | 45                | 2,500       |
| 3     | Obsidian Abyss | 60                | 10,000      |

### Components
- **RegionConfiguration** — ScriptableObject with region names and unlock costs
- **RegionController** — Checks gold on gate trigger, executes unlock
- **RegionGateView** — MonoBehaviour on gate objects (Cubes), handles trigger detection and unlock animation

### Behavior
- Gates are physical barriers (Cubes) blocking passage between regions
- When the player enters a gate's trigger zone and has enough gold, the gate auto-unlocks
- Gold is deducted on unlock
- Gate animates to zero scale via DOTween, then deactivates
- Fires haptic feedback on unlock

---

## Haptic Feedback

**Location:** `Assets/_Project/GoblinMine/Game/Haptic/`

Simple haptic vibration for key game moments.

### Components
- **TriggerHapticCommand** — Wraps `Handheld.Vibrate()` for mobile haptic feedback

### Trigger Points
- Mining a resource (each collection tick)
- Depositing resources at the depot
- Purchasing an upgrade
- Unlocking a region gate
