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

        public void Init(VehicleController vehicleController, Action<GameObject> onTrackExit, out Vector3 size)
        {
            _vehicleController = vehicleController;
            _onTrackExit = onTrackExit;
            _markToDestroy = false;

            size = GetComponent<MeshRenderer>().bounds.size;
        }

        private void OnTriggerExit(Collider other)
        {
            _markToDestroy = other.CompareTag("Vehicle");
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Vehicle"))
            {
                other.GetComponent<VehicleController>().SetCurrentTrack(this);
            }
        }

        private void Update()
        {
            if (!_markToDestroy) return;
            
            distanceToVehicle = Vector3.Distance(_vehicleController.transform.position, transform.position);

            if (distanceToVehicle > DestroyDistance && _markToDestroy)
            {
                _onTrackExit?.Invoke(gameObject);
            }
        }
    }
}
