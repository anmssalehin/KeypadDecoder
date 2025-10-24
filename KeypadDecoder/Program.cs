class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter \'q\' to quit");

        while (true) {
            Console.Write("Enter Sequence: ");
            var input = Console.ReadLine();
            if (input == null) continue;

            input = input.Trim();
            if (input == "q") break;
            if (input.Length == 0) continue;
            if (input.Length > 0 && input[input.Length - 1] != '#') input += "#";

            var decodedText = KeypadDecoder.Solution.OldPhonePad(input);
            Console.WriteLine($"Decoded text: {decodedText}");
        }
    }
}