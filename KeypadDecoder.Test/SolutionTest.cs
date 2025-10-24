namespace KeypadDecoder.Test
{
    /// <summary>
    /// Unit tests for the <see cref="Solution"/> static interface.
    /// Verifies that <see cref="Solution.OldPhonePad"/> correctly decodes
    /// </summary>
    [TestClass]
    public class SolutionTest
    {
        /// <summary>
        /// Verifies decoding of a single repeated key press.
        /// </summary>
        [TestMethod]
        public void VerifySingle()
        {
            var result = Solution.OldPhonePad("33#");
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "e");
        }

        /// <summary>
        /// Verifies decoding with backspace usage.
        /// </summary>
        [TestMethod]
        public void VerifyBackspace()
        {
            var result = Solution.OldPhonePad("227*#");
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "b");
        }

        /// <summary>
        /// Verifies decoding of a multi-key sequence with breaks.
        /// </summary>
        [TestMethod]
        public void VerifyMultipleKey()
        {
            var result = Solution.OldPhonePad("4433555 555666#");
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "hello");
        }

        /// <summary>
        /// Verifies decoding of a more complex sequence with backspace.
        /// </summary>
        [TestMethod]
        public void VerifyComplexSequence()
        {
            var result = Solution.OldPhonePad("8 88777444666*664#");
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "turing");
        }

        /// <summary>
        /// Verifies that input containing unsupported keys returns an empty string.
        /// </summary>
        [TestMethod]
        public void DecodeUnsupportedEncoding()
        {
            var result = Solution.OldPhonePad("1#");
            Assert.AreEqual(result, "");
        }
    }
}
