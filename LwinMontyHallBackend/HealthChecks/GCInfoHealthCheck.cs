using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace LwinMontyHall.HealthChecks
{
    public class GCInfoHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<GCInfoOption> _option;
        
        public GCInfoHealthCheck(IOptionsMonitor<GCInfoOption> option)
        {
            _option = option;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // This example will report degraded status if the application is using more than the configured amount of memory (1gb by default).
            // Additionally we include some GC info in the reported diagnostics.
            var options = _option.Get(context.Registration.Name);
            var allocated = GC.GetTotalMemory(forceFullCollection: false);

            var data = new Dictionary<string, object>
            {
                { "Allocated", allocated },
                { "Gen0Collections", GC.CollectionCount(0) },
                { "Gen1Collections", GC.CollectionCount(1) },
                { "Gen2Collections", GC.CollectionCount(2) }
            };

            // Report failure if the allocated memory is >= the threshold.
            // Using context.Registration.FailureStatus means that the application developer can configure how they want failures to appear.
            var status = allocated >= options.Threshold
                ? context.Registration.FailureStatus
                : HealthStatus.Healthy;

            return Task.FromResult(new HealthCheckResult(
                status,
                description: "reports degraded status if allocated bytes >= 1gb",
                data: data));
        }
    }
}