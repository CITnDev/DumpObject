using System.Linq;
using DumpObject;
using NUnit.Framework;

namespace DumpObjectTests
{
    [TestFixture]
    public class DumpEnumTest
    {
        [Test]
        public void DumpEnumZero()
        {
            var value = MyEnum.Zero;

            var dumper = new Dumper();

            var dump = dumper.Dump(value);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual("MyEnum.Zero", dump.Value, "Dump value shall be 'MyEnum.Zero'.");
            Assert.AreEqual(typeof(MyEnum), dump.Type, "Dump type shall be a MyEnum type.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
        }

        [Test]
        public void DumpEnumOne()
        {
            var value = MyEnum.One;

            var dumper = new Dumper();

            var dump = dumper.Dump(value);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual("MyEnum.One", dump.Value, "Dump value shall be 'MyEnum.One'.");
            Assert.AreEqual(typeof(MyEnum), dump.Type, "Dump type shall be a MyEnum type.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
        }

        public enum MyEnum
        {
            Zero = 0,
            One = 1
        }
    }
}
