using System;
using System.Collections.Generic;
using System.Linq;
using DumpObject;
using NUnit.Framework;

namespace DumpObjectTests
{
    public class DumpObjectTest
    {
        private Dumper _dumper;
        [TestFixtureSetUp]
        public void InitializeTestSuite()
        {
            _dumper = new Dumper();
        }

        public class ObjectWithValueTypeTest
        {
            public ObjectWithValueTypeTest()
            {
                PublicProperty2 = null;

                PublicProperty = "publicProp";
                PublicField = "publicField";

                ProtectedProperty = 10;
                ProtectedField = -10;

                PrivateProperty = DateTime.Today;
                _privateField = DateTime.Today.AddDays(-1);
            }

            public string PublicProperty { get; set; }
            public decimal? PublicProperty2 { get; set; }
            protected long ProtectedProperty { get; set; }
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            private DateTime PrivateProperty { get; set; }
            // ReSharper restore UnusedAutoPropertyAccessor.Local

            public string PublicField;
            protected long ProtectedField;
            // ReSharper disable NotAccessedField.Local
            private DateTime _privateField;
            // ReSharper restore NotAccessedField.Local
        }

        public class ObjectWithObjectTest
        {
            public ObjectWithObjectTest()
            {
                PublicProperty = "publicProp";
                Instance = new ObjectWithValueTypeTest();
            }

            public string PublicProperty { get; set; }
            public ObjectWithValueTypeTest Instance { get; set; }
            public ObjectWithValueTypeTest NullValue { get; set; }
        }

        [Test]
        public void DumpObjectWithValueTypeTest()
        {
            var valueTest = new ObjectWithValueTypeTest();
            var dump = _dumper.Dump(valueTest);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual(null, dump.Value, "Dump value shall be null.");
            Assert.AreEqual(typeof(ObjectWithValueTypeTest), dump.Type, "Dump type shall be a ObjectWithValueTypeTest type.");
            Assert.AreEqual(2, dump.Count(), "Dump children count shall be 2.");
            Assert.AreEqual(0, dump.Level, "Dump level count shall be 0.");
            var children = new List<DumpLevel>(dump);
            Assert.AreEqual(2, children.Count, "Dump IEnumerable copy shall be count 2 items.");

            Assert.AreEqual("PublicProperty", children[0].Header, "First children header shall be 'PublicProperty'.");
            Assert.AreEqual(typeof(string), children[0].Type, "First children type shall be a string.");
            Assert.AreEqual("publicProp", children[0].Value, "First children value shall be 'publicProp'.");
            Assert.AreEqual(1, children[0].Level, "First children level shall be 1.");

            Assert.AreEqual("PublicProperty2", children[1].Header, "Second children header shall be 'PublicProperty'.");
            Assert.AreEqual(typeof(decimal?), children[1].Type, "Second children type shall be a decimal?.");
            Assert.AreEqual(null, children[1].Value, "Second children value shall be null.");
            Assert.AreEqual(1, children[1].Level, "Second children level shall be 1.");
        }

        [Test]
        public void DumpObjectWithObjectTest()
        {
            var valueTest = new ObjectWithObjectTest();

            var dump = _dumper.Dump(valueTest);

            // Root Dump
            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual(null, dump.Value, "Dump value shall be null.");
            Assert.AreEqual(typeof(ObjectWithObjectTest), dump.Type, "Dump type shall be a ObjectWithObjectTest type.");
            Assert.AreEqual(3, dump.Count(), "Dump children count shall be 3.");
            Assert.AreEqual(0, dump.Level, "Dump level count shall be 0.");
            var children = new List<DumpLevel>(dump);
            Assert.AreEqual(3, children.Count, "Dump IEnumerable copy shall be count 3 items.");

            // First child : String
            Assert.AreEqual("PublicProperty", children[0].Header, "First children header shall be 'PublicProperty'.");
            Assert.AreEqual(typeof(string), children[0].Type, "First children type shall be a string.");
            Assert.AreEqual("publicProp", children[0].Value, "First children value shall be 'publicProp'.");
            Assert.AreEqual(1, children[0].Level, "First children level shall be 1.");

            // Second child : ObjectWithValueTypeTest instance
            Assert.AreEqual("Instance", children[1].Header, "Second children header shall be 'Instance'.");
            Assert.AreEqual(typeof(ObjectWithValueTypeTest), children[1].Type, "Second children type shall be a ObjectWithValueTypeTest.");
            Assert.AreEqual(1, children[1].Level, "Second children level shall be 1.");
            Assert.IsNull(children[1].Value, "Second children value shall be null.");
            Assert.AreEqual(2, children[1].Count(), "Second children value type shall have 2 children.");

            var children2 = new List<DumpLevel>(children[1]);

            Assert.AreEqual(typeof(string), children2[0].Type, "First children type shall be a string.");
            Assert.AreEqual("PublicProperty", children2[0].Header, "First children value shall be 'PublicProperty'.");
            Assert.AreEqual("publicProp", children2[0].Value, "First children value shall be 'publicProp'.");
            Assert.AreEqual(2, children2[0].Level, "First children level shall be 2.");

            Assert.AreEqual(typeof(decimal?), children2[1].Type, "First children type shall be a Nullable<decimal>.");
            Assert.AreEqual("PublicProperty2", children2[1].Header, "First children value shall be 'PublicProperty2'.");
            Assert.IsNull(children2[1].Value, "Value shall be null.");
            Assert.AreEqual(2, children2[1].Level, "First children level shall be 2.");


            // Second child : ObjectWithValueTypeTest null
            Assert.AreEqual("NullValue", children[2].Header, "Second children header shall be 'NullValue'.");
            Assert.AreEqual(typeof(ObjectWithValueTypeTest), children[2].Type, "Second children type shall be a ObjectWithValueTypeTest.");
            Assert.AreEqual(1, children[2].Level, "Second children level shall be 1.");
            Assert.Null(children[2].Value, "Second children value shall be null.");

        }
    }
}
