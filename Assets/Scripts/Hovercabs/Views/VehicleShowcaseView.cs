using System;
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

        public void Render()
        {
            _showcaseMaterial.SetFloat(IsAvailableProperty,1f);         
        }
    }
}