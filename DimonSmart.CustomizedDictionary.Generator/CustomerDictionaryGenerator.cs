using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System;

namespace DimonSmart.CustomizedDictionary.Generator
{
    [Generator]
    public class CustomerDictionaryGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // Initialization logic can go here
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var myFiles = context
                .AdditionalFiles
                .Where(at => at.Path.EndsWith(".csv"));
            // if (!Debugger.IsAttached)
            //    Debugger.Launch();

            foreach (var file in myFiles)
            {
                var dictionaryDescriptorSource = file.GetText(context.CancellationToken)?.ToString();
                var customizedDictionarySpecification = new CustomizedDictionarySpecification(dictionaryDescriptorSource);
                var generatedClass = GetGeneratedClass(customizedDictionarySpecification);
                var sourceText = SourceText.From(generatedClass, Encoding.UTF8);
                context.AddSource($"{Path.GetFileNameWithoutExtension(file.Path)}generated.cs", sourceText);
            }
        }

        private string GetGeneratedClass(CustomizedDictionarySpecification customizedDictionarySpecification)
        {
            var src = DictionaryTemplate;
            src = src.Replace("@@NameSpace", customizedDictionarySpecification.Namespace);

            return src;
        }

        private const string DictionaryTemplate =
            """
            namespace @@NameSpace;
            public class CustomerDictionary
            {
                public void Print()
                {
                    Console.Writeline("7");
                }
            
            }
            
            
            
            """;
    }
}