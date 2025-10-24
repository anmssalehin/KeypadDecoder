namespace KeypadDecoder.Test
{
    /// <summary>
    /// Unit tests for the <see cref="Tokenizer"/> class.
    /// Verifies that input strings are correctly tokenized into key + press count sequences.
    /// Also tests handling of invalid input and tokenizer initialization errors.
    /// </summary>
    [TestClass]
    public sealed class TokenizerTest
    {
        Tokenizer Tokenizer;

        /// <summary>
        /// Initializes a tokenizer instance for tests with digits 2-9, backspace (*), space as breaker, and '#' as terminator.
        /// </summary>
        public TokenizerTest()
        {
            Tokenizer = new Tokenizer(
                new HashSet<char> { '2', '3', '4', '5', '6', '7', '8', '9' },
                new HashSet<char> { '*' },
                ' ',
                '#'
            );
        }

        /// <summary>
        /// Verifies that repeated key presses of the same key are grouped correctly.
        /// </summary>
        bool TestFor(string input, List<Token> expectedTokens)
        {
            var computedTokens = Tokenizer.Tokenize(input);
            if (computedTokens.Count != expectedTokens.Count) return false;
            for (int i = 0; i < computedTokens.Count; i++)
            {
                if (computedTokens[i].Key != expectedTokens[i].Key) return false;
                if (computedTokens[i].Count != expectedTokens[i].Count) return false;
            }
            return true;
        }

        /// <summary>
        /// Verifies that separated key presses produce separate tokens.
        /// </summary>
        [TestMethod]
        public void Decode22()
        {
            Assert.IsTrue(
                TestFor(
                    "22#",
                    new List<Token> {
                        new Token('2', 2)
                    }
                )
            );
        }

        /// <summary>
        /// Verifies that characters after terminator are ignored.
        /// </summary>
        [TestMethod]
        public void ShouldTokenizeRepeatedKeyPresses()
        {
            Assert.IsTrue(
                TestFor(
                    "22#22",
                    new List<Token> {
                        new Token('2', 2)
                    }
                )
            );
        }

        [TestMethod]
        public void ShouldTokenizeSeparatedKeyPresses()
        {
            Assert.IsTrue(
                TestFor(
                    " 22 2#",
                    new List<Token>
                    {
                        new Token('2', 2),
                        new Token('2', 1),
                    }
                )
            );
        }


        /// <summary>
        /// Verifies that multiple different key sequences are tokenized correctly.
        /// </summary>
        [TestMethod]
        public void ShouldTokenizeMultipleKeySequences()
        {
            Assert.IsTrue(
                TestFor(
                   "33 222#",
                   new List<Token>
                   {
                        new Token('3', 2),
                        new Token('2', 3),
                   }
               )
           );
        }


        /// <summary>
        /// Verifies that multiple different key sequences are tokenized correctly even when separator is used.
        /// </summary>
        [TestMethod]
        public void ShouldTokenizeSeperatedMultipleKeySequences()
        {
            Assert.IsTrue(
                TestFor(
                    "332 22#",
                    new List<Token>
                    {
                        new Token('3', 2),
                        new Token('2', 1),
                        new Token('2', 2),
                    }
                )
            );
        }

        /// <summary>
        /// Verifies that alternating sequences are tokenized correctly.
        /// </summary>
        [TestMethod]
        public void ShouldTokenizeSeperatedAlternatingKeySequences()
        {
            Assert.IsTrue(TestFor(
               "3323 3232#",
               new List<Token>
               {
                    new Token('3', 2),
                    new Token('2', 1),
                    new Token('3', 1),
                    new Token('3', 1),
                    new Token('2', 1),
                    new Token('3', 1),
                    new Token('2', 1),
               }
           ));
        }

        /// <summary>
        /// Verifies that mixed sequences are tokenized correctly.
        /// </summary>
        [TestMethod]
        public void ShouldTokenizeMixedSequences()
        {
            Assert.IsTrue(TestFor(
                "922 2229 933344444#",
                new List<Token>
                {
                    new Token('9', 1),
                    new Token('2', 2),
                    new Token('2', 3),
                    new Token('9', 1),
                    new Token('9', 1),
                    new Token('3', 3),
                    new Token('4', 5),
                }
            ));
        }

        /// <summary>
        /// Verifies that unknown keys throw <see cref="UnknownKeyException"/>.
        /// </summary>
        [TestMethod]
        public void ThrowsOnUnrecognizedCharacter()
        {
            try
            {
                Tokenizer.Tokenize("2A#"); // 'A' is invalid
            }
            catch (UnknownKeyException ex)
            {
                Assert.AreEqual(ex.Key, 'A');
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        /// <summary>
        /// Verifies that input without the terminator character '#' throws <see cref="TerminatorNotFoundException"/>.
        /// </summary>
        [TestMethod]
        public void ThrowsIfNotTerminated()
        {
            try
            {
                Tokenizer.Tokenize("22"); // missing '#'
            }
            catch (TerminatorNotFoundException ex)
            {
                Assert.AreEqual(ex.Terminator, '#');
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        /// <summary>
        /// Verifies that a tokenizer cannot be initialized with a null breaker character.
        /// </summary>
        [TestMethod]
        public void ThrowsIfBreakerIsNull()
        {
            try
            {
                new Tokenizer(
                    new HashSet<char> { '2', '3', '4', '5', '6', '7', '8', '9' },
                    new HashSet<char> { '*' },
                    '\0',
                    '#'
                );
            }
            catch (InvalidTokenizerParamsException ex)
            {
                Assert.AreEqual(ex.Breaker, '\0');
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Verifies that a tokenizer cannot be initialized with a null terminator character.
        /// </summary>
        [TestMethod]
        public void ThrowsIfTerminatorIsNull()
        {
            try
            {
                new Tokenizer(
                    new HashSet<char> { '2', '3', '4', '5', '6', '7', '8', '9' },
                    new HashSet<char> { '*' },
                    ' ',
                    '\0'
                );
            }
            catch (InvalidTokenizerParamsException ex)
            {
                Assert.AreEqual(ex.Terminator, '\0');
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
