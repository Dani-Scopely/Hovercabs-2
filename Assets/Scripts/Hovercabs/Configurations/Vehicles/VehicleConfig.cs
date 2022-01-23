using UnityEngine;

namespace Hovercabs.Configurations.Vehicles
{
    [CreateAssetMenu(fileName = "VehicleConfig", menuName = "Hovercabs/Vehicles/VehicleConfiguration", order = 2)]
    public class VehicleConfig : ScriptableObject
    {
        public string id;
        public float maxSpeed;
        public float maxAcceleration;
        public float breakStrength;
        public float fuelConsumption;
        public float timeToTurn;
        public int level;
        public Sprite emblem;
        public GameObject prefabHigh;
        public GameObject prefabLow;
    }
}
