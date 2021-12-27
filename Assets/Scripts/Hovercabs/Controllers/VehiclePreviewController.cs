using System;
using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class VehiclePreviewController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private string id;
        
        private void Update()
        {
            transform.Rotate(new Vector3(0,rotationSpeed*Time.deltaTime,0));
        }
    }
}
