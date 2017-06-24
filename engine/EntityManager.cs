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

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
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
