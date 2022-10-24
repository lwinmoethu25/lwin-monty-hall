using System;
using LwinMontyHall.Enums;

namespace LwinMontyHall.Models
{
    public interface IMontyHallDoor
    {
        DoorState DoorState { get; set; }
        string Prize { get; set; } // 'Car' or 'Goat'
    }

    public class MontyHallDoor: IMontyHallDoor
    {
        public DoorState DoorState { get; set; }
        public string Prize { get; set; }
    }
}
