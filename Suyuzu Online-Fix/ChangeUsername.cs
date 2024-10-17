using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Suyuzu_Online_Fix
{
    class ChangeUsername
    {
        public static string Username { get; private set; }
        public static string Token { get; private set; }

        public static void UsernamePrompt()
        {
            // Username Prompt
            var username = AnsiConsole.Prompt(
                new TextPrompt<string>("[white]Please enter a username:[/]")
                    .PromptStyle("gold1")
                    .Validate(name =>
                    {
                        if (Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
                            return ValidationResult.Success();
                        else
                            return ValidationResult.Error("[red]Username can only contain letters and numbers![/]");
                    }));

            // Set the username
            Username = username;

            // Generate the token
            Token = Guid.NewGuid().ToString();

            AnsiConsole.WriteLine("");
            AnsiConsole.MarkupLine($"[white]Username set to:[/] [gold1]{Username}[/]");
            AnsiConsole.MarkupLine($"[white]Generated token:[/] [gold1]{Token}[/]");
            AnsiConsole.WriteLine("");
            AnsiConsole.MarkupLine($"[white]Press anything to return...[/]");

            Console.ReadKey();
            Console.Clear();
            Program.ShowMenu();
        }
    }
}