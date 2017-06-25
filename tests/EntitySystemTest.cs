using System.Collections.ObjectModel;
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
            engine.AddEntity(entity1);

            EntitySystem es = new IteratingEntitySystem1();
            engine.AddSystem(es);

            engine.Process();

            Assert.True(entity1.GetComponent<Component1>().Processed);
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

        [Fact]
        public void SystemCanAddAComponent()
        {
            Engine engine = new Engine();

            Entity entity1 = new Entity();
            Component3 component3 = new Component3();
            entity1.AddComponent(component3);
            engine.AddEntity(entity1);

            engine.AddSystem(new IteratingEntitySystem3());
            engine.AddSystem(new IteratingEntitySystem4());

            engine.Process();

            Assert.Equal(1, component3.Counter);

            //engine.Process();

            //Assert.Equal(1, component3.Counter);
            Assert.Equal(1, entity1.GetComponent<Component4>().Counter);
        }

        [Fact]
        public void SystemCanDeleteEntity()
        {
            Engine engine = new Engine();
            engine.AddSystem(new HealthSystem(engine));

            Entity entity = new Entity();
            entity.AddComponent(new HealthComponent() { Health = 1 });
            engine.AddEntity(entity);

            engine.Process();

            Assert.Equal(0, entity.GetComponent<HealthComponent>().Health);
            Assert.False(engine.ContainsEntity(entity));

            // entity should not be reprocessed
            engine.Process();

            Assert.Equal(0, entity.GetComponent<HealthComponent>().Health);
            Assert.False(engine.ContainsEntity(entity));
        }

        [Fact]
        public void SystemCanAddEntity()
        {
            Engine engine = new Engine();
            engine.AddSystem(new SpawnerSystem());
            engine.AddSystem(new ChildSystem());

            Entity entity = new Entity();
            entity.AddComponent(new ParentComponent());
            engine.AddEntity(entity);

            engine.Process();

            ReadOnlyCollection<Entity> entities = engine.EntitiesFor(new Family(typeof(ParentComponent)));
            Assert.Equal(2, entities.Count);
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
            entities = engine.EntitiesFor(new Family(typeof(Component1)));
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
            entities = engine.EntitiesFor(new Family(typeof(Component2)));
        }
    }

    class IteratingEntitySystem3 : IteratingEntitySystem
    {
        override public void processEntity(Entity entity)
        {
            entity.GetComponent<Component3>().Counter++;
            entity.RemoveComponent<Component3>();
            entity.AddComponent(new Component4());
        }

        override public void AddedToEngine(Engine engine)
        {
            entities = engine.EntitiesFor(new Family(typeof(Component3)));
        }
    }

    class IteratingEntitySystem4 : IteratingEntitySystem
    {
        override public void processEntity(Entity entity)
        {
            entity.GetComponent<Component4>().Counter++;
        }

        override public void AddedToEngine(Engine engine)
        {
            entities = engine.EntitiesFor(new Family(typeof(Component4)));
        }
    }

    class HealthSystem : IteratingEntitySystem
    {
        private readonly Engine engine;

        public HealthSystem(Engine engine)
        {
            this.engine = engine;
        }

        override public void processEntity(Entity entity)
        {
            HealthComponent healthComponent = entity.GetComponent<HealthComponent>();
            healthComponent.Health--;
            if (healthComponent.Health <= 0)
            {
                engine.RemoveEntity(entity);
            }
        }

        override public void AddedToEngine(Engine engine)
        {
            entities = engine.EntitiesFor(new Family(typeof(HealthComponent)));
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
            entities = engine.EntitiesFor(new Family(typeof(Component1), typeof(Component2)));
        }
    }

    class SpawnerSystem : IteratingEntitySystem
    {
        private Engine engine;

        override public void AddedToEngine(Engine engine)
        {
            this.engine = engine;
            entities = engine.EntitiesFor(new Family(typeof(ParentComponent)));
        }

        override public void processEntity(Entity entity)
        {
            Entity entity1 = new Entity();
            entity1.AddComponent(new ParentComponent());
            engine.AddEntity(entity1);
        }
    }

    class ChildSystem : IteratingEntitySystem
    {
        private Engine engine;

        override public void AddedToEngine(Engine engine)
        {
            this.engine = engine;
            entities = engine.EntitiesFor(new Family(typeof(ChildComponent)));
        }

        override public void processEntity(Entity entity)
        {
            entity.GetComponent<ChildComponent>().Counter++;
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

    internal class Component3
    {
        public int Counter;
    }

    internal class Component4
    {
        public int Counter;
    }

    internal class HealthComponent
    {
        public int Health;
    }

    internal class ParentComponent
    {
    }

    internal class ChildComponent
    {
        public int Counter;
    }
}
