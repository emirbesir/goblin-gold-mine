# Unity Projects — Code Review Checklist

Use this checklist when reviewing branches before merging into `develop`. Each item should be verified for every changed `.cs` file. Mark items as PASS / FAIL / N/A.

---

## 1. Architecture Rules

### Commands
- [ ] **All business logic lives in Commands** — no business logic in Repositories, Views, or Models
- [ ] Commands can be async (`async UniTask Execute(..., CancellationToken token)`) or synchronous (returning `void`, `int`, `string`, `List<T>`, etc.) depending on the use case
- [ ] For async Commands, `CancellationToken` is checked at meaningful points inside loops, not just at the end
- [ ] Commands should be **atomic** — they encapsulate a single unit of business logic
- [ ] Commands should **avoid orchestrating or calling other Commands** when possible — orchestration belongs in Controllers
- [ ] No luck-based retry loops (e.g. `while` loops with random attempts) — if an operation can fail, handle it deterministically

### Repositories
- [ ] Repositories are **simple data getters/setters only** — zero business logic
- [ ] Repositories know **only about their Models** — no references to Views, Controllers, Commands, Signals, or other Repositories
- [ ] No signal firing from Repositories
- [ ] No conditional logic beyond basic lookup

### Controllers
- [ ] Controllers **orchestrate** — they subscribe to signals, invoke commands, and update views
- [ ] Controllers **can contain simple logic** (e.g. conditionals like showing menu view vs. game view based on FTUE state)
- [ ] As logic gets more complex, it should be **extracted into Commands** — Controllers should not accumulate heavy business logic
- [ ] Controllers can compose and call multiple Commands in sequence

### Models
- [ ] Models are **data containers only** — auto-properties and reactive events (`Action`, `ReactiveProperty`)
- [ ] No methods on Models (no `TakeDamage()`, no `ApplyBuff()`, etc.)
- [ ] No business logic in Models

### Views
- [ ] Views **never instantiate objects** — instantiation belongs in Commands or Controllers
- [ ] Views handle rendering and communicate with Controllers via **Action events** (e.g. `OnButtonClicked`, `OnDragStarted`) — **not** Zenject Signals
- [ ] Views don't contain business logic
- [ ] Switch statements in Views (e.g. choosing a sprite based on an enum) are acceptable

### Signals (Zenject SignalBus)
- [ ] Signals are used for **decoupled communication between systems** (Controller-to-Controller, system-to-system)
- [ ] Signals are simple data carriers (no logic)
- [ ] Signal names clearly describe the event (`EnemyDiedSignal`, not `ProcessSignal`)
- [ ] Views do **not** fire Zenject Signals — they use Action events instead

---

## 2. SOLID Principles

### Single Responsibility (S)
- [ ] Each class has one reason to change
- [ ] File length is reasonable (< 200 lines as a guideline; > 300 is a red flag)

### Open/Closed (O)
- [ ] New behavior can be added via new classes rather than modifying existing ones
- [ ] Configuration values (multipliers, durations, radii, thresholds) are in **ScriptableObjects**, not hardcoded
- [ ] Switch statements are acceptable when they keep things simple — don't over-engineer with polymorphism when a switch/case works cleanly

### Liskov Substitution (L)
- [ ] Derived classes don't break base class contracts
- [ ] Interface implementations fulfill the full contract

### Interface Segregation (I)
- [ ] No "fat" interfaces forcing implementers to stub unused methods
- [ ] Interfaces are focused and minimal

### Dependency Inversion (D)
- [ ] All dependencies are injected via constructor (Zenject)
- [ ] No `new` for service/command/repository classes (use DI)
- [ ] No static service locator patterns

---

## 3. Clean Code

### Naming
- [ ] Classes: PascalCase with type suffix (`AttackCommand`, `EnemyView`, `ScoreRepository`)
- [ ] Private fields: `_camelCase` with underscore prefix
- [ ] **Exception:** `[SerializeField]` fields in Views and Configurations use `camelCase` without underscore
- [ ] Properties: PascalCase with expression-bodied getters where appropriate
- [ ] Constants/hashes: `private static readonly` with meaningful names
- [ ] Namespaces match folder structure: `ArvisGames.ProjectConveyorHeroes.Game.Feature.Layer`
- [ ] No abbreviations unless universally understood (HP, AoE, DoT, DPS are fine)
- [ ] **Duration/cooldown fields explicitly state units:** `attackCooldownSeconds`, `stunDurationMilliseconds`
- [ ] **Distance fields explicitly state units:** `attackRangeUnits`, `aoeRadiusUnits` (game world space)
- [ ] **Speed/velocity fields explicitly state units:** `projectileSpeedUnitsPerSecond`, `movementSpeedUnitsPerTurn`

