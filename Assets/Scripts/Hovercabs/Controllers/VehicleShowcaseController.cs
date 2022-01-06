using System;
using Hovercabs.Models;
using Hovercabs.Views;
using UnityEngine;

namespace Hovercabs.Controllers
{
    [RequireComponent(typeof(VehicleShowcaseView))]
    public class VehicleShowcaseController : MonoBehaviour
    {
        private VehicleShowcaseView _view;
        public float rotationSpeed;
        [SerializeField] private string id;
        
        private void Awake()
        {
            _view = GetComponent<VehicleShowcaseView>();
        }

        private void Start()
        {
            _view.Render();
        }

        private void Update()
        {
            transform.Rotate(new Vector3(0,rotationSpeed*Time.deltaTime,0));
        }
    }
}
