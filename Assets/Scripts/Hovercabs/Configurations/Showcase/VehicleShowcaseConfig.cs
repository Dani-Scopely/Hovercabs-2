using UnityEngine;

namespace Hovercabs.Configurations.Showcase
{
    [CreateAssetMenu(fileName = "VehicleShowcaseConfig", menuName = "Hovercabs/Showcase/Configuration", order = 1)]
    public class VehicleShowcaseConfig : ScriptableObject
    {
        public float rotationSpeed = 0f;
        public float movementOffset = 0.3f;
        public float movementSpeed = 2.3f;
        public float movementStrength = 0.1f;
    }
}
