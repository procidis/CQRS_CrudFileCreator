using CrudFileCreator;

string? mainPath;
do
{
    Console.WriteLine("Enter xxx.Application Library Path");
    mainPath = Console.ReadLine()?.Trim();
} while (string.IsNullOrWhiteSpace(mainPath) || !Directory.Exists(mainPath));

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

var getQueryPath = Path.Combine(queriesPath, getPrefix);
var createCommandPath = Path.Combine(commandsPath, createPrefix);
var deleteCommandPath = Path.Combine(commandsPath, deletePrefix);
var updateCommandPath = Path.Combine(commandsPath, updatePrefix);

if (!Directory.Exists(createCommandPath)) Directory.CreateDirectory(createCommandPath);
if (!Directory.Exists(deleteCommandPath)) Directory.CreateDirectory(deleteCommandPath);
if (!Directory.Exists(updateCommandPath)) Directory.CreateDirectory(updateCommandPath);

var queryContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "query_template.txt"));
var queryResultContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "query_result_template.txt"));
var queryValidatorContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "query_validator_template.txt"));
var queryHandlerContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "query_handler_template.txt"));
var commandContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "command_template.txt"));
var commandResultContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "command_result_template.txt"));
var commandValidatorContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "command_validator_template.txt"));
var commandHandlerContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "command_handler_template.txt"));

string prefix;
switch (mode)
{
    case CreationMode.All:
        prefix = getPrefix;
        CreateFile(getQueryPath, $"{prefix}Query", commandContent);
        CreateFile(getQueryPath, $"{prefix}QueryHandler", commandHandlerContent);
        CreateFile(getQueryPath, $"{prefix}QueryValidator", commandValidatorContent);
        CreateFile(getQueryPath, $"{prefix}QueryResult", commandResultContent);

        prefix = createPrefix;
        CreateFile(createCommandPath, $"{prefix}Command", queryContent);
        CreateFile(createCommandPath, $"{prefix}CommandHandler", queryHandlerContent);
        CreateFile(createCommandPath, $"{prefix}CommandValidator", queryValidatorContent);
        CreateFile(createCommandPath, $"{prefix}CommandResult", queryResultContent);

        prefix = updatePrefix;
        CreateFile(updateCommandPath, $"{prefix}Command", commandContent);
        CreateFile(updateCommandPath, $"{prefix}CommandHandler", commandHandlerContent);
        CreateFile(updateCommandPath, $"{prefix}CommandValidator", commandValidatorContent);
        CreateFile(updateCommandPath, $"{prefix}CommandResult", commandResultContent);

        prefix = deletePrefix;
        CreateFile(deleteCommandPath, $"{prefix}Command", commandContent);
        CreateFile(deleteCommandPath, $"{prefix}CommandHandler", commandHandlerContent);
        CreateFile(deleteCommandPath, $"{prefix}CommandValidator", commandValidatorContent);
        CreateFile(deleteCommandPath, $"{prefix}CommandResult", commandResultContent);
        break;
    case CreationMode.Create:
        prefix = createPrefix;
        CreateFile(createCommandPath, $"{prefix}Command", queryContent);
        CreateFile(createCommandPath, $"{prefix}CommandHandler", queryHandlerContent);
        CreateFile(createCommandPath, $"{prefix}CommandValidator", queryValidatorContent);
        CreateFile(createCommandPath, $"{prefix}CommandResult", queryResultContent);
        break;
    case CreationMode.Get:
        prefix = getPrefix;
        CreateFile(getQueryPath, $"{prefix}Query", commandContent);
        CreateFile(getQueryPath, $"{prefix}QueryHandler", commandHandlerContent);
        CreateFile(getQueryPath, $"{prefix}QueryValidator", commandValidatorContent);
        CreateFile(getQueryPath, $"{prefix}QueryResult", commandResultContent);
        break;
    case CreationMode.Update:
        prefix = updatePrefix;
        CreateFile(updateCommandPath, $"{prefix}Command", commandContent);
        CreateFile(updateCommandPath, $"{prefix}CommandHandler", commandHandlerContent);
        CreateFile(updateCommandPath, $"{prefix}CommandValidator", commandValidatorContent);
        CreateFile(updateCommandPath, $"{prefix}CommandResult", commandResultContent);
        break;
    case CreationMode.Delete:
        prefix = deletePrefix;
        CreateFile(deleteCommandPath, $"{prefix}Command", commandContent);
        CreateFile(deleteCommandPath, $"{prefix}CommandHandler", commandHandlerContent);
        CreateFile(deleteCommandPath, $"{prefix}CommandValidator", commandValidatorContent);
        CreateFile(deleteCommandPath, $"{prefix}CommandResult", commandResultContent);
        break;
    default:
        throw new ArgumentOutOfRangeException();
}

void CreateFile(string path, string fileName, string template)
{
    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    var content = template.Replace("xxx", fileName);
    File.WriteAllText(Path.Combine(path, $"{fileName}.cs"), content);
}