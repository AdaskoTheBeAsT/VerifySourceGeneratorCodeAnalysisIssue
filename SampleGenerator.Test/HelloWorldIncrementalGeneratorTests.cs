using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SampleGenerator.Test;

public class HelloWorldIncrementalGeneratorTests
{
    [Fact]
    public Task VerifyGeneratedSourceAsync()
    {
        // Arrange
        var source = @"// Your test code here";

        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var compilation = CSharpCompilation.Create(
            "Tests",
            new[] { syntaxTree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var generator = new HelloWorldIncrementalGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator); // Note the use of GeneratorDriver here

        // Act
        _ = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);

        // Assert
        var generatedSources = outputCompilation.SyntaxTrees;

        return Verifier.Verify(generatedSources);
    }
}
