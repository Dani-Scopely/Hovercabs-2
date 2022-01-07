using Hovercabs.Managers;
using Hovercabs.Models;
using Hovercabs.Services;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        
        private TrackManager _trackManager;
        private VehiclesService _vehiclesService;
        private Vehicle _currentVehicle;
        private GameObject _vehicle;
        
        public void Init(TrackManager trackManager, VehiclesService vehiclesService)
        {
            _trackManager = trackManager;
            _vehiclesService = vehiclesService;
            
            _trackManager.Init();
            _currentVehicle = _vehiclesService.GetCurrentVehicle();
         
            SetupVehicle();
            SetupCamera();
            
            Debug.Log($"We want to play with: {_currentVehicle.Id}");
        }

        private void SetupVehicle()
        {
            _vehicle = Instantiate(Resources.Load<GameObject>($"Vehicles/{_currentVehicle.Id}/{_currentVehicle.Id}_low"),
                transform, true);
            _vehicle.transform.position = Vector3.zero;
            _vehicle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        
        private void SetupCamera()
        {
            cameraController.SetTarget(_vehicle.transform);
        }
    }
}
