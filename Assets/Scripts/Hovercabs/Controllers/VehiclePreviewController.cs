using System;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class VehiclePreviewController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        
        private void Update()
        {
            transform.Rotate(new Vector3(0,rotationSpeed*Time.deltaTime,0));
        }
    }
}
