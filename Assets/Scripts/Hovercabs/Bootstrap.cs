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
        
        private void Awake()
        {
            _profileService = new ProfileService();
            _vehiclesService = new VehiclesService(vehiclesConfig);
        }

        private void Start()
        {
            gameManager.Init(_profileService, _vehiclesService, trackManager);    
        }
    }
}
