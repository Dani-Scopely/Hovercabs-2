using System.Data.Common;
using UnityEngine;

namespace Hovercabs.Models
{
    public class Vehicle
    {
        public string Id { get; }
        public GameObject Model { get; }
        public Sprite Emblem { get; }
        public int Level { get; set; }
        public bool IsAvailable { get; set; }

        public Vehicle(string id, int level, GameObject model, Sprite emblem)
        {
            Id = id;
            Level = level;
            Model = model;
            Emblem = emblem;
        }
    }
}