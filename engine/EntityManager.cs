using System;
using System.Collections.Generic;

namespace Savoa
{
    interface EntityManager
    {
        void AddEntity(Entity entity);

        void RemoveEntity(Entity entity);

        bool ContainsEntity(Entity entity);

        List<Entity> Entities();
    }

    class DefaultEntityManager : EntityManager
    {
        private List<Entity> entities = new List<Entity>();

        public delegate void AddEntityHandler(Entity entity);

        public event AddEntityHandler EntityAdded;
        public event AddEntityHandler EntityRemoved;

        public DefaultEntityManager()
        {
        }

        public void AddEntity(Entity entity)
        {
            EntityAdded?.Invoke(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            EntityRemoved?.Invoke(entity);
        }

        internal void OnEntityAdded(Entity entity)
        {
            entities.Add(entity);
        }

        internal void OnEntityRemoved(Entity entity)
        {
            entities.Remove(entity);
        }

        public bool ContainsEntity(Entity entity)
        {
            return entities.Contains(entity);
        }

        public List<Entity> Entities()
        {
            return entities;
        }
    }
}
