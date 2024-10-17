using Spectre.Console;

namespace Suyuzu_Online_Fix
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Title = "Suyuzu v1.1.0";
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
                    .AddChoiceGroup("[BlueViolet]Emulators:[/]\n", "[White]Yuzu[/]", "[White]Suyu[/]", "[White]Sudachi[/]\n\n")
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
                case "[White]Sudachi[/]\n\n":
                    Fixes.SudachiFix.ApplySudachiFix();
                    break;
                case "[White]Change Username[/]":
                    ChangeUsername.UsernamePrompt();
                    break;
            }
        }
    }
}