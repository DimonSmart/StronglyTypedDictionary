namespace DimonSmart.CustomizedDictionary.Generator;

public static class CustomizedDictionaryTemplate
{
    public static string DictionaryTemplate = @"
namespace @@NameSpace
{
    public class @@DictionaryName
    {
        private Dictionary<@@KeyType, @@ValueType> _items = new Dictionary<@@KeyType, @@ValueType>();

        public @@ValueType this[@@KeyType @@KeyName]
        {
            get => _items.TryGetValue(@@KeyName, out var value) ? value : default;
            set => _items[@@KeyName] = value;
        }

        public void Add(@@KeyType @@KeyName, @@ValueType @@ValueName)
        {
            if (!_items.ContainsKey(@@KeyName))
            {
                _items.Add(@@KeyName, @@ValueName);
            }
            else
            {
                throw new ArgumentException($""An item with the same @@KeyName already exists."");
            }
        }

        public bool ContainsKey(@@KeyType @@KeyName)
        {
            return _items.ContainsKey(@@KeyName);
        }

        public bool Remove(@@KeyType @@KeyName)
        {
            return _items.Remove(@@KeyName);
        }

        public int Count => _items.Count;

        public IEnumerable<@@KeyType> @@KeyNamePlural => _items.Keys;

        public IEnumerable<@@ValueType> @@ValueNamePlural => _items.Values;

        public bool TryGetValue(@@KeyType @@KeyName, out @@ValueType @@ValueName)
        {
            return _items.TryGetValue(@@KeyName, out @@ValueName);
        }
    }
}";

}