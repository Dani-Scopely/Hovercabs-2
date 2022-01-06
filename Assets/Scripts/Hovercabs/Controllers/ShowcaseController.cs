using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class ShowcaseController : MonoBehaviour
    {
        private GameObject _currentVehicle;
        private VehicleShowcaseController _vehicleShowcaseController;

        [SerializeField] private VehicleEmblemController vehicleEmblemController;

        public void SetVehicle(Vehicle vehicle)
        {
            if(_currentVehicle!=null) Destroy(_currentVehicle);
            
            _currentVehicle = Instantiate(vehicle.Model, transform);
            
            _currentVehicle.transform.localPosition = Vector3.zero;
            _currentVehicle.transform.localScale = Vector3.one;
            
            _vehicleShowcaseController = _currentVehicle.GetComponent<VehicleShowcaseController>();
            
            _vehicleShowcaseController.SetVehicleInfo(vehicle);

            vehicleEmblemController.SetEmblem(vehicle);
            
        }
    }
}