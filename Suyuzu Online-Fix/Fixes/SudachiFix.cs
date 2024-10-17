using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace Suyuzu_Online_Fix.Fixes
{
    class SudachiFix
    {
        public static void ApplySudachiFix()
        {
            try
            {
                // Status
                AnsiConsole.Status()
                    .Start("[Gold1]Starting Sudachi-Fix[/]", ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Star);
                        ctx.SpinnerStyle(Style.Parse("BlueViolet"));

                        // Check if qt-config.ini exists
                        string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sudachi", "config", "qt-config.ini");
                        AnsiConsole.MarkupLine("[BlueViolet]LOG:[/] [White]Searching config file...[/]");
                        if (!File.Exists(configPath))
                        {
                            AnsiConsole.MarkupLine("[BlueViolet]LOG:[/] [White]qt-config.ini doesn't exist.[/]");
                            AnsiConsole.MarkupLine("");
                            AnsiConsole.MarkupLine("[White]Online-Fix[/] [red1]could not be applied![/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[BlueViolet]LOG:[/] [White]Config file found...[/]");

                            // Read config file
                            AnsiConsole.MarkupLine("[BlueViolet]LOG:[/] [White]Reading config file...[/]");
                            string[] lines = File.ReadAllLines(configPath);
                            ctx.Status("[Gold1]Applying Online-Fix...[/]");
                            AnsiConsole.MarkupLine("[BlueViolet]LOG:[/] [White]Editing config file...[/]");
                            for (int i = 0; i < lines.Length; i++)
                            {
                                // Edit config file
                                if (lines[i].StartsWith("web_api_url\\default="))
                                {
                                    lines[i] = "web_api_url\\default=false";
                                }
                                else if (lines[i].StartsWith("web_api_url="))
                                {
                                    lines[i] = "web_api_url=api.ynet-fun.xyz";
                                }
                                else if (lines[i].StartsWith("enable_telemetry\\default="))
                                {
                                    lines[i] = "enable_telemetry\\default=false";
                                }
                                else if (lines[i].StartsWith("enable_telemetry="))
                                {
                                    lines[i] = "enable_telemetry=false";
                                }

                                if (!string.IsNullOrEmpty(ChangeUsername.Username) && !string.IsNullOrEmpty(ChangeUsername.Token))
                                {
                                    if (lines[i].StartsWith("sudachi_username\\default="))
                                    {
                                        lines[i] = "sudachi_username\\default=false";
                                    }
                                    else if (lines[i].StartsWith("sudachi_username="))
                                    {
                                        lines[i] = $"sudachi_username={ChangeUsername.Username}";
                                    }
                                    else if (lines[i].StartsWith("sudachi_token\\default="))
                                    {
                                        lines[i] = "sudachi_token\\default=false";
                                    }
                                    else if (lines[i].StartsWith("sudachi_token="))
                                    {
                                        lines[i] = $"sudachi_token={ChangeUsername.Token}";
                                    }
                                }
                            }

                            // Save Changes
                            AnsiConsole.MarkupLine("[BlueViolet]LOG:[/] [White]Saving changes...[/]");
                            File.WriteAllLines(configPath, lines);

                            var rule = new Rule();
                            rule.Style = Style.Parse("BlueViolet");
                            AnsiConsole.Write(rule);

                            AnsiConsole.MarkupLine("");
                            AnsiConsole.MarkupLine("[White]Online-Fix[/] [green]successfully applied![/]");

                            if (!string.IsNullOrEmpty(ChangeUsername.Username) && !string.IsNullOrEmpty(ChangeUsername.Token))
                            {
                                AnsiConsole.MarkupLine($"[White]New Username:[/] [gold1]{ChangeUsername.Username}[/]");
                                AnsiConsole.MarkupLine($"[White]New Token:[/] [gold1]{ChangeUsername.Token}[/]");
                            }
                        }
                    });
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("");
                AnsiConsole.MarkupLine($"[red1]An error occurred:[/]\n[maroon]{ex.Message}[/]");
            }

            AnsiConsole.MarkupLine("");
            AnsiConsole.MarkupLine("[White]Press[/] [Gold1]enter[/] [White]to return...[/]");
            Console.ReadKey();
            Console.Clear();
            Program.ShowMenu();
            return;
        }
    }
}