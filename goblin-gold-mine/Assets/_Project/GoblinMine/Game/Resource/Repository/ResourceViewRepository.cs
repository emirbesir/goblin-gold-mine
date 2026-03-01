using System;
using System.Collections.Generic;
using System.Linq;
using GoblinMine.Game.Resource.View;

namespace GoblinMine.Game.Resource.Repository
{
    public class ResourceViewRepository
    {
        public List<ResourceView> Views { get; set; } = new List<ResourceView>();

        public ResourceView GetViewById(Guid id)
        {
            return Views.FirstOrDefault(v => v.Id == id);
        }
    }
}
