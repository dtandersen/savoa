using System;
using System.Collections.Generic;

namespace Savoa
{
    public class EntityBag
    {
        private Engine engine;
        private Type[] componentTypes;

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
