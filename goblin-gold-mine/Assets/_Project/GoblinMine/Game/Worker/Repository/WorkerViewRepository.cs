using System;
using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Worker.View;

namespace _Project.GoblinMine.Game.Worker.Repository
{
    public class WorkerViewRepository
    {
        public List<WorkerView> WorkerViews { get; set; } = new List<WorkerView>();

        public WorkerView GetWorkerViewById(Guid id)
        {
            return WorkerViews.FirstOrDefault(w => w.Id == id);
        }
    }
}
