using System;
using Hovercabs.Events;
using Hovercabs.Services;
using TMPro;
using UnityEngine;
using Utils;

namespace Hovercabs.Controllers
{
    public class LoadingController : MonoBehaviour, IObserver
    {
        private Action _onResourcesInitialized;
        private VehiclesService _vehiclesService;
        
        [SerializeField] private TextMeshProUGUI assetsLoadedProgressTxt;

        private void Awake()
        {
            EventBus.GetBus().Register(this, typeof(OnResourceLoaded));
        }

        public void Init(VehiclesService vehiclesService, Action onResourcesInitialized)
        {
            _onResourcesInitialized = onResourcesInitialized;
            _vehiclesService = vehiclesService;
            InitResources();
        }
        
        private void InitResources()
        {
            InitVehiclesResources();
        }

        private void InitVehiclesResources()
        {
            _vehiclesService.OnVehiclesLoaded += OnVehiclesLoaded;
            _vehiclesService.Init();
        }

        private void OnVehiclesLoaded()
        {
            _vehiclesService.OnVehiclesLoaded -= OnVehiclesLoaded;
            _onResourcesInitialized?.Invoke();
        }

        private void OnDestroy()
        {
            EventBus.GetBus().Unregister(this);
        }

        public void OnEvent(IEvent pEvent)
        {
            if (pEvent is OnResourceLoaded onResourceLoaded)
            {
                assetsLoadedProgressTxt.text = onResourceLoaded.ResourceName;
            }
        }
    }
}