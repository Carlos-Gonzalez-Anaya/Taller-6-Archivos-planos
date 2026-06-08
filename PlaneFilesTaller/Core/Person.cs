namespace Core;

public class Person
{
    // ID: 5 caracteres
    public int Id { get; set; }
    // FirstName: 20 caracteres
    public string FirstName { get; set; } = null!;
    // LastName: 20 caracteres
    public string LastName { get; set; } = null!;
    // Phone: 15 caracteres
    public string Phone { get; set; } = null!;
    // City: 20 caracteres
    public string City { get; set; } = null!;
    // Balance: 15 caracteres
    public decimal Balance { get; set; }

    public override string ToString()
    {
        return $"{Id.ToString().PadLeft(5, '0')}" +
               $"{FirstName.PadRight(20)}" +
               $"{LastName.PadRight(20)}" +
               $"{Phone.PadRight(15)}" +
               $"{City.PadRight(20)}" +
               $"{Balance.ToString("F2").PadLeft(15)}";
    }

    public static Person Parse(string line)
    {
        return new Person
        {
            Id = int.Parse(line.Substring(0, 5).Trim()),
            FirstName = line.Substring(5, 20).Trim(),
            LastName = line.Substring(25, 20).Trim(),
            Phone = line.Substring(45, 15).Trim(),
            City = line.Substring(60, 20).Trim(),
            Balance = decimal.Parse(line.Substring(80, 15).Trim(),
                        System.Globalization.CultureInfo.InvariantCulture)
        };
    }
}