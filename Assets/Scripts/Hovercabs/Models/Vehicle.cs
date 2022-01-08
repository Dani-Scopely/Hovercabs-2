using System.Data.Common;
using Hovercabs.Configurations.Vehicles;
using UnityEngine;

namespace Hovercabs.Models
{
    public class Vehicle
    {
        public string Id { get; }
        public GameObject ModelHigh { get; }
        public GameObject ModelLow { get; }
        public Sprite Emblem { get; }
        public int Level { get; set; }
        public bool IsAvailable { get; set; }

        public Vehicle(VehicleConfig vehicleConfig)
        {
            Id = vehicleConfig.id;
            Level = vehicleConfig.level;
            ModelHigh = vehicleConfig.prefabHigh;
            ModelLow = vehicleConfig.prefabLow;
            Emblem = vehicleConfig.emblem;
        }
    }
}