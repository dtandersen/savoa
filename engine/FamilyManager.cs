using System;
using System.Collections.Generic;

namespace Savoa
{
    interface FamilyManager
    {
        EntityBag EntitiesFor(Family family);
        void Process();
    }

    class DefaultFamilyManager : FamilyManager
    {
        private readonly Dictionary<Family, List<Entity>> families;
        private readonly Dictionary<Family, EntityBag> familyReferences;
        private EntityBag entities;

        public DefaultFamilyManager(EntityBag entities)
        {
            families = new Dictionary<Family, List<Entity>>();
            familyReferences = new Dictionary<Family, EntityBag>();
            this.entities = entities;
        }

        public EntityBag EntitiesFor(Family family)
        {
            List<Entity> entities;

            if (families.ContainsKey(family))
            {
                entities = families[family];
            }
            else
            {
                entities = new List<Entity>();
                families[family] = entities;
                familyReferences[family] = new EntityBag(entities);
            }

            entities.Clear();

            foreach (Entity entity in this.entities.GetEntities())
            {
                if (!family.Matches(entity))
                {
                    continue;
                }

                entities.Add(entity);
            }

            EntityBag bag = familyReferences[family];
            bag.SetEntities(entities);

            return bag;
        }

        public void Process()
        {
            foreach (KeyValuePair<Family, List<Entity>> entry in families)
            {
                EntitiesFor(entry.Key);
            }
        }
    }
}
