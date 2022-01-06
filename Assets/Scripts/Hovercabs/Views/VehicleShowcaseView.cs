using System;
using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.Views
{
    public class VehicleShowcaseView : MonoBehaviour
    {
        private Material _showcaseMaterial;
        private static readonly int IsAvailableProperty = Shader.PropertyToID("IsAvailable");

        private void Awake()
        {
            _showcaseMaterial = GetComponent<Renderer>().sharedMaterial;
        }

        public void Render(Vehicle vehicle)
        {
            Debug.Log("Is available: "+vehicle.IsAvailable);
            _showcaseMaterial.SetFloat(IsAvailableProperty,vehicle.IsAvailable ? 1f : 0f);         
        }
    }
}