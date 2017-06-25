using System;
using System.Diagnostics;

namespace Savoa.Benchmark
{
    class Benchmark
    {
        static void Main(string[] args)
        {
            Benchmark program = new Benchmark();
            program.Run();
        }

        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Engine engine = new Engine();
            MoveSystem moveSystem = new MoveSystem();
            engine.AddSystem(moveSystem);
            for (int i = 0; i < 10000; i++)
            {
                engine.AddEntity(newMover());
            }
            for (int i = 0; i < 10000; i++)
            {
                engine.Process();
            }
            //4.69
            //4.9
            //4.96
            stopwatch.Stop();
            Console.Out.WriteLine("Processed={0}", moveSystem.Counter);
            Console.Out.WriteLine("Elapsed={0}", stopwatch.Elapsed);
        }

        private Entity newMover()
        {
            Entity entity = new Entity();
            entity.AddComponent(new MoveComponent());
            return entity;
        }
    }

    class MoveComponent
    {
        public int X;
        public int Y;
    }

    class MoveSystem : IteratingEntitySystem
    {
        public int Counter;

        public override void AddedToEngine(Engine engine)
        {
            entities = engine.EntitiesFor(new Family(typeof(MoveComponent)));
        }

        public override void processEntity(Entity entity)
        {
            Counter++;
            MoveComponent moveComponent = entity.GetComponent<MoveComponent>();
            moveComponent.X += 1;
            moveComponent.Y += 1;
        }
    }
}
