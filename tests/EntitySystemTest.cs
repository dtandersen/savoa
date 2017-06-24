using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Savoa
{
    public class SystemTest
    {
        ITestOutputHelper outputHelper;

        public SystemTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Fact]
        public void Test1()
        {
            Engine engine = new Engine(new XunitLogger(outputHelper));
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
        public void Test2()
        {
            Engine engine = new Engine(new XunitLogger(outputHelper));
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
        public void Test3()
        {
            Engine engine = new Engine(new XunitLogger(outputHelper));
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
            entities = engine.EntitiesFor(typeof(Component1));
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
            entities = engine.EntitiesFor(typeof(Component2));
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
            entities = engine.EntitiesFor(typeof(Component1), typeof(Component2));
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