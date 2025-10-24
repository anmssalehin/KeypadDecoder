using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeypadDecoder
{
    /// <summary>
    /// Base class for all keypad key processors.
    /// Each processor transforms input text according to its key's behavior.
    /// </summary>
    public abstract class KeyProcessor
    {
        public readonly char Label;

        public KeyProcessor(char label)
        {
            Label = label;
        }

        public void Process(StringBuilder input, Token token)
        {
            if (token.Key != Label)
            {
                throw new TokenProcessMisalignmentException(
                    token.Key, Label,
                    $"Token {token.Key} sent to wrong processor of {Label}"
                );
            }
            Transform(input, token);
        }

        protected abstract void Transform(StringBuilder input, Token token);
    }

    /// <summary>
    /// Processes character keys (e.g., 2 -> a/b/c, 3 -> d,e,f etc).
    /// </summary>
    public class CharacterKeyProcessor : KeyProcessor
    {
        readonly string Characters;

        public CharacterKeyProcessor(char label, string characters) : base(label)
        {
            if (string.IsNullOrEmpty(characters))
                throw new ArgumentException("Character mapping cannot be empty.", nameof(characters));
            Characters = characters;
        }

        protected override void Transform(StringBuilder input, Token token)
        {
            input.Append(Characters[(token.Count - 1) % Characters.Length]);
        }
    }

    /// <summary>
    /// Processes backspace key by removing the last character.
    /// </summary>
    public class BackKeyProcessor : KeyProcessor
    {
        public BackKeyProcessor(char label) : base(label) { }

        protected override void Transform(StringBuilder input, Token token)
        {
            if (input.Length == 0) return;
            input.Remove(input.Length - 1, 1);
        }
    }
}
