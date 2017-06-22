using System;
using Xunit;

namespace tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Engine engine = new Engine();
            Entity entity1 = new Entity();
            engine.add(entity1);
            Assert.True(engine.hasEntity(entity1));

            Entity entity2 = new Entity();
            engine.add(entity2);
            Assert.True(engine.hasEntity(entity1));
            Assert.True(engine.hasEntity(entity2));

            engine.remove(entity2);
            Assert.True(engine.hasEntity(entity1));
            Assert.False(engine.hasEntity(entity2));

            engine.remove(entity1);
            Assert.False(engine.hasEntity(entity1));
            Assert.False(engine.hasEntity(entity2));
        }
    }
}
