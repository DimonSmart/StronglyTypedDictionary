namespace DimonSmart.CustomizedDictionary.Consumer;

public static class Program
{
    public static void Main()
    {
        var d = new ErrorNumberToMessageMapping();
        d.Add(404, "Something not found");
        if (!d.ContainsErrorCode(42))
            Console.WriteLine("May be this is not an error at all");

        if (d.TryGetMessage(404, out var errorMessage))
            Console.WriteLine($"404: {errorMessage}");

        var messageFor43 = d.GetMessageOrDefault(43, "WOW");
    }
}