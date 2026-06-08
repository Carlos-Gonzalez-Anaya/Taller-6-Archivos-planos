namespace Core;

public class PersonService
{
    public void Write(string path, IEnumerable<Person> persons)
    {
        File.WriteAllLines(path, persons.Select(x => x.ToString()));
    }

    public List<Person> Read(string path)
    {
        if (!File.Exists(path)) return new List<Person>();
        var lines = File.ReadAllLines(path);
        return lines.Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(Person.Parse)
                    .ToList();
    }

    public void ShowAll(List<Person> persons)
    {
        if (persons.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        Console.WriteLine(new string('=', 60));
        foreach (var p in persons)
        {
            Console.WriteLine($"\n{p.Id,-5}   {p.FirstName} {p.LastName}");
            Console.WriteLine($"        Phone:   {p.Phone}");
            Console.WriteLine($"        City:    {p.City}");
            Console.WriteLine($"        Balance: {p.Balance,15:C2}");
        }
        Console.WriteLine();
    }

    public void AddPerson(List<Person> persons)
    {
        Console.WriteLine("\n--- Add Person ---");

        // ID
        Console.Write("ID: ");
        string idInput = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!int.TryParse(idInput, out int id) || id <= 0)
        {
            Console.WriteLine("ID must be a positive number.");
            return;
        }
        if (persons.Any(p => p.Id == id))
        {
            Console.WriteLine("That ID already exists.");
            return;
        }

        // First name
        Console.Write("First name: ");
        string firstName = Console.ReadLine()?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(firstName) || firstName.Length < 2)
        {
            Console.WriteLine("First name must have at least 2 characters.");
            return;
        }

        // Last name
        Console.Write("Last name: ");
        string lastName = Console.ReadLine()?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(lastName) || lastName.Length < 2)
        {
            Console.WriteLine("Last name must have at least 2 characters.");
            return;
        }

        // Phone
        Console.Write("Phone: ");
        string phone = Console.ReadLine()?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(phone) || phone.Length < 7)
        {
            Console.WriteLine("Phone must have at least 7 characters.");
            return;
        }

        // City
        Console.Write("City: ");
        string city = Console.ReadLine()?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(city))
        {
            Console.WriteLine("City cannot be empty.");
            return;
        }

        // Balance
        Console.Write("Balance: ");
        string balInput = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!decimal.TryParse(balInput, out decimal balance) || balance < 0)
        {
            Console.WriteLine("Balance must be a positive number.");
            return;
        }

        persons.Add(new Person
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Phone = phone,
            City = city,
            Balance = balance
        });

        Console.WriteLine($"\nPerson '{firstName} {lastName}' added. Remember to Save changes (option 5).");
    }

    public void EditPerson(List<Person> persons)
    {
        Console.WriteLine("\n--- Edit Person ---");

        Console.Write("Enter ID to edit: ");
        string idInput = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var person = persons.FirstOrDefault(p => p.Id == id);
        if (person == null)
        {
            Console.WriteLine("ID not found.");
            return;
        }

        Console.WriteLine($"\nEditing: {person.FirstName} {person.LastName}");
        Console.WriteLine("(Press ENTER to keep current value)\n");

        Console.Write($"First name [{person.FirstName}]: ");
        string firstName = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(firstName))
        {
            if (firstName.Length < 2) { Console.WriteLine("Invalid first name."); return; }
            person.FirstName = firstName;
        }

        Console.Write($"Last name [{person.LastName}]: ");
        string lastName = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(lastName))
        {
            if (lastName.Length < 2) { Console.WriteLine("Invalid last name."); return; }
            person.LastName = lastName;
        }

        Console.Write($"Phone [{person.Phone}]: ");
        string phone = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(phone))
        {
            if (phone.Length < 7) { Console.WriteLine("Invalid phone."); return; }
            person.Phone = phone;
        }

        Console.Write($"City [{person.City}]: ");
        string city = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(city))
            person.City = city;

        Console.Write($"Balance [{person.Balance:F2}]: ");
        string balInput = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(balInput))
        {
            if (!decimal.TryParse(balInput, out decimal balance) || balance < 0)
            {
                Console.WriteLine("Balance must be a positive number.");
                return;
            }
            person.Balance = balance;
        }

        Console.WriteLine("\nRecord updated. Remember to Save changes (option 5).");
    }
    public void DeletePerson(List<Person> persons)
    {
        Console.WriteLine("\n--- Delete Person ---");

        Console.Write("Enter ID to delete: ");
        string idInput = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var person = persons.FirstOrDefault(p => p.Id == id);
        if (person == null)
        {
            Console.WriteLine("ID not found.");
            return;
        }

        Console.WriteLine($"\nID:      {person.Id}");
        Console.WriteLine($"Name:    {person.FirstName} {person.LastName}");
        Console.WriteLine($"Phone:   {person.Phone}");
        Console.WriteLine($"City:    {person.City}");
        Console.WriteLine($"Balance: {person.Balance:C2}");

        Console.Write("\nAre you sure you want to delete this person? (y/n): ");
        string confirm = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

        if (confirm == "y")
        {
            persons.Remove(person);
            Console.WriteLine("Record deleted. Remember to Save changes (option 5).");
        }
        else
        {
            Console.WriteLine("Delete cancelled.");
        }
    }

    public void ShowReport(List<Person> persons)
    {
        if (persons.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        var grouped = persons
            .OrderBy(p => p.City)
            .GroupBy(p => p.City);

        decimal grandTotal = 0;

        Console.WriteLine();
        foreach (var group in grouped)
        {
            Console.WriteLine($"Ciudad: {group.Key}");
            Console.WriteLine($"{"ID",-5}  {"First Name",-20} {"Last Name",-20} {"Balance",15}");
            Console.WriteLine($"{new string('-', 5)}  {new string('-', 20)} {new string('-', 20)} {new string('-', 15)}");

            decimal cityTotal = 0;
            foreach (var p in group)
            {
                Console.WriteLine($"{p.Id,-5}  {p.FirstName,-20} {p.LastName,-20} {p.Balance,15:N2}");
                cityTotal += p.Balance;
            }

            Console.WriteLine(new string('=', 65));
            Console.WriteLine($"{"Total: " + group.Key,-47} {cityTotal,15:N2}");
            Console.WriteLine();
            grandTotal += cityTotal;
        }

        Console.WriteLine(new string('=', 65));
        Console.WriteLine($"{"Total General:",-47} {grandTotal,15:N2}");
        Console.WriteLine();
    }
}