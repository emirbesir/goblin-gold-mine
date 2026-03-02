# Goblin's Gold Mine - Claude Guidelines

## Project Overview
Unity 6 (6000.3.6f1) mobile game - arcade idle game.

## Code Style

### Naming Conventions
- **Classes**: PascalCase with suffix indicating type: `AttackCommand`, `EnemyView`, `ScoreRepository`, `GameController`. **All MonoBehaviours must end with `View`**. All Zenject signals must end with `Signal` and clearly describe the event (`EnemyDiedSignal`, not `ProcessSignal`)
- **Private fields**: `_camelCase` with underscore prefix
- **Exception**: `[SerializeField]` fields in Views and Configurations use `camelCase` without underscore
- **Properties**: PascalCase, prefer expression-bodied getters: `public float Value => _value;`
- **Constants/Animator hashes**: `private static readonly int WalkingId = Animator.StringToHash("Walking");`
- **Namespaces**: Match folder structure: `_Project.GoblinMine.Game.MiningResource.Command`
- **Duration/cooldown fields**: Explicitly state units — `attackCooldownSeconds`, `stunDurationMilliseconds`
- **Distance fields**: Explicitly state units — `attackRangeUnits`, `aoeRadiusUnits` (game world space)
- **Speed/velocity fields**: Explicitly state units — `projectileSpeedUnitsPerSecond`, `movementSpeedUnitsPerTurn`
- **Abbreviations**: No abbreviations are allowed. HP, AoE, etc. are not fine.

