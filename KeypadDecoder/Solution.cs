namespace KeypadDecoder
{
    /// <summary>
    /// Provides a static interface for decoding old phone keypad input strings.
    /// </summary>
    public static class Solution
    {
        private static KeyPad? oldPhonePadinstance = null;


        /// <summary>
        /// Decodes an input string according to classic phone keypad mapping rules.
        /// </summary>
        /// <param name="input">The raw input string (must include '#').</param>
        /// <returns>The decoded string, or an empty string if decoding fails.</returns>
        public static string OldPhonePad(string input)
        {
            var result = Decode(input);
            if (result == null) return "";
            return result;
        }

        /// <summary>
        /// Internal decode method with exception handling and keypad initialization.
        /// Initializes the keypad lazily on first use.
        /// </summary>
        private static string? Decode(string input)
        {
            if (oldPhonePadinstance == null)
            {
                oldPhonePadinstance = new PadBuilder()
                    .SetBreaker(' ')
                    .SetTerminator('#')
                    .AddSpecialKeyProcessor(new BackKeyProcessor('*'))
                    .AddCondensedKeyProcessor(new CharacterKeyProcessor('2', "abc"))
                    .AddCondensedKeyProcessor(new CharacterKeyProcessor('3', "def"))
                    .AddCondensedKeyProcessor(new CharacterKeyProcessor('4', "ghi"))
                    .AddCondensedKeyProcessor(new CharacterKeyProcessor('5', "jkl"))
                    .AddCondensedKeyProcessor(new CharacterKeyProcessor('6', "mno"))
                    .AddCondensedKeyProcessor(new CharacterKeyProcessor('7', "pqrs"))
                    .AddCondensedKeyProcessor(new CharacterKeyProcessor('8', "tuv"))
                    .AddCondensedKeyProcessor(new CharacterKeyProcessor('9', "wxyz"))
                    .build();
            }
            try
            {
                return oldPhonePadinstance.Decode(input);
            }
            catch (KeyProcessorNotFoundException e)
            {
                Console.WriteLine($"Configuration error: {e.Message}");
            }
            catch (UnknownKeyException e)
            {
                Console.WriteLine($"Invalid key: '{e.Key}' in input");
            }
            catch (TerminatorNotFoundException e)
            {
                Console.WriteLine($"Missing terminator '{e.Terminator}'");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }

            return null;
        }
    }
}