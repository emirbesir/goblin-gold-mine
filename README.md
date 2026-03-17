<div align="center">

# Goblin's Gold Mine

An arcade-idle game where you manage lazy workers and expand your mining empire as a greedy boss goblin.

![Last Commit](https://img.shields.io/github/last-commit/emirbesir/goblin-gold-mine?style=flat&logo=git&logoColor=white&color=0080ff)
![Top Language](https://img.shields.io/github/languages/top/emirbesir/goblin-gold-mine?style=flat&color=0080ff)
![Unity](https://img.shields.io/badge/Unity-FFFFFF.svg?style=flat&logo=Unity&logoColor=black)

_Work in Progress_

_Being built and tested with **Unity 6000.3.6f1**_

</div>

## Gameplay

Tap sleeping workers to wake them up and assign them to different mining zones. Workers automatically collect resources but eventually doze off again. Spend collected gold on upgrades to improve worker efficiency and mining capacity.

## Screenshots

<!-- Add screenshots to docs/img/ and update the paths here -->
_Coming soon_

## Technical Highlights

- **Zenject Dependency Injection:** IoC container for dependency management
- **MVC Architecture:** Separated Model (WorkerModel), View (WorkerView), Controller (WorkerController)
- **Command Pattern:** Command objects for state changes (CreateWorkerModelCommand, WorkerMineCommand)
- **Signal System:** Observer pattern for event communication (WorkerStateChangedSignal)
- **Repository Pattern:** WorkerRepository and WorkerViewRepository for data access
- **ScriptableObject Configuration:** Tunable worker parameters (collection amount, mining interval, awake duration)
- **Timer-Based State Machine:** Worker states (Awake ↔ Sleeping) managed via timers
