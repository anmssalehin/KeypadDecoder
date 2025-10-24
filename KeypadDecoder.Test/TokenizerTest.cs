namespace KeypadDecoder.Test
{
    [TestClass]
    public sealed class TokenizerTest
    {
        Tokenizer Tokenizer;

        public TokenizerTest()
        {
            Tokenizer = new Tokenizer(
                new HashSet<char> { '2', '3', '4', '5', '6', '7', '8', '9' },
                new HashSet<char> { '*' },
                ' ',
                '#'
            );
        }


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

        [TestMethod]
        public void Decode22_2()
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


        [TestMethod]
        public void Decode33_222()
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


        [TestMethod]
        public void Decode332_22()
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

        [TestMethod]
        public void Decode3323_3232()
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

        [TestMethod]
        public void Decode922_2229_933344444()
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
