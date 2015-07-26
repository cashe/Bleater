using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codurance.Bleater.Test
{
    [TestClass]
    public abstract class TestBase
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

    }
}
