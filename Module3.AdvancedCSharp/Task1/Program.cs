using Spectre.Console;
using Task1;

WriteDivider("Input");

var path = AnsiConsole.Prompt(
    new TextPrompt<string>("[grey][[path]][/] Provide a [green]path[/] to a specific folder (e.g. C:/temp): ")
    .PromptStyle("green")
    .ValidationErrorMessage("[red]That's not a valid age[/]")
    .Validate(str =>
    {
        bool valid = Directory.Exists(str);
        return valid switch
        {
            false => ValidationResult.Error("[red]Please, provide a valid and existing path to a folder (e.g. C:/temp)[/]"),
            _ => ValidationResult.Success(),
        };
    }));

WriteDivider("Result");

var fileSystemVisitor = new FileSystemVisitor(d => d.Name.Length == 5, f => f.Name.Length < 20);
var folder = new Folder(path);
var result = fileSystemVisitor.Traverse(folder);

foreach (var item in result)
{
    Console.WriteLine(item);
}

static void WriteDivider(string text)
{
    AnsiConsole.WriteLine();
    AnsiConsole.Write(new Rule($"[yellow]{text}[/]").RuleStyle("grey").LeftAligned());
}