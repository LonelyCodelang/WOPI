using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainWeb.Helpers;

namespace MainWeb.Tests.Crypto
{
    [TestClass]
    public class KeyGenTests
    {
        [TestMethod]
        public void Generate_Simple_Hash()
        {
            //arrange
            KeyGen keygen = new KeyGen();

            //act
            var r1 = keygen.GetHash("test.docx");
            var r2 = keygen.GetHash("test.docx");

            //assert
            Assert.AreEqual(r2, r1, "hash doesn't match");

        }
    }
}
