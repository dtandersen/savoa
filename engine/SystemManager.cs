using System.Collections.Generic;

namespace Savoa
{
    interface SystemManager
    {
        IEnumerable<System> Systems { get; }

        void AddSystem(System system);
    }

    class DefaultSystemManager : SystemManager
    {
        private List<System> systems = new List<System>();

        public IEnumerable<System> Systems => systems;

        public DefaultSystemManager()
        {
        }

        public void AddSystem(System system)
        {
            systems.Add(system);
        }
    }
}
