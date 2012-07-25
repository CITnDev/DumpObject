using System;
using System.Collections.Generic;
using System.Linq;
using DumpObject;
using NUnit.Framework;

namespace DumpObjectTests
{
    public class DumpCollectionTests
    {
        private Dumper _dumper;
        [TestFixtureSetUp]
        public void InitializeTestSuite()
        {
            _dumper = new Dumper();
        }

        [Test]
        public void DumpDictionaryTests()
        {
            var dict = new Dictionary<int, int>();

            for (int i = 0; i < 10; i++)
            {
                dict[i] = 10 - i;
            }

            var dump = _dumper.Dump(dict);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.IsNull(dump.Value, "Dump value shall be null.");
            Assert.AreEqual("10 items", dump.Header, "Dump header shall be '10 items'.");
            Assert.AreEqual(10, dump.Count(), "Dump children count shall be 10.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");

            var children = new List<DumpLevel>(dump);

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i.ToString(), children[i].Header, "Header shall be '" + i + "'.");
                Assert.AreEqual(typeof(int), children[i].Type, "Type shall be 'int'.");
                Assert.AreEqual(10-i, children[i].Value, "Value shall be '" + (10 - i) + "'.");
                Assert.AreEqual(0, children[i].Count(), "Children count shall be 0.");
            }
        }

        [Test]
        public void DumpListTests()
        {
            var list = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }

            var dump = _dumper.Dump(list);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.IsNull(dump.Value, "Dump value shall be null.");
            Assert.AreEqual("10 items", dump.Header, "Dump header shall be '10 items'.");
            Assert.AreEqual(10, dump.Count(), "Dump children count shall be 10.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");

            var children = new List<DumpLevel>(dump);

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i.ToString(), children[i].Header, "Header shall be '" + i + "'.");
                Assert.AreEqual(typeof(int), children[i].Type, "Type shall be 'int'.");
                Assert.AreEqual(i, children[i].Value, "Value shall be '" + i + "'.");
                Assert.AreEqual(0, children[i].Count(), "Children count shall be 0.");
            }
        }
    }
}
