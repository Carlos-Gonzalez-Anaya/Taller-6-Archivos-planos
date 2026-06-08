namespace Core;

public class AuthService
{
    public string? Login(string usersPath)
    {
        int attempts = 0;

        while (attempts < 3)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Password: ");
            string password = ReadPassword();

            var (found, active) = ValidateUser(usersPath, username, password);

            if (found && active)
            {
                Console.WriteLine("\nLogin successful. Welcome!\n");
                return username;
            }
            else if (found && !active)
            {
                Console.WriteLine("\nYour account is blocked. Contact an administrator.\n");
                return null;
            }
            else
            {
                attempts++;
                int remaining = 3 - attempts;
                if (remaining > 0)
                    Console.WriteLine($"\nInvalid credentials. {remaining} attempt(s) remaining.\n");
                else
                {
                    Console.WriteLine("\nToo many failed attempts. Your account has been blocked.\n");
                    BlockUser(usersPath, username);
                    return null;
                }
            }
        }
        return null;
    }

    private (bool found, bool active) ValidateUser(string usersPath, string username, string password)
    {
        if (!File.Exists(usersPath)) return (false, false);

        foreach (var line in File.ReadAllLines(usersPath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',');
            if (parts.Length < 3) continue;

            string u = parts[0].Trim();
            string p = parts[1].Trim();
            bool active = parts[2].Trim().ToLower() == "true";

            if (u == username && p == password)
                return (true, active);
        }
        return (false, false);
    }

    private void BlockUser(string usersPath, string username)
    {
        if (!File.Exists(usersPath)) return;

        var lines = File.ReadAllLines(usersPath);
        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            if (parts.Length >= 3 && parts[0].Trim() == username)
                lines[i] = $"{parts[0].Trim()},{parts[1].Trim()},false";
        }
        File.WriteAllLines(usersPath, lines);
    }

    private string ReadPassword()
    {
        string pass = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(intercept: true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                pass += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
            {
                pass = pass[..^1];
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return pass;
    }
}