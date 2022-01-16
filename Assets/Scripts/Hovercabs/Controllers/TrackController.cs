using System;
using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class TrackController : MonoBehaviour
    {
        public Track TrackData { get; set; }
        private Action<GameObject> _onTrackExit;
        private GameObject _other;
        public float DestroyDistance { get; set; }
        private bool _markToDestroy = false;

        [SerializeField] private float distanceToVehicle = 0f;
        private VehicleController _vehicleController;
        private void Awake()
        {
            var collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
        }

        public TrackController Init(VehicleController vehicleController, Action<GameObject> onTrackExit, out Vector3 size)
        {
            _vehicleController = vehicleController;
            _onTrackExit = onTrackExit;
            _markToDestroy = false;

            size = GetComponent<MeshRenderer>().bounds.size;

            return this;
        }

        
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (_vehicleController == null) return;
            
            
            //Debug.Log($"{_trackData.Id} --> {other.name}");
        //}

        private void OnTriggerExit(Collider other)
        {
            _markToDestroy = other.CompareTag("Vehicle");
        }

        private void Update()
        {
            if (!_markToDestroy) return;
            
            distanceToVehicle = Vector3.Distance(_vehicleController.transform.position, transform.position);

            if (distanceToVehicle > DestroyDistance && _markToDestroy)
            {
                _onTrackExit?.Invoke(gameObject);
                //Destroy(gameObject);
            }
        }
    }
}
