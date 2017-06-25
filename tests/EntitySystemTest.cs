using Xunit;

namespace Savoa
{
    public class SystemTest
    {
        [Fact]
        public void ProcessSystem()
        {
            Engine engine = new Engine();
            EntitySystem es = new IteratingEntitySystem12();
            engine.AddSystem(es);

            Entity entity1 = new Entity();
            entity1.AddComponent(new Component1());
            entity1.AddComponent(new Component2());
            engine.AddEntity(entity1);

            Entity entity2 = new Entity();
            entity2.AddComponent(new Component2());
            entity2.AddComponent(new Component1());
            engine.AddEntity(entity2);

            engine.Process();

            Assert.True(entity1.GetComponent<Component1>().Processed);
            Assert.True(entity1.GetComponent<Component2>().Processed);
            Assert.True(entity2.GetComponent<Component1>().Processed);
            Assert.True(entity2.GetComponent<Component2>().Processed);
        }

        [Fact]
        public void AddSystemAfterEntity()
        {
            Engine engine = new Engine();

            Entity entity1 = new Entity();
            entity1.AddComponent(new Component1());
            entity1.AddComponent(new Component2());
            engine.AddEntity(entity1);

            Entity entity2 = new Entity();
            entity2.AddComponent(new Component2());
            entity2.AddComponent(new Component1());
            engine.AddEntity(entity2);

            EntitySystem es = new IteratingEntitySystem12();
            engine.AddSystem(es);

            engine.Process();

            Assert.True(entity1.GetComponent<Component1>().Processed);
            Assert.True(entity1.GetComponent<Component2>().Processed);
            Assert.True(entity2.GetComponent<Component1>().Processed);
            Assert.True(entity2.GetComponent<Component2>().Processed);
        }

        [Fact]
        public void ProcessTwoSystems()
        {
            Engine engine = new Engine();
            engine.AddSystem(new IteratingEntitySystem1());
            engine.AddSystem(new IteratingEntitySystem2());

            Entity entity1 = new Entity();
            entity1.AddComponent(new Component1());
            entity1.AddComponent(new Component2());
            engine.AddEntity(entity1);

            Entity entity2 = new Entity();
            entity2.AddComponent(new Component2());
            entity2.AddComponent(new Component1());
            engine.AddEntity(entity2);

            engine.Process();

            Assert.True(entity1.GetComponent<Component1>().Processed);
            Assert.True(entity1.GetComponent<Component2>().Processed);
            Assert.True(entity2.GetComponent<Component1>().Processed);
            Assert.True(entity2.GetComponent<Component2>().Processed);
        }

        [Fact]
        public void FilterEntitiesBeforeProcessing()
        {
            Engine engine = new Engine();
            engine.AddSystem(new IteratingEntitySystem1());
            engine.AddSystem(new IteratingEntitySystem2());

            Entity entity1 = new Entity();
            entity1.AddComponent(new Component1());
            engine.AddEntity(entity1);

            Entity entity2 = new Entity();
            entity2.AddComponent(new Component2());
            engine.AddEntity(entity2);

            engine.Process();

            Assert.True(entity1.GetComponent<Component1>().Processed);
            Assert.True(entity2.GetComponent<Component2>().Processed);
        }
    }

    class IteratingEntitySystem1 : IteratingEntitySystem
    {
        override public void processEntity(Entity entity)
        {
            entity.GetComponent<Component1>().Processed = true;
        }

        override public void AddedToEngine(Engine engine)
        {
            entities = engine.EntitiesFor(new Family(typeof(Component1)), typeof(Component1));
        }
    }

    class IteratingEntitySystem2 : IteratingEntitySystem
    {
        override public void processEntity(Entity entity)
        {
            entity.GetComponent<Component2>().Processed = true;
        }

        override public void AddedToEngine(Engine engine)
        {
            entities = engine.EntitiesFor(new Family(typeof(Component2)), typeof(Component2));
        }
    }

    class IteratingEntitySystem12 : IteratingEntitySystem
    {
        override public void processEntity(Entity entity)
        {
            entity.GetComponent<Component1>().Processed = true;
            entity.GetComponent<Component2>().Processed = true;
        }

        override public void AddedToEngine(Engine engine)
        {
            entities = engine.EntitiesFor(new Family(typeof(Component1), typeof(Component2)), typeof(Component1), typeof(Component2));
        }
    }

    internal class Component1
    {
        public bool Processed;
    }

    internal class Component2
    {
        public bool Processed;
    }
}
