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

    interface SystemManager
    {
        void Process();
        void AddSystem(System system);
    }

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

    class DefaultSystemManager : SystemManager
    {
        private List<System> systems = new List<System>();

        public DefaultSystemManager()
        {
        }

        public void Process()
        {
            foreach (System system in systems)
            {
                system.Process();
            }
        }

        public void AddSystem(System system)
        {
            systems.Add(system);
        }
    }

    public class EntityBag
    {
        Engine engine;
        Type[] componentTypes;

        public EntityBag(Engine engine, Type[] componentTypes)
        {
            this.engine = engine;
            this.componentTypes = componentTypes;
        }

        public List<Entity> GetEntities()
        {
            List<Entity> filtered = new List<Entity>();
            foreach (Entity entity in engine.Entities())
            {
                bool mismatch = false;
                foreach (Type t in componentTypes)
                {
                    if (!entity.HasComponent(t))
                    {
                        mismatch = true;
                        break;
                    }
                }

                if (mismatch) { continue; }

                filtered.Add(entity);
            }

            return filtered;
        }
    }
}
