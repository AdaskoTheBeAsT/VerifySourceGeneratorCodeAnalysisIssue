using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SampleGenerator;

#pragma warning disable RS1038
[Generator]
public class HelloWorldIncrementalGenerator
    : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register the source generator
        context.RegisterSourceOutput(context.CompilationProvider, (ctx, compilation) =>
        {
            var source = @"
            namespace HelloWorldNamespace 
            {
                public static class HelloWorld 
                {
                    public static void SayHello() 
                    {
                        Console.WriteLine(""Hello, World!"");
                    }
                }
            }";

            ctx.AddSource(nameof(HelloWorldIncrementalGenerator), SourceText.From(source, Encoding.UTF8));
        });
    }
}
#pragma warning restore RS1038
