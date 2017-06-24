namespace Savoa
{
    public abstract class EntitySystem : System
    {
        protected Engine engine;
        protected EntityBag entities;

        abstract public void Process();

        abstract public void processEntity(Entity entity);
        public virtual void AddedToEngine(Engine engine)
        {
            this.engine = engine;
        }
    }
}
