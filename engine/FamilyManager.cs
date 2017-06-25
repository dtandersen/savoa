using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Savoa
{
    interface FamilyManager
    {
        ReadOnlyCollection<Entity> EntitiesFor(Family family);
        void OnEntityAdded(Entity entity);
    }

    class DefaultFamilyManager : FamilyManager
    {
        private readonly Dictionary<Family, List<Entity>> families;
        private readonly Dictionary<Family, ReadOnlyCollection<Entity>> familyReferences;
        private ReadOnlyCollection<Entity> entities;

        public DefaultFamilyManager(ReadOnlyCollection<Entity> entities)
        {
            families = new Dictionary<Family, List<Entity>>();
            familyReferences = new Dictionary<Family, ReadOnlyCollection<Entity>>();
            this.entities = entities;
        }

        public ReadOnlyCollection<Entity> EntitiesFor(Family family)
        {
            List<Entity> familyMembers;

            if (families.ContainsKey(family))
            {
                familyMembers = families[family];
            }
            else
            {
                familyMembers = new List<Entity>();
                families[family] = familyMembers;
                familyReferences[family] = new ReadOnlyCollection<Entity>(familyMembers);
            }

            familyMembers.Clear();

            foreach (Entity entity in this.entities)
            {
                if (!family.Matches(entity))
                {
                    continue;
                }

                familyMembers.Add(entity);
            }

            ReadOnlyCollection<Entity> entities = familyReferences[family];

            return entities;
        }

        public void OnEntityAdded(Entity entity)
        {
            UpdateFamilies(entity);
        }

        internal void OnEntityRemoved(Entity entity)
        {
            RemoveFamilyMember(entity);
        }

        public void OnEntityModified(Entity entity)
        {
            UpdateFamilies(entity);
        }

        /// <summary>
        /// Update the family memberships of the entity.
        /// </summary>
        private void UpdateFamilies(Entity entity)
        {
            foreach (KeyValuePair<Family, List<Entity>> entry in families)
            {
                Family family = entry.Key;
                if (family.Matches(entity))
                {
                    List<Entity> familyMembers = families[family];
                    if (!familyMembers.Contains(entity))
                    {
                        familyMembers.Add(entity);
                    }
                }
                else
                {
                    List<Entity> familyMembers = families[family];
                    familyMembers.Remove(entity);
                }
            }
        }

        private void RemoveFamilyMember(Entity entity)
        {
            foreach (KeyValuePair<Family, List<Entity>> entry in families)
            {
                Family family = entry.Key;
                List<Entity> familyMembers = this.families[family];
                familyMembers.Remove(entity);
            }
        }
    }
}
