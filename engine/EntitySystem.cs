namespace Savoa
{
    public abstract class EntitySystem : System
    {
        protected EntityBag entities;

        abstract public void Process();

        abstract public void processEntity(Entity entity);

        abstract public void AddedToEngine(Engine engine);
    }
}
