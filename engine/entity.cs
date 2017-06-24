using System.Collections.Generic;
using System;

namespace Savoa
{
    public class Entity
    {
        private List<object> components = new List<object>();

        public void AddComponent(object component)
        {
            components.Add(component);
        }

        public T GetComponent<T>()
        {
            foreach (object component in components)
            {
                if (component is T)
                    return (T)component;
            }

            return default(T);
        }

        public bool HasComponent(Type t)
        {
            foreach (object component in components)
            {
                if (component.GetType() == t)
                    return true;
            }

            return false;
        }
    }
}
