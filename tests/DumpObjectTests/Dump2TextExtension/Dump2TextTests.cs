using System;
using System.Diagnostics;
using DumpObject;
using NUnit.Framework;

namespace DumpObjectTests.Dump2TextExtension
{
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

    [TestFixture]
    class Dump2TextTests
    {
        [Test]
        public void DumpObjectWithValueType2Text()
        {
            var valueTest = new ObjectWithValueTypeTest();
            var dumper = new Dumper();

            var dumpText = dumper.Dump(valueTest).ToText();

            Trace.WriteLine(dumpText);

            var expected = "DumpObjectTests.Dump2TextExtension.ObjectWithValueTypeTest" + Environment.NewLine +
                           "	 - PublicProperty : publicProp" + Environment.NewLine +
                           "	 - PublicProperty2 : <null>";

            Assert.AreEqual(expected, dumpText);
        }

        [Test]
        public void DumpObjectWithValueTypeFirstLevel2Text()
        {
            var valueTest = new ObjectWithValueTypeTest();
            var dumper = new Dumper{MaxDumpLevel = 1};

            var dumpText = dumper.Dump(valueTest).ToText();

            Trace.WriteLine(dumpText);

            var expected = "DumpObjectTests.Dump2TextExtension.ObjectWithValueTypeTest" + Environment.NewLine +
                           "	 - PublicProperty : ..." + Environment.NewLine +
                           "	 - PublicProperty2 : ...";

            Assert.AreEqual(expected, dumpText);
        }

        [Test]
        public void DumpObjectWithObject2Text()
        {
            var valueTest = new ObjectWithObjectTest();
            var dumper = new Dumper();

            var dumpText = dumper.Dump(valueTest).ToText();

            Trace.WriteLine(dumpText);

            var expected = "DumpObjectTests.Dump2TextExtension.ObjectWithObjectTest" + Environment.NewLine +
                            "	 - PublicProperty : publicProp" + Environment.NewLine +
                            "	 - Instance : DumpObjectTests.Dump2TextExtension.ObjectWithValueTypeTest" + Environment.NewLine +
                            "		 - PublicProperty : publicProp" + Environment.NewLine +
                            "		 - PublicProperty2 : <null>" + Environment.NewLine +
                            "	 - NullValue : <null>";
            Assert.AreEqual(expected, dumpText);
        }
    }
}
