namespace Savoa
{
    public interface System
    {
        void Process();
        void AddedToEngine(Engine engine);
    }

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

    public abstract class IteratingEntitySystem : EntitySystem
    {

        // abstract public void processEntity(Entity entity);

        override public void Process()
        {
            foreach (Entity entity in entities.GetEntities())
            {
                processEntity(entity);
            }
        }
    }
}
