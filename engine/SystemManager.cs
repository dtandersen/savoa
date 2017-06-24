using System.Collections.Generic;

namespace Savoa
{
    interface SystemManager
    {
        void Process();
        void AddSystem(System system);
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
}
