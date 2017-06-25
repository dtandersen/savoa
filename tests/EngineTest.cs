using Xunit;

namespace Savoa
{
    public class EngineTest
    {
        [Fact]
        public void AddEntityToEngine()
        {
            Engine engine = new Engine();
            Entity entity1 = new Entity();
            engine.AddEntity(entity1);

            engine.Process();

            Assert.True(engine.ContainsEntity(entity1));

            Entity entity2 = new Entity();
            engine.AddEntity(entity2);

            engine.Process();

            Assert.True(engine.ContainsEntity(entity1));
            Assert.True(engine.ContainsEntity(entity2));

            engine.RemoveEntity(entity2);

            engine.Process();
            
            Assert.True(engine.ContainsEntity(entity1));
            Assert.False(engine.ContainsEntity(entity2));

            engine.RemoveEntity(entity1);

            engine.Process();

            Assert.False(engine.ContainsEntity(entity1));
            Assert.False(engine.ContainsEntity(entity2));
        }
    }
}
