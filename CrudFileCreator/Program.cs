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

var createCommandPath = Path.Combine(commandsPath, createPrefix);
var deleteCommandPath = Path.Combine(commandsPath, deletePrefix);
var updateCommandPath = Path.Combine(commandsPath, updatePrefix);

if (!Directory.Exists(createCommandPath)) Directory.CreateDirectory(createCommandPath);
if (!Directory.Exists(deleteCommandPath)) Directory.CreateDirectory(deleteCommandPath);
if (!Directory.Exists(updateCommandPath)) Directory.CreateDirectory(updateCommandPath);

var queryContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "query_template.txt"));
var commandContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "command_template.txt"));
 
switch (mode)
{
    case CreationMode.All:
        CreateFile(queriesPath, getPrefix, queryContent);
        CreateFile(commandsPath, createPrefix, commandContent);
        CreateFile(commandsPath, deletePrefix, commandContent);
        CreateFile(commandsPath, updatePrefix, commandContent);
        break;
    case CreationMode.Create:
        CreateFile(commandsPath, createPrefix, commandContent);
        break;
    case CreationMode.Get:
        CreateFile(queriesPath, getPrefix, queryContent);
        break;
    case CreationMode.Update:
        CreateFile(commandsPath, updatePrefix, commandContent);
        break;
    case CreationMode.Delete:
        CreateFile(commandsPath, deletePrefix, commandContent);
        break;
    default:
        throw new ArgumentOutOfRangeException();
}

void CreateFile(string path, string prefix, string template)
{
    path = Path.Combine(path, prefix);
    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    var content = template.Replace("xxx", prefix);
    File.WriteAllText(Path.Combine(path, $"{prefix}.cs"), content);
}