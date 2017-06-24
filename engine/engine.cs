using System;
using System.Collections.Generic;

namespace Savoa
{
    public class Engine
    {
        private EntityManager entityManager;
        private SystemManager systemManager;
        private FamilyManager familyManager;

        public Engine()
        {
            entityManager = new DefaultEntityManager();
            systemManager = new DefaultSystemManager();
            familyManager = new DefaultFamilyManager(new EntityBag(entityManager.Entities()));
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
            familyManager.Process();
            systemManager.Process();
        }

        public EntityBag EntitiesFor(Family family, params Type[] componentTypes)
        {
            return familyManager.EntitiesFor(family);
        }
    }
}
