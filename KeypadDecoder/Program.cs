class Program
{
    static void Main(string[] args)
    {
        var inputs = new List<string>{
            "436**22243#",
            "33#",
            "227*#",
            "4433555 555666#",
            "8 88777444666*664#",
            "12"
        };
        foreach (var input in inputs)
        {
            Console.WriteLine(input);
            var decodedStr = KeypadDecoder.Solution.OldPhonePad(input);
            Console.WriteLine($">> {decodedStr}");
        }
    }
}