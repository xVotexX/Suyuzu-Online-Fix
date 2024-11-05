using Spectre.Console;

namespace Suyuzu_Online_Fix
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Title = "Suyuzu v1.2.0";
            ShowMenu();
        }

        public static void ShowMenu() 
        {
            // Top Part
            AnsiConsole.Write(
                new FigletText("Suyuzu  Online-Fix")
                    .Centered()
                    .Color(Color.BlueViolet));

            AnsiConsole.WriteLine();
            var rule = new Rule("[BlueViolet]> Made by xVotex <[/]");
            rule.Style = Style.Parse("BlueViolet");
            AnsiConsole.Write(rule);

            // Choice Menu
            var emuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("")
                    .AddChoiceGroup("[BlueViolet]Emulators:[/]\n", "[White]Yuzu[/]", "[White]Suyu[/]", "[White]Sudachi[/]", "[White]Apply to all[/]\n\n")
                    .AddChoiceGroup("[BlueViolet]Settings:[/]\n", "[White]Change Username[/]")
                    .HighlightStyle(new Style(foreground: Color.BlueViolet)));

            switch (emuChoice)
            {
                case "[White]Yuzu[/]":
                    Fixes.YuzuFix.ApplyYuzuFix();
                    break;
                case "[White]Suyu[/]":
                    Fixes.SuyuFix.ApplySuyuFix();
                    break;
                case "[White]Sudachi[/]":
                    Fixes.SudachiFix.ApplySudachiFix();
                    break;
                case "[White]Apply to all[/]\n\n":
                    Fixes.Ata.ApplyToAll();
                    break;
                case "[White]Change Username[/]":
                    ChangeUsername.UsernamePrompt();
                    break;
            }
        }
    }
}