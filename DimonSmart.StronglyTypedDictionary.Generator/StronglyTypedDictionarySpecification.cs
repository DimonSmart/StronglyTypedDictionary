using System;
using System.Text.RegularExpressions;

namespace DimonSmart.StronglyTypedDictionary.Generator;

public class StronglyTypedDictionarySpecification
{
    public string Namespace { get; private set; }
    public string DictionaryName { get; private set; }
    public string KeyType { get; private set; }
    public string KeyName { get; private set; }
    public string KeyNamePlural { get; private set; }
    public string ValueType { get; private set; }
    public string ValueName { get; private set; }
    public string ValueNamePlural { get; private set; }

    // Constructor that parses the input string with pluralization
    public StronglyTypedDictionarySpecification(string input)
    {
        var regex = new Regex(@"^(?<Namespace>.+?)\.(?<DictionaryName>\w+)<(?<KeyType>\w+)\s+(?<KeyName>\w+)(\((?<KeyNamePlural>\w+)\))?,\s*(?<ValueType>\w+)\s+(?<ValueName>\w+)(\((?<ValueNamePlural>\w+)\))?>$");
        var match = regex.Match(input);

        if (!match.Success)
        {
            throw new ArgumentException("Input string is not in the correct format.", nameof(input));
        }

        Namespace = match.Groups["Namespace"].Value;
        DictionaryName = match.Groups["DictionaryName"].Value;
        KeyType = match.Groups["KeyType"].Value;
        KeyName = match.Groups["KeyName"].Value;
        ValueType = match.Groups["ValueType"].Value;
        ValueName = match.Groups["ValueName"].Value;

        // Handle plural forms if specified; otherwise, assume simple pluralization by appending "s"
        KeyNamePlural = match.Groups["KeyNamePlural"].Success ? match.Groups["KeyNamePlural"].Value : GetPluralForm(KeyName);
        ValueNamePlural = match.Groups["ValueNamePlural"].Success ? match.Groups["ValueNamePlural"].Value : GetPluralForm(ValueName);
    }

    private static string GetPluralForm(string singular) =>
        singular.EndsWith("s", StringComparison.OrdinalIgnoreCase) ? singular + "es" : singular + "s";
}