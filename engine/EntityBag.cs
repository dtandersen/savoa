using System;
using System.Collections.Generic;

namespace Savoa
{
    public class EntityBag
    {
        private List<Entity> entities;

        public EntityBag(List<Entity> entities)
        {
            this.entities = entities;
        }

        public List<Entity> GetEntities()
        {
            return entities;
        }

        internal void SetEntities(List<Entity> entities)
        {
            this.entities = entities;
        }
    }
}
