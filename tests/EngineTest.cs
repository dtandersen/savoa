using System;
using Xunit;

namespace Savoa
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Engine engine = new Engine(new NullLogger());
            Entity entity1 = new Entity();
            engine.AddEntity(entity1);
            Assert.True(engine.ContainsEntity(entity1));

            Entity entity2 = new Entity();
            engine.AddEntity(entity2);
            Assert.True(engine.ContainsEntity(entity1));
            Assert.True(engine.ContainsEntity(entity2));

            engine.RemoveEntity(entity2);
            Assert.True(engine.ContainsEntity(entity1));
            Assert.False(engine.ContainsEntity(entity2));

            engine.RemoveEntity(entity1);
            Assert.False(engine.ContainsEntity(entity1));
            Assert.False(engine.ContainsEntity(entity2));
        }
    }
}
