using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace SharedLib
{
    public static class RuntimeEnvironmentProvider
    {
        public static RuntimeEnvironmentSettings GetRuntimeEnvironment()
        {
            return new RuntimeEnvironmentSettings()
            {
                RunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true",

                Framework = RuntimeInformation.FrameworkDescription,

                OSDescription = RuntimeInformation.OSDescription,

                ProcessorCount = Environment.ProcessorCount,

                MemoryCurrentUsage = Process.GetCurrentProcess().WorkingSet64,

                MemoryMaxAvailable = Process.GetCurrentProcess().MaxWorkingSet.ToInt64(),

                CGroupMemoryUsage = CGroupMemoryUsage
            };
        }

        private static string CGroupMemoryUsage
        {
            get
            {
                if (RuntimeInformation.OSDescription.StartsWith("Linux") && Directory.Exists("/sys/fs/cgroup/memory"))
                {
                    var lines = System.IO.File.ReadAllLines("/sys/fs/cgroup/memory/memory.usage_in_bytes");
                    if (lines.Length > 0)
                    {
                        return lines[0];
                    }
                }
                return null;
            }
        }
    }
}