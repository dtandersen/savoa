using System;
using System.Collections.Generic;

namespace Savoa
{
    public class Engine
    {
        private EntityManager entityManager;
        private SystemManager systemManager;

        public Engine()
        {
            entityManager = new DefaultEntityManager();
            systemManager = new DefaultSystemManager();
        }

        public void AddEntity(Entity entity)
        {
            entityManager.AddEntity(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entityManager.RemoveEntity(entity);
        }

        public bool ContainsEntity(Entity entity)
        {
            return entityManager.ContainsEntity(entity);
        }

        public void AddSystem(System system)
        {
            systemManager.AddSystem(system);
            system.AddedToEngine(this);
        }

        public List<Entity> Entities()
        {
            return entityManager.Entities();
        }

        public void Process()
        {
            systemManager.Process();
        }

        public EntityBag EntitiesFor(params Type[] componentTypes)
        {
            return new EntityBag(this, componentTypes);
        }
    }
}
