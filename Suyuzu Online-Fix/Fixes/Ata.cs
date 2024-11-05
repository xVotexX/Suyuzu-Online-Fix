using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace Suyuzu_Online_Fix.Fixes
{
    class Ata
    {
        public static void ApplyToAll()
        {
            int fixAppliedCount = 0; 
            int expectedFixCount = 0;

            try
            {
                // Status
                AnsiConsole.Status()
                    .Start("[Gold1]Starting Emulator Fixes[/]", ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Star);
                        ctx.SpinnerStyle(Style.Parse("BlueViolet"));

                        // Define paths and prefixes for each emulator
                        var emulators = new (string ConfigPath, string Prefix, bool Exists)[]
                        {
                            (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "yuzu", "config", "qt-config.ini"), "yuzu", true),
                            (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "suyu", "config", "qt-config.ini"), "suyu", true),
                            (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sudachi", "config", "qt-config.ini"), "sudachi", true)
                        };

                        AnsiConsole.MarkupLine("[BlueViolet]LOG:[/] [White]Searching config files...[/]");

                        // Check each config
                        for (int j = 0; j < emulators.Length; j++)
                        {
                            var (configPath, prefix, exists) = emulators[j];
                            
                            if (!File.Exists(configPath))
                            {
                                AnsiConsole.MarkupLine($"[BlueViolet]LOG:[/] [White][Gold1]{prefix}[/] config doesn't exist[/]");
                                emulators[j].Exists = false;
                                continue;
                            }

                            AnsiConsole.MarkupLine($"[BlueViolet]LOG:[/] [White][Gold1]{prefix}[/] config found[/]");

                            // Read config file
                            var lines = File.ReadAllLines(configPath);
                            ctx.Status("[Gold1]Applying Online-Fixes[/]");
                            AnsiConsole.MarkupLine($"[BlueViolet]LOG:[/] [White]Editing [Gold1]{prefix}[/] config file...[/]");

                            for (int i = 0; i < lines.Length; i++)
                            {
                                // Modify API URL and telemetry settings
                                if (lines[i].StartsWith("web_api_url\\default="))
                                {
                                    lines[i] = "web_api_url\\default=false";
                                    fixAppliedCount++;
                                }
                                else if (lines[i].StartsWith("web_api_url="))
                                {
                                    lines[i] = "web_api_url=api.ynet-fun.xyz";
                                    fixAppliedCount++;
                                }
                                else if (lines[i].StartsWith("enable_telemetry\\default="))
                                {
                                    lines[i] = "enable_telemetry\\default=false";
                                    fixAppliedCount++;
                                }
                                else if (lines[i].StartsWith("enable_telemetry="))
                                {
                                    lines[i] = "enable_telemetry=false";
                                    fixAppliedCount++;
                                }

                                // Apply username and token if set
                                if (!string.IsNullOrEmpty(ChangeUsername.Username) && !string.IsNullOrEmpty(ChangeUsername.Token))
                                {
                                    if (lines[i].StartsWith($"{prefix}_username\\default="))
                                    {
                                        lines[i] = $"{prefix}_username\\default=false";
                                        fixAppliedCount++;
                                    }
                                    else if (lines[i].StartsWith($"{prefix}_username="))
                                    {
                                        lines[i] = $"{prefix}_username={ChangeUsername.Username}";
                                        fixAppliedCount++;
                                    }
                                    else if (lines[i].StartsWith($"{prefix}_token\\default="))
                                    {
                                        lines[i] = $"{prefix}_token\\default=false";
                                        fixAppliedCount++;
                                    }
                                    else if (lines[i].StartsWith($"{prefix}_token="))
                                    {
                                        lines[i] = $"{prefix}_token={ChangeUsername.Token}";
                                        fixAppliedCount++;
                                    }
                                }
                            }

                            // Save changes
                            AnsiConsole.MarkupLine($"[BlueViolet]LOG:[/] [White]Saving [Gold1]{prefix}[/] config changes...[/]");
                            File.WriteAllLines(configPath, lines);
                        }

                        var rule = new Rule();
                        rule.Style = Style.Parse("BlueViolet");
                        AnsiConsole.Write(rule);

                        AnsiConsole.MarkupLine("");

                        if (string.IsNullOrEmpty(ChangeUsername.Username) && string.IsNullOrEmpty(ChangeUsername.Token))
                        {
                            expectedFixCount = 12;
                        }
                        else
                        {
                            expectedFixCount = 24;
                        }

                        // Check if all expected fixes were applied
                        if (fixAppliedCount == expectedFixCount)
                        {
                            AnsiConsole.MarkupLine($"[White]Online Fixes[/] [green]successfully applied![/]");
                        }
                        else if (fixAppliedCount > 0 && fixAppliedCount < expectedFixCount)
                        {
                            AnsiConsole.MarkupLine($"[White]Online Fixes applied, but some settings [Gold1]were already set[/] or [Gold1]could not be updated[/]. Check the logs for details.[/]\n{fixAppliedCount}, {expectedFixCount}");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine($"[White]Online Fixes [red1]could not be applied![/] Check the logs for details.[/]\n{fixAppliedCount}, {expectedFixCount}");
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
        }
    }
}