using System.Collections.Generic;
using System.Linq;
using DumpObject;
using NUnit.Framework;

namespace DumpObjectTests
{
    [TestFixture]
    public class DumpStructTest
    {
        public struct TestStruct
        {
            public string Property { get; set; }
            public string Field;
        }

        [Test]
        public void DumpEmptyStruct()
        {
            var testStruct = new TestStruct();
            var dumper = new Dumper();

            var dump = dumper.Dump(testStruct);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual(null, dump.Value, "Dump value shall be null.");
            Assert.AreEqual(typeof(TestStruct), dump.Type, "Dump type shall be a TestStruct type.");
            Assert.AreEqual(1, dump.Count(), "Dump children count shall be 1.");
            Assert.AreEqual(0, dump.Level, "Dump level count shall be 0.");
            var children = new List<DumpLevel>(dump);
            Assert.AreEqual(1, children.Count, "Dump IEnumerable copy shall be count 1 item.");

            Assert.AreEqual("Property", children[0].Header, "First children header shall be 'Property'.");
            Assert.AreEqual(typeof(string), children[0].Type, "First children type shall be a string.");
            Assert.AreEqual(null, children[0].Value, "First children value shall be null.");
            Assert.AreEqual(1, children[0].Level, "First children level shall be 1.");
        }
    }
}
