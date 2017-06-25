using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Savoa
{
    public class Engine
    {
        private DefaultEntityManager entityManager;
        private SystemManager systemManager;
        private DefaultFamilyManager familyManager;
        private ComponentManager componentManager;

        public Engine()
        {
            entityManager = new DefaultEntityManager();
            systemManager = new DefaultSystemManager();
            familyManager = new DefaultFamilyManager(new ReadOnlyCollection<Entity>(entityManager.Entities()));
            componentManager = new ComponentManager();

            entityManager.EntityAdded += componentManager.OnEntityAdded;
            entityManager.EntityRemoved += componentManager.OnEntityRemoved;

            componentManager.EntityAdded += entityManager.OnEntityAdded;
            componentManager.EntityAdded += familyManager.OnEntityAdded;
            componentManager.EntityModified += familyManager.OnEntityModified;
            componentManager.EntityRemoved += entityManager.OnEntityRemoved;
            componentManager.EntityRemoved += familyManager.OnEntityRemoved;
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
            componentManager.ApplyOperations();

            foreach (System system in systemManager.Systems)
            {
                system.Process();

                componentManager.ApplyOperations();
            }
        }

        public ReadOnlyCollection<Entity> EntitiesFor(Family family)
        {
            return familyManager.EntitiesFor(family);
        }
    }
}
