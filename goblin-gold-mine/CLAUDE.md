# Unity Projects - Coding Guidelines

## Code Style

### Naming Conventions
- **Classes**: PascalCase with suffix indicating type: `AttackCommand`, `EnemyView`, `ScoreRepository`, `GameController`
- **Private fields**: `_camelCase` with underscore prefix
- **Exception**: `[SerializeField]` fields in Views and Configurations use `camelCase` without underscore
- **Properties**: PascalCase, prefer expression-bodied getters: `public float Value => _value;`
- **Constants/Animator hashes**: `private static readonly int WalkingId = Animator.StringToHash("Walking");`
- **Namespaces**: Match folder structure: `ProjectName.Game.Hero.Command`
- **Duration/cooldown fields**: Explicitly state units — `attackCooldownSeconds`, `stunDurationMilliseconds`
- **Distance fields**: Explicitly state units — `attackRangeUnits`, `aoeRadiusUnits` (game world space)
- **Speed/velocity fields**: Explicitly state units — `projectileSpeedUnitsPerSecond`, `movementSpeedUnitsPerTurn`

### Formatting
- 4 spaces indentation
- Opening braces on **new line** (Allman / C# style)
- Use `var` wherever possible
- Use `readonly` for immutable field references

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
- **Repositories**: Hold state (models), accessed by controllers and commands. Repositories know **only about their Models** — no references to Views, Controllers, Commands, Signals, or other Repositories.
- **Views**: MonoBehaviours for rendering, communicate with Controllers via **Action events** (e.g. `OnButtonClicked`, `OnDragStarted`) — **not** Zenject Signals. **Views should NEVER instantiate objects** - instantiation belongs in Commands or Controllers.
- **Signals**: Zenject SignalBus for decoupled communication between systems (Controller-to-Controller, system-to-system). Views do not fire Signals.

### Feature Folder Structure
Each feature in `Assets/ProjectName/Game/` follows:
```
Feature/
├── Command/         # Action execution (AttackCommand, SpawnCommand)
├── Configuration/   # ScriptableObject configs
├── Controller/      # Orchestrates logic
├── Model/           # Data containers with reactive events (no business logic, no methods)
├── Repository/      # State storage
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

Lifecycle interfaces: `IPreInitializable`, `IPostInitializable`, `IInitializable`

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
- **Models are data containers with reactive events** - No business logic, no methods. Models hold auto-properties and may use `Action`, `ReactiveProperty`, or similar events to notify when data changes. All logic (mutations, queries, computations) belongs in Commands or Repositories.
- **ScriptableObject References** - When a serialized field on a ScriptableObject needs a prefab/asset assigned, edit the `.asset` YAML directly. Look up the GUID from the `.meta` file and the fileID from the target component inside the prefab. Never ask the user to assign references manually in the Inspector.

## Common Gotchas
- Views have dual repositories: `EnemyRepository` (models) + `EnemyViewRepository` (view references)
- Commands can be async (with CancellationToken) or synchronous — not all commands need to be async
- Commands should be atomic and avoid calling other commands — Controllers handle orchestration
- Always cache Animator parameter hashes as static readonly
- Use Memory Pools for spawning, not direct Instantiate
- ScriptableObjects for all configuration - no magic numbers in code
- No luck-based retry loops (e.g. while loops with random attempts) — use deterministic selection
- LINQ is fine in turn-based/event-driven code but avoid in Update/FixedUpdate/LateUpdate
- Switch statements are acceptable — don't over-engineer with polymorphism when switch/case works cleanly
