namespace KeypadDecoder
{
    /// <summary>
    /// Represents a key press grouping on a keypad,
    /// consisting of a key character and the number of consecutive presses.
    /// </summary>
    public struct Token
    {
        public readonly char Key;
        public readonly int Count;

        public Token(char key, int count)
        {
            Key = key;
            Count = count;
        }
    }


    /// <summary>
    /// Converts a raw keypad input string into a sequence of <see cref="Token"/> objects,
    /// grouping consecutive key presses, handling breaks, special keys, and termination.
    /// </summary>
    public class Tokenizer
    {
        /// <summary> Contains the set of keys that produce characters when pressed (e.g., {'2', '3', '4'}). </summary>
        readonly HashSet<char> CharacterKeys;

        /// <summary> Set of keys with special behavior such as backspace (e.g., {'*'}). </summary>
        readonly HashSet<char> SpecialKeys;

        /// <summary> Character used to indicate a pause between letters entered using the same key. </summary>
        readonly char Breaker;

        /// <summary> Character that marks the end of the input sequence. </summary>
        readonly char Terminator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tokenizer"/> class
        /// with the given key definitions and control characters.
        /// </summary>
        public Tokenizer(HashSet<char> characterKeys, HashSet<char> specialKeys, char breaker, char terminator)
        {

            if (breaker == '\0' || terminator == '\0')
            {
                throw new InvalidTokenizerParamsException(breaker, terminator);
            }
            CharacterKeys = characterKeys;
            SpecialKeys = specialKeys;
            Breaker = breaker;
            Terminator = terminator;
        }


        /// <summary>
        /// Converts the provided input string into a list of <see cref="Token"/> objects.
        /// Consecutive identical key presses are grouped into a single token.
        /// Special keys are not grouped - each special key produce a seperate token.
        /// Break and terminator are handled as defined.
        /// </summary>
        /// <param name="input">
        /// The raw keypad input (e.g., "4433555 555666#").
        /// Must include the terminator character.
        /// </param>
        /// <returns>
        /// A list of <see cref="Token"/> objects representing the parsed key press sequence.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown if an unrecognized character is encountered,
        /// or if the input does not end with the terminator.
        /// </exception>
        public List<Token> Tokenize(string input)
        {
            input = input.Trim();

            var tokens = new List<Token>();

            char prevCharacter = '\0';
            int pressCount = 0;

            bool terminated = false;

            // Pushes the current accumulated key and count to the token list.
            Action pushState = () => {
                if (pressCount > 0 && prevCharacter != '\0') tokens.Add(new Token(prevCharacter, pressCount));
                pressCount = 0;
            };

            for (int index = 0; index < input.Length; index++)
            {
                char currentCharacter = input[index];
                if (CharacterKeys.Contains(currentCharacter))
                {
                    if (currentCharacter != prevCharacter)
                    {
                        pushState();
                    }
                    pressCount++;
                }
                else if (SpecialKeys.Contains(currentCharacter))
                {
                    pushState();
                    pressCount=1;
                }
                else if (currentCharacter == Breaker)
                {
                    pushState();
                }
                else if (currentCharacter == Terminator)
                {
                    pushState();
                    terminated = true;
                    break;
                }
                else
                {
                    throw new UnknownKeyException(
                        currentCharacter,
                        $"Unreconized character {currentCharacter}  at position {index} in input '{input}'"
                    );

                }
                prevCharacter = currentCharacter;
            }

            if (!terminated)
            {
                throw new TerminatorNotFoundException(
                    Terminator,
                    $"Given input {input} is not terminated by {Terminator}"
                );
            }

            return tokens;
        }
    }
}
