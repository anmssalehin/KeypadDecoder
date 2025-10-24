using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeypadDecoder.Test
{
    [TestClass]
    public class KeyProcessorTest
    {
        [TestMethod]
        public void TestCharacterB()
        {
            var prevState = new StringBuilder("xsd", 10);
            new CharacterKeyProcessor('2', "abc").Process(prevState, new Token('2', 2));
            Assert.AreEqual(prevState.ToString(), "xsdb");
        }

        [TestMethod]
        public void TestBackspace()
        {
            var prevState = new StringBuilder("xsd", 10);
            new BackKeyProcessor('*').Process(prevState, new Token('*', 1));
            Assert.AreEqual(prevState.ToString(), "xs");
        }

        [TestMethod]
        public void TestBackspace2()
        {
            var prevState = new StringBuilder("xs", 10);
            new BackKeyProcessor('*').Process(prevState, new Token('*', 1));
            Assert.AreEqual(prevState.ToString(), "x");
            new BackKeyProcessor('*').Process(prevState, new Token('*', 1));
            Assert.AreEqual(prevState.ToString(), "");
            new BackKeyProcessor('*').Process(prevState, new Token('*', 1));
            Assert.AreEqual(prevState.ToString(), "");
        }

        [TestMethod]
        public void TestTokenMisalignment()
        {
            try
            {
                var prevState = new StringBuilder("xsd", 10);
                new CharacterKeyProcessor('2', "abc").Process(prevState, new Token('3', 2));
            }
            catch (TokenProcessMisalignmentException ex)
            {
                Assert.AreEqual(ex.ProcessKey, '2');
                Assert.AreEqual(ex.TokenKey, '3');
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