### Formatting
- [ ] 4 spaces indentation (no tabs)
- [ ] Opening braces on **new line** (Allman / C# style)
- [ ] `var` used wherever possible
- [ ] `readonly` on all fields that are assigned only in constructor
- [ ] No trailing whitespace or extra blank lines

### Magic Numbers
- [ ] No hardcoded numeric values in logic — **all magic numbers belong in ScriptableObject Configurations**
- [ ] Durations, multipliers, thresholds, radii, counts — all belong in Configuration assets
- [ ] If a value is truly internal to one command and will never need tuning, a `const` is acceptable, but prefer Configuration

### Methods
- [ ] Methods are short and single-purpose (< 30 lines as guideline)
- [ ] Methods do one thing at one level of abstraction
- [ ] No deeply nested conditionals (> 3 levels of nesting = refactor)

### Dead Code
- [ ] No commented-out code
- [ ] No unused `using` statements
- [ ] No unused parameters (especially in interface-conforming methods — document with `// intentionally unused` if needed)
- [ ] No unreachable code paths

### Duplication
- [ ] No copy-pasted logic across files — extract to shared Command or utility
- [ ] Similar patterns across switch cases should be evaluated for extraction

---

## 4. Error Handling & Safety

- [ ] Null checks before accessing View references (Views can be despawned/pooled)
- [ ] Null checks on repository lookups that might return null
- [ ] `CancellationToken` properly propagated through async call chains
- [ ] No swallowed exceptions (empty catch blocks)
- [ ] Collection operations handle empty collections gracefully (check `.Count` before indexing)
- [ ] No luck-based retry patterns (random loops hoping for a valid result) — use deterministic selection

---

## 5. Performance

- [ ] Memory pools used for frequently spawned objects (enemies, projectiles) — no direct `Instantiate`
- [ ] No LINQ operations or allocations in frequently called callbacks (`Update`, `FixedUpdate`, `LateUpdate`)
- [ ] LINQ is acceptable in turn-based / event-driven code (commands, signal handlers, etc.)
- [ ] No unnecessary allocations in hot paths (per-frame code)
- [ ] Animator parameter hashes cached as `private static readonly int`
- [ ] No string concatenation in hot paths (use `StringBuilder` or `string.Format`)

---

## 6. Unity & Project-Specific

- [ ] ScriptableObject references assigned via `.asset` YAML (GUID + fileID), never manually in Inspector
- [ ] All new classes bound in the appropriate Installer (`GameInstaller`, `BootstrapInstaller`, etc.)
- [ ] Lifecycle interfaces used correctly (`IPreInitializable`, `IPostInitializable`, `IInitializable`)
- [ ] `.meta` files committed for all new assets and folders

---

## 7. Git & Merge Readiness

- [ ] No unrelated changes mixed into the branch
- [ ] No temporary debug code (`Debug.Log` statements that aren't meant to stay)
- [ ] Commit messages follow project convention
- [ ] No large binary files committed that should be in Git LFS or excluded
- [ ] Branch is rebased/up-to-date with target branch (no merge conflicts)

---

## Review Severity Levels

| Level | Meaning | Action |
|-------|---------|--------|
| **CRITICAL** | Architecture violation, data corruption risk, or crash | Must fix before merge |
| **WARNING** | Code smell, performance issue, or missing configuration | Should fix before merge |
| **SUGGESTION** | Style inconsistency, readability improvement, or future-proofing | Track for follow-up |

---

## How to Use This Checklist

1. Run `git diff develop..<branch> -- '*.cs'` to see all C# changes
2. For each changed file, walk through the relevant sections above
3. Log findings with severity, file name, line number, and recommended fix
4. All CRITICAL items must be resolved before merge approval
5. WARNING items should be resolved or have a tracked follow-up task
6. SUGGESTION items are optional but encouraged
