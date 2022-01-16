using System;
using Hovercabs.Configurations.Showcase;
using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.Views
{
    public class VehicleShowcaseView : MonoBehaviour
    {
        private Material _showcaseMaterial;
        private VehicleShowcaseConfig _config;
        private static readonly int IsAvailableProperty = Shader.PropertyToID("IsAvailable");
        private static readonly int RotationSpeedProperty = Shader.PropertyToID("RotationSpeed");
        private static readonly int MovementSpeedProperty = Shader.PropertyToID("MovementSpeed");
        private static readonly int MovementOffsetProperty = Shader.PropertyToID("MovementOffset");
        private static readonly int MovementStrengthProperty = Shader.PropertyToID("MovementStrength");
        private void Awake()
        {
            _showcaseMaterial = GetComponent<Renderer>().sharedMaterial;
        }

        public void Init(VehicleShowcaseConfig config)
        {
            _config = config;
        }
        
        public void Render(Vehicle vehicle)
        {
            _showcaseMaterial.SetFloat(IsAvailableProperty,vehicle.IsAvailable ? 1f : 0f);
            
            _showcaseMaterial.SetFloat(RotationSpeedProperty, _config.rotationSpeed);
            _showcaseMaterial.SetFloat(MovementSpeedProperty, _config.movementSpeed);
            _showcaseMaterial.SetFloat(MovementOffsetProperty, _config.movementOffset);
            _showcaseMaterial.SetFloat(MovementStrengthProperty, _config.movementStrength);
        }
    }
}