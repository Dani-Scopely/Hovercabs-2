using System;
using Hovercabs.Configurations.Vehicles;
using Hovercabs.Managers;
using Hovercabs.Services;
using UnityEngine;

namespace Hovercabs
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TrackManager trackManager;
        
        [Header("Configurations")]
        [SerializeField] private VehiclesConfig vehiclesConfig;
        
        private ProfileService _profileService;
        private VehiclesService _vehiclesService;
        private TrackService _trackService;
        
        private void Awake()
        {
            _profileService = new ProfileService();
            _vehiclesService = new VehiclesService(vehiclesConfig);
            _trackService = new TrackService();
        }

        private void Start()
        {
            gameManager.Init(_trackService, _profileService, _vehiclesService, trackManager);    
        }
    }
}
