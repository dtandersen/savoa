using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savoa
{
    public class Family
    {
        private Type[] componentTypes;

        public Family(params Type[] componentTypes)
        {
            this.componentTypes = componentTypes;
        }

        internal bool Matches(Entity entity)
        {
            foreach (Type t in componentTypes)
            {
                if (!entity.HasComponent(t))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
