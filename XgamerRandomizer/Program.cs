using System.Text.Json;

var path = $@"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}\Storage\flavors.json";
var fileInfo = new FileInfo(path);

if (!fileInfo.Exists)
    using (fileInfo.Create()) { }




var text = File.ReadAllText(path);
List<string> flavors = new();
try
{
    flavors = JsonSerializer.Deserialize<List<string>>(text) ?? new List<string>();
}
catch (JsonException){}

ConsoleKey input = ConsoleKey.Execute;
Console.WriteLine("Welcome!");
while (input != ConsoleKey.D4)
{
    Console.WriteLine("1. Get a random flavor.\n2. Add a flavor.\n3. Remove a flavor.\n4. Exit.");
    input = Console.ReadKey().Key;
    Console.WriteLine('\n');
    string? stringInput;
    switch (input)
    {
        case ConsoleKey.D1:
            if (flavors.Count == 0)
            {
                Console.WriteLine("There are no flavors yet!");
                break;
            }

            var randomIndex = new Random().Next(0, flavors.Count);
            Console.WriteLine(flavors[randomIndex]);
            input = ConsoleKey.D4;
            break;

        case ConsoleKey.D2:
            Console.Write("Input the name of the flavor you want to add: ");
            stringInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(stringInput))
            {
                Console.WriteLine("Stop trolling!");
            }
            else
            {
                flavors.Add(stringInput);
            }
            break;

        case ConsoleKey.D3:
            Console.WriteLine("Type in a flavor from the list:");
            foreach (var flavor in flavors)
                Console.WriteLine(flavor);

            stringInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(stringInput) || !flavors.Contains(stringInput))
            {
                Console.WriteLine("Stop trolling!");
            }
            else
            {
                flavors.Remove(stringInput);
            }
            break;

        case ConsoleKey.D4:
            break;

        default:
            input = ConsoleKey.D4;
            break;
    }
}

text = JsonSerializer.Serialize(flavors);
File.WriteAllText(path, text);
