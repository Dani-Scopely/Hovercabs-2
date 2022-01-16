using UnityEngine;

namespace Hovercabs.Configurations.Gameplay.Vehicles
{
    [CreateAssetMenu(fileName = "VehicleGameplayConfig", menuName = "Hovercabs/Gameplay/Vehicles/Configuration", order = 1)]
    public class VehicleGameplayConfig : ScriptableObject
    {
        public Vector3 initialPosition = new Vector3(0, 0.2f, 0);
        public Vector3 initialScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
