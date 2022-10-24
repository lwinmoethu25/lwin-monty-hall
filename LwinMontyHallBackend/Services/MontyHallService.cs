using System;
using LwinMontyHall.Enums;
using LwinMontyHall.Models;

namespace LwinMontyHall.Services
{
    public interface IMontyHallService
    {
        Task<MontyHallResponse> startSimulation(MontyHallRequest request); 
        Task<IMontyHallDoor> userChooseDoor(int doorIndex);
        Task<IMontyHallDoor> userChangeDoor(DoorState doorState);
        Task<IMontyHallDoor> hostOpenDoor();
        Task resetDoor();    
    }

    public class MontyHallService : IMontyHallService
    {
        
        // private readonly ILogger<MontyHallService> _logger;
        private readonly Random _random;

        public MontyHallService()
        {
            // _logger = logger;
            _random = new Random();
        }

        public const string GOAT = "Goat";
        public const string CAR = "Car";

        private List<IMontyHallDoor> _montyHallDoors = new List<IMontyHallDoor>
        {
            new MontyHallDoor { DoorState = DoorState.Closed, Prize = GOAT },
            new MontyHallDoor { DoorState = DoorState.Closed, Prize = GOAT },
            new MontyHallDoor { DoorState = DoorState.Closed, Prize = CAR },
        };

        public async Task<MontyHallResponse> startSimulation(MontyHallRequest request)
        {
            var winCounts = 0;
            var lossCounts = 0;

            if (request.simulationCounts <= 0)
            {
                throw new InvalidOperationException("Invalid simulation Counts.");
            }

            for (var i = 0; i < request.simulationCounts; i++)
            {
                // Random Door Choose
                var randomDoor = _random.Next(0, 3);
                var initialChosen = await userChooseDoor(randomDoor);

                // Host Open Door
                var hostChoose = hostOpenDoor();

                // User Final Choose Door
                var finalDoor = request.isDoorChanged ? await userChangeDoor(DoorState.Closed) : initialChosen;

                // Determine Win/Lose
                if (finalDoor.Prize == CAR)
                {
                    winCounts++;
                }
                else
                {
                    lossCounts++;
                }

                // Rest Doors
                await resetDoor();
            }

            return new MontyHallResponse()
            {
                SimulationCounts = request.simulationCounts,
                IsDoorChanged = request.isDoorChanged,
                WinCounts = winCounts,
                LossCounts = lossCounts,
            };
        }

        public async Task<IMontyHallDoor> userChooseDoor(int doorIndex)
        {
            if (doorIndex < 0 || doorIndex > 2)
            {
                throw new InvalidOperationException($"Door {doorIndex} doesn't exist.");
            }

            // Change Door State to Chosen
            _montyHallDoors[doorIndex].DoorState = DoorState.Chosen;
            return _montyHallDoors[doorIndex];
        }

        public async Task<IMontyHallDoor> hostOpenDoor()
        {
            var door = _montyHallDoors.First(x => x.Prize == GOAT && x.DoorState != DoorState.Chosen);
            door.DoorState = DoorState.Opened;
            return door;
        }

        public async Task<IMontyHallDoor> userChangeDoor(DoorState doorState)
        {
            var door = _montyHallDoors.Where(x => x.DoorState == doorState).First();
            door.DoorState = DoorState.Chosen;
            return door;
        }

        public async Task resetDoor()
        {
            _montyHallDoors.ForEach(x => x.DoorState = DoorState.Closed);
            _montyHallDoors = _montyHallDoors.OrderBy(x => new Random().Next()).ToList();
        }
    }
}
