namespace SharedLib
{
    public class RuntimeEnvironmentSettings
    {
        private const decimal OneMb = 1048576;
        private const int RoundLength = 3;

        public bool RunningInContainer { get; set; }

        public string Framework { get; set; }

        public string OSDescription { get; set; }

        public int ProcessorCount { get; set; }

        public long MemoryCurrentUsage { get; set; }

        public decimal MemoryCurrentUsageMb => decimal.Round(MemoryCurrentUsage / OneMb, RoundLength);

        public long MemoryMaxAvailable { get; set; }

        public decimal MemoryMaxAvailableMb => decimal.Round(MemoryMaxAvailable / OneMb, RoundLength);

        public string CGroupMemoryUsage { get; set; }

        public decimal CGroupMemoryUsageMb
        {
            get
            {
                var usage = CGroupMemoryUsage;
                if (!long.TryParse(usage, out long usageNumber))
                {
                    usageNumber = 0;
                }

                return decimal.Round(usageNumber / OneMb, RoundLength);
            }
        }
    }
}