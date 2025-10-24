using System.Text;

namespace KeypadDecoder
{
    /// <summary>
    /// Represents a fully configured keypad capable of decoding
    /// tokenized key sequences into text output.
    /// </summary>
    public class KeyPad
    {
        private readonly Dictionary<char, KeyProcessor> Processors;
        private readonly Tokenizer Tokenizer;


        public KeyPad(Dictionary<char, KeyProcessor> processors, Tokenizer tokenizer)
        {
            Processors = processors;
            Tokenizer = tokenizer;
        }


        /// <summary>
        /// Decodes a keypad input string (e.g., "4433555 555666#") into readable text.
        /// </summary>
        /// <param name="input">The raw keypad input ending with a terminator.</param>
        /// <returns>The decoded message string.</returns>
        /// <exception cref="KeyProcessorNotFoundException">Thrown when no processor exists for a given key.</exception>
        public string Decode(string input)
        {
            var tokens = Tokenizer.Tokenize(input);
            var ans = new StringBuilder("", tokens.Count);
            foreach (var token in tokens)
            {
                if (!Processors.ContainsKey(token.Key))
                {
                    throw new KeyProcessorNotFoundException(
                        token.Key,
                        input,
                        $"Processor not found for {token.Key} on input {input}"
                    );
                }
                Processors[token.Key].Process(ans, token);
            }
            return ans.ToString();
        }
    }



    /// <summary>
    /// Builder for configuring and constructing <see cref="KeyPad"/> instances.
    /// Supports chaining for flexible setup of keypad layout and control keys.
    /// </summary>
    public class PadBuilder
    {
        private Dictionary<char, KeyProcessor> Processors;
        private HashSet<char> CharacterKeys;
        private HashSet<char> SpecialKeys;
        private char Breaker;
        private char Terminator;

        public PadBuilder()
        {
            Processors = new Dictionary<char, KeyProcessor>();
            CharacterKeys = new HashSet<char>();
            SpecialKeys = new HashSet<char>();
            Breaker = '\0';
            Terminator = '\0';
        }

        public PadBuilder SetBreaker(char breaker)
        {
            Breaker = breaker;
            return this;
        }

        public PadBuilder SetTerminator(char terminator)
        {
            Terminator = terminator;
            return this;
        }

        public PadBuilder AddCondensedKeyProcessor(KeyProcessor processor)
        {
            CharacterKeys.Add(processor.Label);
            Processors[processor.Label] = processor;
            return this;
        }

        public PadBuilder AddSpecialKeyProcessor(KeyProcessor processor)
        {
            SpecialKeys.Add(processor.Label);
            Processors[processor.Label] = processor;
            return this;
        }

        /// <summary>
        /// Finalizes the configuration and creates a new <see cref="KeyPad"/> instance.
        /// </summary>
        /// <returns>A fully configured keypad ready to decode input strings.</returns>
        public KeyPad build()
        {
            return new KeyPad(
                new Dictionary<char, KeyProcessor>(Processors),
                new Tokenizer(CharacterKeys, SpecialKeys, Breaker, Terminator)
            );
        }
    }
}