### Formatting
- 4 spaces indentation
- Opening braces on **new line** (Allman / C# style)
- Use `var` wherever possible
- Use `readonly` for immutable field references
- No trailing whitespace or extra blank lines

## Architecture

### Component Relationships
```
┌─────────────────────────────────────────────────────────────┐
│                       Controllers                            │
│  - Subscribe to Signals         - Fire Signals               │
│  - Execute Commands             - Update Views               │
│  - Read/Write Repositories                                   │
└───────┬─────────────────┬─────────────────┬─────────────────┘
        │                 │                 │
        ▼                 ▼                 ▼
   ┌──────────┐     ┌─────────────┐     ┌──────────┐
   │ Commands │────►│Repositories │     │  Views   │
   │          │     │  (Models)   │     │ (Action  │
   │          │     └─────────────┘     │  events) │
   │          │─────────────────────────►          │
   │          │  (animations, VFX, etc.)└────┬─────┘
   └────┬─────┘                              │
        │                                    │
        ▼                                    ▼
   ┌─────────┐                       Controllers/Commands
   │ Signals │──► Controllers        (via Action callbacks)
   └─────────┘
```

- **Controllers**: Orchestrate logic, subscribe to signals, coordinate commands and views. Can contain simple conditional logic (e.g. FTUE checks). As logic gets complex, extract into Commands.
- **Commands**: Execute specific atomic actions, access repositories, handle object instantiation. Commands can also work with Views — calling view methods to trigger animations, VFX, or other visual feedback, awaiting async view methods (`UniTask`), chaining visual sequences, and performing model/state changes alongside. Commands should avoid orchestrating/calling other commands — orchestration belongs in Controllers. Commands can be async (`UniTask` with `CancellationToken`) or synchronous (returning `void`, `int`, `List<T>`, etc.).
- **Repositories**: Hold state (models), accessed by controllers and commands. Repositories know **only about their Models** — no references to Views, Controllers, Commands, Signals, or other Repositories. No signal firing from Repositories. No conditional logic beyond basic lookup.
- **Views**: MonoBehaviours for rendering, communicate with Controllers via **Action events** (e.g. `OnButtonClicked`, `OnDragStarted`) — **not** Zenject Signals. **Views should NEVER instantiate objects** - instantiation belongs in Commands or Controllers. Views contain no business logic. Switch statements in Views for visual mapping (e.g. choosing a sprite based on an enum) are acceptable.
- **Signals**: Zenject SignalBus for decoupled communication between systems (Controller-to-Controller, system-to-system). Views do not fire Signals. Signals are simple data carriers — no logic.

### Feature Folder Structure
Each feature in `Assets/_Project/GoblinMine/Game/` follows:
```
Feature/
├── Command/         # Action execution (AttackCommand, SpawnCommand)
├── Configuration/   # ScriptableObject configs
├── Controller/      # Orchestrates logic
├── Model/           # Data containers with reactive events (no business logic, no methods)
├── Repository/      # State storage
├── Service/         # Business logic
├── Signal/          # Event definitions
└── View/            # MonoBehaviour visuals
```

### Key Patterns

**Command Pattern** - All game actions are encapsulated. Commands can be async or synchronous:
```csharp
// Async command
public class SomeAsyncCommand
{
    public async UniTask Execute(..., CancellationToken token) { }
}

// Synchronous command
public class SomeCommand
{
    public List<EnemyModel> Execute(HeroModel hero, int maxTargets) { }
}
```

**Repository Pattern** - Single source of truth for state:
```csharp
public class SomeRepository
{
    public List<Model> Items { get; set; }
}
```

**Signal/Event System** - Zenject SignalBus for decoupling:
```csharp
_signalBus.Subscribe<SomeSignal>(OnSignalReceived);
_signalBus.Fire(new SomeSignal { ... });
```

**Memory Pools** - For frequently spawned objects (enemies, projectiles):
```csharp
public class Pool : MemoryPool<SomeView> { }
```

## Dependency Injection (Zenject)

All bindings in Installer classes. Common patterns:
```csharp
Container.Bind<SomeCommand>().AsSingle().NonLazy();
Container.Bind<SomeRepository>().AsSingle().NonLazy();
Container.Bind<SomeView>().FromInstance(view).AsSingle().NonLazy();
Container.Bind<SomeConfiguration>().FromScriptableObject(config).AsSingle().NonLazy();
```

- Lifecycle interfaces: `IPreInitializable`, `IPostInitializable`, `IInitializable`
- **All new classes must be bound in the appropriate Installer** (`GameInstaller`, `BootstrapInstaller`, etc.)
- No `new` for service/command/repository classes — use DI. No static service locator patterns.

## Key Assemblies
- `Bootstrap.asmdef` - Scene initialization
- `Game.asmdef` - Gameplay systems
- `Menu.asmdef` - UI/menus
- `Project.asmdef` - Cross-scene persistence
- `Shared.asmdef` - Reusable utilities

## Dependencies
- **Zenject** - DI framework
- **UniTask** - Async/await (`Cysharp.Threading.Tasks`)
- **DOTween** - Tweening (`DG.Tweening`)
- **R3** - Reactive extensions (used sparingly)
- **TextMesh Pro** - UI text

## Git Workflow
- Main branch: `main`
- Development branch: `develop`
- Create feature branches from `develop`

## Rules

### Data & Configuration
- **Models are data containers with reactive events** - No business logic, no methods. Models hold auto-properties and may use `Action`, `ReactiveProperty`, or similar events to notify when data changes. All logic (mutations, queries, computations) belongs in Commands or Repositories.
- **Configurations are pure data** - ScriptableObject configurations expose serialized data only (fields + simple accessors). No business logic, no caching, no computed lookups. If logic is needed to transform config data (e.g. building model lists, lookups), that logic belongs in a Command.
- **ScriptableObject References** - When a serialized field on a ScriptableObject needs a prefab/asset assigned, edit the `.asset` YAML directly. Look up the GUID from the `.meta` file and the fileID from the target component inside the prefab. Never ask the user to assign references manually in the Inspector.
- **No magic sentinel values** - Never use special values (e.g. `-1`, `0`, `null` string) to encode state implicitly. Use an explicit `bool` field/property instead (e.g. `IsAssigned`, `IsInitialized`). The meaning of a field's value must be obvious from its type and name alone.
- **No magic numbers** - All configuration values (durations, multipliers, thresholds, radii, counts) belong in ScriptableObject Configurations. If a value is truly internal to one command and will never need tuning, a `const` is acceptable, but prefer Configuration.

### Code Quality
- **File length** - Keep files under 200 lines. Over 300 lines is a red flag — split into smaller classes.
- **Method length** - Methods should be short and single-purpose (under 30 lines). Methods do one thing at one level of abstraction.
- **No deep nesting** - More than 3 levels of nesting means the code should be refactored (early returns, extract method).
- **No dead code** - No commented-out code, no unused `using` statements, no unused parameters, no unreachable code paths.
- **No code duplication** - No copy-pasted logic across files. Extract to shared Command or utility.

### Error Handling & Safety
- **Null checks on Views** - Always null-check before accessing View references (Views can be despawned/pooled).
- **Null checks on repository lookups** - Repository lookups that might return null must be checked.
- **CancellationToken propagation** - Properly propagate `CancellationToken` through async call chains. Check at meaningful points inside loops, not just at the end.
- **No swallowed exceptions** - No empty catch blocks. If a catch is intentionally empty, add a comment explaining why.
- **Collection safety** - Check `.Count` before indexing. Handle empty collections gracefully.

### Performance
- **No allocations in hot paths** - No unnecessary allocations in per-frame code (`Update`, `FixedUpdate`, `LateUpdate`).
- **No string concatenation in hot paths** - Use `StringBuilder` or `string.Format` instead.

### Unity Specifics
- **Commit `.meta` files** - All new assets and folders must have their `.meta` files committed.
- **No leftover debug code** - No `Debug.Log` statements that aren't meant to stay in production.

## Common Gotchas
- Views have dual repositories: `EnemyRepository` (models) + `EnemyViewRepository` (view references)
- Always cache Animator parameter hashes as static readonly
- Use Memory Pools for spawning, not direct Instantiate
- No luck-based retry loops (e.g. while loops with random attempts) — use deterministic selection
- LINQ is fine in turn-based/event-driven code but avoid in Update/FixedUpdate/LateUpdate
- Switch statements are acceptable — don't over-engineer with polymorphism when switch/case works cleanly
