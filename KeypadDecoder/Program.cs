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
            if (input.Length > 0 && input[input.Length - 1] != '#') input += "#";
            Console.WriteLine($"Decoded text: {KeypadDecoder.Solution.OldPhonePad(input)}");
        }
    }
}