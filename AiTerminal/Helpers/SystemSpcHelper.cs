using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AiTerminal.Helpers
{
    public class SystemSpcHelper
    {
        public static SystemSpecModel GetSystemSpec()
        {
            var systemSpec = new SystemSpecModel
            {
                OSPlatform = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Windows" :
                             RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "macOS" :
                             RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Linux" : "Unknown",
                OSDescription = RuntimeInformation.OSDescription,
                OSVersion = Environment.OSVersion.ToString(),
                UserName = Environment.UserName,
                UserPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Executor = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmd" :
                           RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "bash, apt-get" :
                           RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "bash" : "Unknown"
            };

            return systemSpec;
        }

        public class SystemSpecModel
        {
            public string OSPlatform { get; set; }
            public string OSDescription { get; set; }
            public string OSVersion { get; set; }
            public string UserName { get; set; }
            public string UserPath { get; set; }
            public string Executor { get; set; }
        }
    }
}
