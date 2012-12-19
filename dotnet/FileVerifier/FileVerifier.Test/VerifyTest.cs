using System.IO;
using Dusklake.FileVerifier;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FileVerifier.Test
{
    /// <summary>
    ///This is a test class for VerifyTest and is intended
    ///to contain all VerifyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VerifyTest
    {
        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        /// <summary>
        ///A test for IsPdf
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Files/pdf1.pdf")]
        public void IsPdfTestSuccess()
        {
            var fileToTest = File.ReadAllBytes("Files/pdf1.pdf");

            var result = Verify.IsPdf(fileToTest);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        [DeploymentItem("Files/png1.png")]
        public void IsPdfTestFail()
        {
            var fileToTest = File.ReadAllBytes("Files/png1.png");

            var result = Verify.IsPdf(fileToTest);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        [DeploymentItem("Files/png1.png")]
        public void IsPngTestSuccess()
        {
            var fileToTest = File.ReadAllBytes("Files/png1.png");

            var result = Verify.IsPng(fileToTest);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        [DeploymentItem("Files/pdf1.pdf")]
        public void IsPngTestFail()
        {
            var fileToTest = File.ReadAllBytes("Files/pdf1.pdf");

            var result = Verify.IsPng(fileToTest);

            Assert.IsFalse(result);
        }
        [TestMethod()]
        [DeploymentItem("Files/jpg1.jpg")]
        public void IsJpgTestSuccess()
        {
            var fileToTest = File.ReadAllBytes("Files/jpg1.jpg");

            var result = Verify.IsJpg(fileToTest);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        [DeploymentItem("Files/pdf1.pdf")]
        public void IsJpgTestFail()
        {
            var fileToTest = File.ReadAllBytes("Files/pdf1.pdf");

            var result = Verify.IsJpg(fileToTest);

            Assert.IsFalse(result);
        }
    }
}
