using System;

string option;

do
{
    option = Menu();

    switch (option)
    {
        case "1":
            Console.WriteLine(">> Show content (not implemented yet)");
            break;
        case "2":
            Console.WriteLine(">> Add person (not implemented yet)");
            break;
        case "3":
            Console.WriteLine(">> Edit person (not implemented yet)");
            break;
        case "4":
            Console.WriteLine(">> Delete person (not implemented yet)");
            break;
        case "5":
            Console.WriteLine(">> Save changes (not implemented yet)");
            break;
        case "6":
            Console.WriteLine(">> City report (not implemented yet)");
            break;
        case "0":
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }

} while (option != "0");


string Menu()
{
    Console.WriteLine(new string('=', 46));
    Console.WriteLine("1. Show content");
    Console.WriteLine("2. Add person");
    Console.WriteLine("3. Edit person");
    Console.WriteLine("4. Delete person");
    Console.WriteLine("5. Save changes");
    Console.WriteLine("6. City report");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
    return Console.ReadLine() ?? string.Empty;
}