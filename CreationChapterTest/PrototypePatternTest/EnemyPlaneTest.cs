using PrototypePattern;

using System.Diagnostics;

namespace PrototypePatternTest
{
    [TestClass]
    public class EnemyPlaneTest
    {
        [TestMethod]
        public void CtorTakeLongTime_Bad_ReturnsTrue()
        {
            var factory = new PrototypeFactory();

            var result = factory.CtorTakeLongTime();
            Debug.WriteLine($"cotrTime:{result.cotrTime} factoryTime:{result.factoryTime}");
            Assert.IsTrue(result.res);
        }
    }
}