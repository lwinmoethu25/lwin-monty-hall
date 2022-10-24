using System;

namespace LwinMontyHall.Models
{
    public interface IMontyHallResponse
    {
        public int SimulationCounts { get;}
        public bool IsDoorChanged { get; }
        public int WinCounts { get; }
        public double WinRatesPercentage { get;}
        public int LossCounts { get; }
        public double LossRatesPercentage { get; }
    }

    public class MontyHallResponse : IMontyHallResponse
    {
        public int SimulationCounts { get; set; }
        public bool IsDoorChanged { get; set; }
        public int WinCounts { get; set; }
        public double WinRatesPercentage => (WinCounts *100)/SimulationCounts;
        public int LossCounts { get; set; }
        public double LossRatesPercentage => (LossCounts * 100)/SimulationCounts;
    }
}
