// ReSharper disable once RedundantUsingDirective
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using static DimonSmart.StronglyTypedDictionary.Generator.StronglyTypedDictionaryTemplate;

namespace DimonSmart.StronglyTypedDictionary.Generator
{
    [Generator]
    public class StronglyTypedDictionaryGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // Initialization logic can go here
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var myFiles = context
                .AdditionalFiles
                .Where(at => at.Path.EndsWith(".StronglyTypedDictionary"));

            // if (!Debugger.IsAttached)
            //    Debugger.Launch();

            foreach (var file in myFiles)
            {
                var dictionaryDescriptorSource = file.GetText(context.CancellationToken)?.ToString();
                var stronglyTypedDictionarySpecification = new StronglyTypedDictionarySpecification(dictionaryDescriptorSource);
                var generatedClass = GetGeneratedClass(DictionaryTemplate, stronglyTypedDictionarySpecification);
                var sourceText = SourceText.From(generatedClass, Encoding.UTF8);
                context.AddSource($"{Path.GetFileNameWithoutExtension(file.Path)}_g.cs", sourceText);
            }
        }

        private static string GetGeneratedClass(string template, StronglyTypedDictionarySpecification StronglyTypedDictionarySpecification)
        {
            var src = template;

            src = src.Replace("@@NameSpace@@", StronglyTypedDictionarySpecification.Namespace);
            src = src.Replace("@@DictionaryName@@", StronglyTypedDictionarySpecification.DictionaryName);
            src = src.Replace("@@KeyType@@", StronglyTypedDictionarySpecification.KeyType);
            src = src.Replace("@@KeyName@@", StronglyTypedDictionarySpecification.KeyName);
            src = src.Replace("@@KeyNameCapitalFirstLetter@@", CapitalizeFirstLetter(StronglyTypedDictionarySpecification.KeyName));
            src = src.Replace("@@KeyNamePlural@@", StronglyTypedDictionarySpecification.KeyNamePlural);
            src = src.Replace("@@KeyNamePluralCapitalFirstLetter@@", CapitalizeFirstLetter(StronglyTypedDictionarySpecification.KeyNamePlural));
            src = src.Replace("@@ValueType@@", StronglyTypedDictionarySpecification.ValueType);
            src = src.Replace("@@ValueName@@", StronglyTypedDictionarySpecification.ValueName);
            src = src.Replace("@@ValueNameCapitalFirstLetter@@", CapitalizeFirstLetter(StronglyTypedDictionarySpecification.ValueName));
            src = src.Replace("@@ValueNamePlural@@", StronglyTypedDictionarySpecification.ValueNamePlural);
            src = src.Replace("@@ValueNamePluralCapitalFirstLetter@@", CapitalizeFirstLetter(StronglyTypedDictionarySpecification.ValueNamePlural));

            return src;
        }
        public static string CapitalizeFirstLetter(string word) =>
            string.IsNullOrEmpty(word) ? word : char.ToUpper(word[0]) + word.Substring(1);
    }
}