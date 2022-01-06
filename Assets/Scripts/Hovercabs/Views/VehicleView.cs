using UnityEngine;

namespace Hovercabs.Views
{
    public class VehicleView: MonoBehaviour
    {
        private Material _vehicleMaterial;
        private static readonly int IsAvailableProperty = Shader.PropertyToID("IsAvailable");
        private static readonly int RotationSpeedProperty = Shader.PropertyToID("RotationSpeed");
        //private static readonly int RotationAxisProperty = Shader.PropertyToID("RotationAxis");
        
        private void Awake()
        {
            _vehicleMaterial = GetComponent<Renderer>().sharedMaterial;
        }
        
        public void Render()
        {
            _vehicleMaterial.SetFloat(IsAvailableProperty,1f);  
            _vehicleMaterial.SetFloat(RotationSpeedProperty, 0f);
        }
    }
}