using System;
using ArvisGames.SignalFlow;
using _Project.GoblinMine.Game.Worker.Model;

namespace _Project.GoblinMine.Game.Worker.Signal
{
    public class WorkerStateChangedSignal : ISignal
    {
        public Guid WorkerId { get; set; }
        public WorkerState State { get; set; }
    }
}
