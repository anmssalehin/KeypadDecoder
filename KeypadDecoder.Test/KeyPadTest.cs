namespace KeypadDecoder.Test
{
    /// <summary>
    /// Unit tests for the <see cref="KeyPad"/> class.
    /// </summary>
    [TestClass]
    public class PadTest
    {
        KeyPad Pad;

        /// <summary>
        /// Initializes a KeyPad instance for multiple test cases
        /// </summary>
        public PadTest()
        {
            Pad = new PadBuilder()
            .SetBreaker(' ')
            .SetTerminator('#')
            .AddSpecialKeyProcessor(new BackKeyProcessor('*'))
            .AddCondensedKeyProcessor(new CharacterKeyProcessor('2', "abc"))
            .AddCondensedKeyProcessor(new CharacterKeyProcessor('3', "def"))
            .build();
        }


        /// <summary>
        /// Verifies that attempting to decode a key that has no associated processor
        /// throws a <see cref="KeyProcessorNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void ThrowsIfKeyNotFound()
        {
            var keyProcessors = new Dictionary<char, KeyProcessor>
            {
                {'2', new CharacterKeyProcessor('2', "abc") },
                {'3', new CharacterKeyProcessor('3', "def") },
            };
            var tokenizer = new Tokenizer(
                new HashSet<char> { '2', '3', '4' },
                new HashSet<char> { '*' },
                ' ',
                '#'
            );

            try
            {
                var pad = new KeyPad(keyProcessors, tokenizer);
                pad.Decode("4#");
            }
            catch (KeyProcessorNotFoundException ex)
            {
                Assert.AreEqual(ex.Key, '4');
            }
            catch (Exception ex)
            {
                Assert.Fail($"{ex.Message}");
            }
        }


        /// <summary>
        /// Verifies that decoding an input containing an unknown key 
        /// throws an <see cref="UnknownKeyException"/>.
        /// </summary>
        [TestMethod]
        public void ThrowsAtUnknownKey()
        {
            try
            {
                Pad.Decode("1#");
            }
            catch (UnknownKeyException ex)
            {
                Assert.AreEqual(ex.Key, '1');
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Verifies correct decoding of a typical key sequence with backspace usage.
        /// </summary>
        [TestMethod]
        public void VerifyTypicalScenario()
        {
            try
            {
                Assert.AreEqual(Pad.Decode("223*33#"), "be");
            } catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Verifies decoding of a long repeated key sequence with backspace.
        /// </summary>
        [TestMethod]
        public void VerifyLongSequence()
        {
            try
            {
                Assert.AreEqual(Pad.Decode("222222223*33#"), "be");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
