using System.Reflection;
using CrudFileCreator;

string? mainPath;
do
{
    Console.WriteLine("Enter xxx.Application Library Path");
    mainPath = Console.ReadLine()?.Trim();
} while (string.IsNullOrWhiteSpace(mainPath) || !Directory.Exists(mainPath));

while (true)
{
    Run();
}

void Run()
{
    string? typeName;
    do
    {
        Console.WriteLine("Enter Type Name");
        typeName = Console.ReadLine()?.Trim();
    } while (string.IsNullOrWhiteSpace(typeName));

    CreationMode mode;
    string? creationMode;
    do
    {
        Console.WriteLine("Enter Creation Mode");
        Console.WriteLine("1 - All");
        Console.WriteLine("2 - Create");
        Console.WriteLine("3 - Get");
        Console.WriteLine("4 - Update");
        Console.WriteLine("5 - Delete");
        creationMode = Console.ReadLine()?.Trim();
    } while (!Enum.TryParse<CreationMode>(creationMode, out mode));


    var queriesPath = Path.Combine(mainPath, "Queries");
    var commandsPath = Path.Combine(mainPath, "Commands");

    var getPrefix = $"Get{typeName}";
    var createPrefix = $"Create{typeName}";
    var deletePrefix = $"Delete{typeName}";
    var updatePrefix = $"Update{typeName}";
    var subFolder = $"{typeName}s";

    var getQueryPath = Path.Combine(queriesPath, subFolder, getPrefix);
    var createCommandPath = Path.Combine(commandsPath, subFolder, createPrefix);
    var deleteCommandPath = Path.Combine(commandsPath, subFolder, deletePrefix);
    var updateCommandPath = Path.Combine(commandsPath, subFolder, updatePrefix);

    if (!Directory.Exists(createCommandPath)) Directory.CreateDirectory(createCommandPath);
    if (!Directory.Exists(deleteCommandPath)) Directory.CreateDirectory(deleteCommandPath);
    if (!Directory.Exists(updateCommandPath)) Directory.CreateDirectory(updateCommandPath);

    var assemblyLocation = new FileInfo(typeof(CreationMode).Assembly.Location).Directory!.FullName;
    var queryContent = File.ReadAllText(Path.Combine(assemblyLocation, "Templates", "query_template.txt"));
    var queryResultContent = File.ReadAllText(Path.Combine(assemblyLocation, "Templates", "query_result_template.txt"));
    var queryValidatorContent = File.ReadAllText(Path.Combine(assemblyLocation, "Templates", "query_validator_template.txt"));
    var queryHandlerContent = File.ReadAllText(Path.Combine(assemblyLocation, "Templates", "query_handler_template.txt"));
    var commandContent = File.ReadAllText(Path.Combine(assemblyLocation, "Templates", "command_template.txt"));
    var commandResultContent = File.ReadAllText(Path.Combine(assemblyLocation, "Templates", "command_result_template.txt"));
    var commandValidatorContent = File.ReadAllText(Path.Combine(assemblyLocation, "Templates", "command_validator_template.txt"));
    var commandHandlerContent = File.ReadAllText(Path.Combine(assemblyLocation, "Templates", "command_handler_template.txt"));

    string prefix;
    switch (mode)
    {
        case CreationMode.All:
            prefix = getPrefix;
            CreateFile(getQueryPath, $"{prefix}Query", queryContent, prefix);
            CreateFile(getQueryPath, $"{prefix}QueryHandler", queryHandlerContent, prefix);
            CreateFile(getQueryPath, $"{prefix}QueryValidator", queryValidatorContent, prefix);
            CreateFile(getQueryPath, $"{prefix}QueryResult", queryResultContent, prefix);

            prefix = createPrefix;
            CreateFile(createCommandPath, $"{prefix}Command", commandContent, prefix);
            CreateFile(createCommandPath, $"{prefix}CommandHandler", commandHandlerContent, prefix);
            CreateFile(createCommandPath, $"{prefix}CommandValidator", commandValidatorContent, prefix);
            CreateFile(createCommandPath, $"{prefix}CommandResult", commandResultContent, prefix);

            prefix = updatePrefix;
            CreateFile(updateCommandPath, $"{prefix}Command", commandContent, prefix);
            CreateFile(updateCommandPath, $"{prefix}CommandHandler", commandHandlerContent, prefix);
            CreateFile(updateCommandPath, $"{prefix}CommandValidator", commandValidatorContent, prefix);
            CreateFile(updateCommandPath, $"{prefix}CommandResult", commandResultContent, prefix);

            prefix = deletePrefix;
            CreateFile(deleteCommandPath, $"{prefix}Command", commandContent, prefix);
            CreateFile(deleteCommandPath, $"{prefix}CommandHandler", commandHandlerContent, prefix);
            CreateFile(deleteCommandPath, $"{prefix}CommandValidator", commandValidatorContent, prefix);
            CreateFile(deleteCommandPath, $"{prefix}CommandResult", commandResultContent, prefix);
            break;
        
        case CreationMode.Get:
            prefix = getPrefix;
            CreateFile(getQueryPath, $"{prefix}Query", queryContent, prefix);
            CreateFile(getQueryPath, $"{prefix}QueryHandler", queryHandlerContent, prefix);
            CreateFile(getQueryPath, $"{prefix}QueryValidator", queryValidatorContent, prefix);
            CreateFile(getQueryPath, $"{prefix}QueryResult", queryResultContent, prefix);
            break;
        
        case CreationMode.Create:
            prefix = createPrefix;
            CreateFile(createCommandPath, $"{prefix}Command", commandContent, prefix);
            CreateFile(createCommandPath, $"{prefix}CommandHandler", commandHandlerContent, prefix);
            CreateFile(createCommandPath, $"{prefix}CommandValidator", commandValidatorContent, prefix);
            CreateFile(createCommandPath, $"{prefix}CommandResult", commandResultContent, prefix);
            break;
        case CreationMode.Update:
            prefix = updatePrefix;
            CreateFile(updateCommandPath, $"{prefix}Command", commandContent, prefix);
            CreateFile(updateCommandPath, $"{prefix}CommandHandler", commandHandlerContent, prefix);
            CreateFile(updateCommandPath, $"{prefix}CommandValidator", commandValidatorContent, prefix);
            CreateFile(updateCommandPath, $"{prefix}CommandResult", commandResultContent, prefix);
            break;
        case CreationMode.Delete:
            prefix = deletePrefix;
            CreateFile(deleteCommandPath, $"{prefix}Command", commandContent, prefix);
            CreateFile(deleteCommandPath, $"{prefix}CommandHandler", commandHandlerContent, prefix);
            CreateFile(deleteCommandPath, $"{prefix}CommandValidator", commandValidatorContent, prefix);
            CreateFile(deleteCommandPath, $"{prefix}CommandResult", commandResultContent, prefix);
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

void CreateFile(string path, string fileName, string template, string? typeWithPrefix)
{
    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    var content = template.Replace("xxx", typeWithPrefix);
    path = Path.Combine(path, $"{fileName}.cs");
    File.WriteAllText(path, content);
    Console.WriteLine(path);
}