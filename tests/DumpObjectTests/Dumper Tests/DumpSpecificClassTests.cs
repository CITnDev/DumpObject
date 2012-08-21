using System.Globalization;
using System.Linq;
using DumpObject;
using NUnit.Framework;

namespace DumpObjectTests
{
    [TestFixture]
    public class DumpSpecificClassTests
    {
        private Dumper _dumper;
        [TestFixtureSetUp]
        public void InitializeTestSuite()
        {
            _dumper = new Dumper();
        }

        [Test]
        public void DumpCultureInfo_fr()
        {
            var culture = CultureInfo.GetCultureInfo("fr");
            var dump = _dumper.Dump(culture);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual("fr", dump.Value, "Dump value shall be True.");
            Assert.AreEqual(typeof(CultureInfo), dump.Type, "Dump type shall be a CultureInfo.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");
        }

        [Test]
        public void DumpCultureInfo_frFR()
        {
            var culture = CultureInfo.GetCultureInfo("fr-FR");
            var dump = _dumper.Dump(culture);

            Assert.NotNull(dump, "Dump shall return a DumpLevel instance.");
            Assert.AreEqual("fr-FR", dump.Value, "Dump value shall be True.");
            Assert.AreEqual(typeof(CultureInfo), dump.Type, "Dump type shall be a CultureInfo.");
            Assert.AreEqual(0, dump.Count(), "Dump children count shall be 0.");
            Assert.AreEqual(0, dump.Level, "Dump level shall be 0.");
        }
    }
}
