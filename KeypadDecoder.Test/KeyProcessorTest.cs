using System.Text;

namespace KeypadDecoder.Test
{
    /// <summary>
    /// Unit tests for <see cref="CharacterKeyProcessor"/> and <see cref="BackKeyProcessor"/>.
    /// </summary>
    [TestClass]
    public class KeyProcessorTest
    {
        /// <summary>
        /// Verifies that pressing a character key multiple times appends the correct letter.
        /// </summary>
        [TestMethod]
        public void TestCharacterB()
        {
            var prevState = new StringBuilder("xsd", 10);
            new CharacterKeyProcessor('2', "abc").Process(prevState, new Token('2', 2));
            Assert.AreEqual(prevState.ToString(), "xsdb");
        }

        /// <summary>
        /// Verifies that the backspace key removes the last character from the input state.
        /// </summary>
        [TestMethod]
        public void TestBackspace()
        {
            var prevState = new StringBuilder("xsd", 10);
            new BackKeyProcessor('*').Process(prevState, new Token('*', 1));
            Assert.AreEqual(prevState.ToString(), "xs");
        }

        /// <summary>
        /// Verifies that the backspace key behaves correctly state even when it's empty.
        /// </summary>
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

        /// <summary>
        /// Verifies that processing a token with a key mismatch 
        /// throws a <see cref="TokenProcessMisalignmentException"/> with correct details.
        /// </summary>
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
