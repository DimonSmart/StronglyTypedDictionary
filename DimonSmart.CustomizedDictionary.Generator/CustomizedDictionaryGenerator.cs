using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using static DimonSmart.CustomizedDictionary.Generator.CustomizedDictionaryTemplate;

namespace DimonSmart.CustomizedDictionary.Generator
{
    [Generator]
    public class CustomizedDictionaryGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // Initialization logic can go here
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var myFiles = context
                .AdditionalFiles
                .Where(at => at.Path.EndsWith(".dictionary"));

            // if (!Debugger.IsAttached)
            //    Debugger.Launch();

            foreach (var file in myFiles)
            {
                var dictionaryDescriptorSource = file.GetText(context.CancellationToken)?.ToString();
                var customizedDictionarySpecification = new CustomizedDictionarySpecification(dictionaryDescriptorSource);
                var generatedClass = GetGeneratedClass(DictionaryTemplate, customizedDictionarySpecification);
                var sourceText = SourceText.From(generatedClass, Encoding.UTF8);
                context.AddSource($"{Path.GetFileNameWithoutExtension(file.Path)}_g.cs", sourceText);
            }
        }

        private static string GetGeneratedClass(string template, CustomizedDictionarySpecification customizedDictionarySpecification)
        {
            var src = template;

            src = src.Replace("@@NameSpace@@", customizedDictionarySpecification.Namespace);
            src = src.Replace("@@DictionaryName@@", customizedDictionarySpecification.DictionaryName);
            src = src.Replace("@@KeyType@@", customizedDictionarySpecification.KeyType);
            src = src.Replace("@@KeyName@@", customizedDictionarySpecification.KeyName);
            src = src.Replace("@@KeyNameCapitalFirstLetter@@", CapitalizeFirstLetter(customizedDictionarySpecification.KeyName));
            src = src.Replace("@@KeyNamePlural@@", customizedDictionarySpecification.KeyNamePlural);
            src = src.Replace("@@KeyNamePluralCapitalFirstLetter@@", CapitalizeFirstLetter(customizedDictionarySpecification.KeyNamePlural));
            src = src.Replace("@@ValueType@@", customizedDictionarySpecification.ValueType);
            src = src.Replace("@@ValueName@@", customizedDictionarySpecification.ValueName);
            src = src.Replace("@@ValueNameCapitalFirstLetter@@", CapitalizeFirstLetter(customizedDictionarySpecification.ValueName));
            src = src.Replace("@@ValueNamePlural@@", customizedDictionarySpecification.ValueNamePlural);
            src = src.Replace("@@ValueNamePluralCapitalFirstLetter@@", CapitalizeFirstLetter(customizedDictionarySpecification.ValueNamePlural));

            return src;
        }
        public static string CapitalizeFirstLetter(string word) =>
            string.IsNullOrEmpty(word) ? word : char.ToUpper(word[0]) + word.Substring(1);

    }
}