using System;

namespace LwinMontyHall.HealthChecks
{
    public interface IGCInfoOption
    {
        long Threshold { get; set; }
    }
}
