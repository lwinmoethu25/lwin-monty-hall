using System;
using LwinMontyHall.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LwinMontyHall.Extensions
{
    public static class HealthCheckBuilderExtension
    {
        public static IHealthChecksBuilder AddGCInfoCheck(
            this IHealthChecksBuilder builder,
            string name,
            HealthStatus? failureStatus = null,
            IEnumerable<string>? tags = null,
            long? thresholdInBytes = null)
        {
            builder.AddCheck<GCInfoHealthCheck>(
                name, 
                failureStatus ?? HealthStatus.Degraded, 
                tags ?? Enumerable.Empty<string>());

            if (thresholdInBytes.HasValue)
                builder.Services.Configure<GCInfoOption>(name, options => options.Threshold = thresholdInBytes.Value);

            return builder;
        }
    }
}