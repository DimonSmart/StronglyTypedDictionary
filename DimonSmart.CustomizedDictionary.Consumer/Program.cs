namespace DimonSmart.CustomizedDictionary.Consumer;

public static class Program
{
    public static void Main()
    {
      var d = new IpMap();
      d.Add("localhost", 10);
    }
}