namespace KeypadDecoder.Test
{
    [TestClass]
    public class PadTest
    {
        KeyPad Pad;

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

        [TestMethod]
        public void Decode223b33()
        {
            try
            {
                Assert.AreEqual(Pad.Decode("223*33#"), "be");
            } catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Decode222222223b33()
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
