using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiTerminal.Helpers
{

    public class TerminalCommandRunner
    {
        public static void ExecuteCommand(string command)
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = Environment.OSVersion.Platform == PlatformID.Win32NT ? "cmd.exe" : "/bin/bash",
                    Arguments = (Environment.OSVersion.Platform == PlatformID.Win32NT ? "/c " : "-c ") + $"\"{command}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(processInfo);
                if (process == null)
                {
                    Console.WriteLine("Failed to start the process.");
                    return;
                }

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.OutputDataReceived += (_, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Console.WriteLine($"Output: {e.Data}");
                    }
                };

                process.ErrorDataReceived += (_, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Console.WriteLine($"Error: {e.Data}");
                    }
                };

                process.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
            }
        }
    }

}
