using System;
using System.Collections.Generic;

namespace Savoa
{
    internal class ComponentManager
    {
        private HashSet<Entity> modified;
        private HashSet<Entity> deleted;
        private HashSet<Entity> added;

        public delegate void EntityModificationHandler(Entity entity);

        public event EntityModificationHandler EntityAdded;
        public event EntityModificationHandler EntityModified;
        public event EntityModificationHandler EntityRemoved;

        public bool HasUpdates => modified.Count > 0;

        public ComponentManager()
        {
            modified = new HashSet<Entity>();
            deleted = new HashSet<Entity>();
            added = new HashSet<Entity>();
        }

        internal void OnEntityAdded(Entity entity)
        {
            entity.ComponentAdded += this.OnComponentAdded;
            entity.ComponentRemoved += this.OnComponentRemoved;
            added.Add(entity);
        }

        internal void OnEntityRemoved(Entity entity)
        {
            deleted.Add(entity);
        }

        private void OnComponentRemoved(Entity entity)
        {
            modified.Add(entity);
        }

        private void OnComponentAdded(Entity entity)
        {
            modified.Add(entity);
        }
        
        internal void ApplyOperations()
        {
            foreach (Entity entity in added)
            {
                EntityAdded(entity);
            }

            added.Clear();
            //if (!HasUpdates) return;

            foreach (Entity entity in modified)
            {
                EntityModified(entity);
            }

            modified.Clear();

            foreach (Entity entity in deleted)
            {
                EntityRemoved(entity);
            }

            deleted.Clear();
        }
    }
}
