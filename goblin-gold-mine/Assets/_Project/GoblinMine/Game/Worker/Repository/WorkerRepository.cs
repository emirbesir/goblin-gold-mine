using System;
using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Worker.Model;

namespace _Project.GoblinMine.Game.Worker.Repository
{
    public class WorkerRepository
    {
        public List<WorkerModel> Workers { get; set; } = new List<WorkerModel>();

        public WorkerModel GetWorkerById(Guid id)
        {
            return Workers.FirstOrDefault(w => w.Id == id);
        }
    }
}
