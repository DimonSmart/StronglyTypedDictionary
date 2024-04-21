namespace DimonSmart.StronglyTypedDictionary.Generator;

public static class StronglyTypedDictionaryTemplate
{
    public static string DictionaryTemplate = """
          namespace @@NameSpace@@
          {
              public interface I@@DictionaryName@@<TK, TV>
              {
                  @@ValueType@@ this[TK @@KeyName@@] { get; set; }
                  void Add(TK @@KeyName@@, TV @@ValueName@@);
                  bool Contains@@KeyNameCapitalFirstLetter@@(TK @@KeyName@@);
                  bool Remove(TK @@KeyName@@);
                  int Count { get; }
                  IEnumerable<TK> @@KeyNamePluralCapitalFirstLetter@@ { get; }
                  IEnumerable<TV> @@ValueNamePluralCapitalFirstLetter@@ { get; }
                  bool TryGet@@ValueNameCapitalFirstLetter@@(TK @@KeyName@@, out TV @@ValueName@@);
                  TV Get@@ValueNameCapitalFirstLetter@@OrDefault(TK @@KeyName@@, TV default@@ValueName@@);
              }
          
              public class @@DictionaryName@@ : I@@DictionaryName@@<@@KeyType@@, @@ValueType@@>
              {
                  private readonly Dictionary<@@KeyType@@, @@ValueType@@> _items = new();
          
                  public @@ValueType@@ this[@@KeyType@@ @@KeyName@@]
                  {
                      get => _items[@@KeyName@@];
                      set => _items[@@KeyName@@] = value;
                  }
          
                  public void Add(@@KeyType@@ @@KeyName@@, @@ValueType@@ @@ValueName@@)
                  {
                      if (!_items.ContainsKey(@@KeyName@@))
                      {
                          _items.Add(@@KeyName@@, @@ValueName@@);
                      }
                      else
                      {
                          throw new ArgumentException($"An item with the same @@KeyName@@ already exists.");
                      }
                  }
          
                  public bool Contains@@KeyNameCapitalFirstLetter@@(@@KeyType@@ @@KeyName@@)
                  {
                      return _items.ContainsKey(@@KeyName@@);
                  }
          
                  public bool Remove(@@KeyType@@ @@KeyName@@)
                  {
                      return _items.Remove(@@KeyName@@);
                  }
          
                  public int Count => _items.Count;
          
                  public IEnumerable<@@KeyType@@> @@KeyNamePluralCapitalFirstLetter@@ => _items.Keys;
          
                  public IEnumerable<@@ValueType@@> @@ValueNamePluralCapitalFirstLetter@@ => _items.Values;
          
                  public bool TryGet@@ValueNameCapitalFirstLetter@@(@@KeyType@@ @@KeyName@@, out @@ValueType@@ @@ValueName@@)
                  {
                      return _items.TryGetValue(@@KeyName@@, out @@ValueName@@);
                  }
          
                  public @@ValueType@@ Get@@ValueNameCapitalFirstLetter@@OrDefault(@@KeyType@@ @@KeyName@@, @@ValueType@@ default@@ValueName@@)
                  {
                      return _items.TryGetValue(@@KeyName@@, out var value) ? value : default@@ValueName@@;
                  }
              }
          }
          """;

}