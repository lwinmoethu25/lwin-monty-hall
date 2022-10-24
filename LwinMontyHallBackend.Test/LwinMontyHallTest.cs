using System;
using LwinMontyHall.Models;
using LwinMontyHall.Services;
using Xunit;

namespace LwinMontyHallBackend.Test
{
    public class LwinMontyHallTest
    {
        private readonly IMontyHallService _service;

        public LwinMontyHallTest()
        {
            _service = new MontyHallService();
        }

        [Fact]
        public async void LwinMontyHallTest_ChangeDoor_Success()
        {
            var simulationRequest = new MontyHallRequest()
            {
                simulationCounts = 100,
                isDoorChanged = true
            };
            var result = await _service.startSimulation(simulationRequest);
            Assert.True(result.WinCounts > result.LossCounts);
        }

        [Fact]
        public async void LwinMontyHallTest_DoNotChangeDoor_Success()
        {
            var simulationRequest = new MontyHallRequest()
            {
                simulationCounts = 100,
                isDoorChanged = false
            };
            var result = await _service.startSimulation(simulationRequest);
            Assert.True(result.WinCounts < result.LossCounts);
        }
        
    }
}
