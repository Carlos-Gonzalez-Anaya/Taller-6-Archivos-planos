using Core;

var basePath = "C:\\Users\\Carlos Gonzalez\\Documents\\cursos\\Estructura de Datos\\PlaneFiles\\Taller#6\\Data\\";
var personsPath = basePath + "Persons.txt";
var usersPath = basePath + "Users.txt";
var logPath = basePath + "log.txt";

// LOGIN
var auth = new AuthService();
var currentUser = auth.Login(usersPath);

if (currentUser == null)
{
    Console.WriteLine("Access denied. Press any key to exit.");
    Console.ReadKey();
    return;
}

using var logger = new LogWriter(logPath);
logger.WriteLog("INFO", currentUser, "LOGIN - Session started");

var service = new PersonService();
var persons = service.Read(personsPath);
logger.WriteLog("INFO", currentUser, "LOAD - Persons file loaded");

string option;
do
{
    option = Menu();

    switch (option)
    {
        case "1":
            service.ShowAll(persons);
            logger.WriteLog("INFO", currentUser, "SHOW - Content listed");
            break;
        case "2":
            service.AddPerson(persons);
            logger.WriteLog("INFO", currentUser, "ADD - Add person executed");
            break;
        case "3":
            service.EditPerson(persons);
            logger.WriteLog("INFO", currentUser, "EDIT - Edit person executed");
            break;
        case "4":
            service.DeletePerson(persons);
            logger.WriteLog("INFO", currentUser, "DELETE - Delete person executed");
            break;
        case "5":
            service.Write(personsPath, persons);
            Console.WriteLine("Changes saved.");
            logger.WriteLog("INFO", currentUser, "SAVE - Changes saved to Persons.txt");
            break;
        case "6":
            service.ShowReport(persons);
            logger.WriteLog("INFO", currentUser, "REPORT - City report viewed");
            break;
        case "0":
            Console.WriteLine("Goodbye!");
            logger.WriteLog("INFO", currentUser, "LOGOUT - Session ended");
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