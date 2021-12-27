using System.Data.Common;
using UnityEngine;

namespace Hovercabs.Models
{
    public class Vehicle
    {
        public string Id { get; }
        public GameObject Model { get; }
        public Sprite Emblem { get; }

        public Vehicle(string id, GameObject model, Sprite emblem)
        {
            Id = id;
            Model = model;
            Emblem = emblem;
        }
    }
}