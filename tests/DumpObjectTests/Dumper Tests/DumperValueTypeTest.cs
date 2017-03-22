using System;
using System.Linq;
using DumpObject;
using NUnit.Framework;
using System.Numerics;

namespace DumpObjectTests
{
    [TestFixture]
    public class DumperValueTypeTest
    {
        private Dumper _dumper;
        [OneTimeSetUp]
        public void InitializeTestSuite()
        {
            _dumper = new Dumper();
        }

        [Test]
        public void TestGet()
        {
            //string expr = "()=>_dumper.MaxDumpLevel";
            //Expression.Lambda<Func<_dumper.MaxDumpLevel.GetType()>>(expr);
        }

        [Test]
        public void DumpBooleanValueType()
        {
            var dump = _dumper.Dump(true);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual(true, dump.Value, "Dump value shall be True.");
            Assert.AreEqual(typeof(bool), dump.Type, "Dump type shall be a Boolean.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");
        }

        [Test]
        public void DumpDateTimeValueType()
        {
            var dump = _dumper.Dump(DateTime.Today);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual(DateTime.Today, dump.Value, "Dump value shall be today.");
            Assert.AreEqual(typeof(DateTime), dump.Type, "Dump type shall be a DateTime.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");
        }

        [Test]
        public void DumpIntValueType()
        {
            var dump = _dumper.Dump((int)5);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual(5, dump.Value, "Dump value shall be 5.");
            Assert.AreEqual(typeof(int), dump.Type, "Dump type shall be an integer.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");
        }

        [Test]
        public void DumpNullableWithValue()
        {
            int? value = 10;
            var dump = _dumper.Dump(value);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual(10, dump.Value, "Dump value shall be 10.");
            Assert.AreEqual(typeof(int?), dump.Type, "Dump type shall be a Nullable<int>.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");
        }

        [Test]
        public void DumpNullableWithoutValue()
        {
            int? value = null;
            var dump = _dumper.Dump(value);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual(null, dump.Value, "Dump value shall be 10.");
            Assert.AreEqual(typeof(int?), dump.Type, "Dump type shall be a Nullable<int>.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");
        }

        [Test]
        public void DumpStringValueType()
        {
            var dump = _dumper.Dump("value");

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual("value", dump.Value, "Dump value shall be 'value'.");
            Assert.AreEqual(typeof(string), dump.Type, "Dump type shall be a string.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");
        }

        [Test]
        public void DumpBigIntegerValueType()
        {
            var bi = new BigInteger(1);
            var dump = _dumper.Dump(bi);
            
            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual("1", dump.Value, "Dump value shall be 1.");
            Assert.AreEqual(typeof(BigInteger), dump.Type, "Dump type shall be a BigInteger.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");
        }
    }
}
