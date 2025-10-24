namespace KeypadDecoder
{
    /// <summary>
    /// Thrown when the tokenizer is initialized with invalid or missing parameters.
    /// </summary>
    public class InvalidTokenizerParamsException : Exception
    {
        public readonly char Breaker;
        public readonly char Terminator;

        public InvalidTokenizerParamsException(char breaker, char terminator)
            : base(
                $"Tokenizer Parameter Error : " +
                $"{(breaker == '\0' ? "Breaker is null" : "")} " +
                $"{(terminator == '\0' ? "Terminator is null" : "")}"
            )
        {
            Breaker = breaker;
            Terminator = terminator;
        }
    }

    /// <summary>
    /// Thrown when input text does not contain the required terminator key (e.g. '#').
    /// </summary>
    public class TerminatorNotFoundException : Exception
    {
        public readonly char Terminator;

        public TerminatorNotFoundException(char terminator, string message) : base(message)
        {
            Terminator = terminator;
        }
    }

    /// <summary>
    /// Thrown when an unrecognized key is encountered during tokenization.
    /// </summary>
    public class UnknownKeyException : Exception
    {
        public readonly char Key;

        public UnknownKeyException(char key, string message) : base(message)
        {
            Key = key;
        }
    }

    /// <summary>
    /// Thrown when a token is routed to an incorrect key processor.
    /// </summary>
    public class TokenProcessMisalignmentException : Exception
    {
        public readonly char TokenKey;
        public readonly char ProcessKey;

        public TokenProcessMisalignmentException(char tokenKey, char processKey, string message) : base(message)
        {
            TokenKey = tokenKey;
            ProcessKey = processKey;
        }
    }

    /// <summary>
    /// Thrown when no matching key processor is found for a given token.
    /// </summary>
    public class KeyProcessorNotFoundException : Exception
    {
        public readonly char Key;
        public readonly string Input;

        public KeyProcessorNotFoundException(char key, string input, string message) : base(message)
        {
            Key = key;
            Input = input;
        }
    }
}
