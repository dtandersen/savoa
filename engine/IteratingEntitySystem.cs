namespace Savoa
{
    public abstract class IteratingEntitySystem : EntitySystem
    {
        override public void Process()
        {
            foreach (Entity entity in entities.GetEntities())
            {
                processEntity(entity);
            }
        }
    }
}
