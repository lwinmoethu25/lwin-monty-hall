using System;

namespace LwinMontyHall.HealthChecks
{
    public class GCInfoOption : IGCInfoOption
    {
        public long Threshold { get; set; } = 1024L * 1024L * 1024L;
    }
}
