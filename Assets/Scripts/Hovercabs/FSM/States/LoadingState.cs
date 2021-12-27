using System;
using System.Collections;
using System.Threading;
using Hovercabs.Controllers;
using Hovercabs.FSM.States.Base;
using Hovercabs.Managers;
using Hovercabs.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Object = UnityEngine.Object;

namespace Hovercabs.FSM.States
{
    public class LoadingState : State
    {
        private AsyncOperation _operation;
        private readonly Action _onLoaded;
        private bool _isLoadingDependencies = false;
        private LoadingController _loadingController;
        private readonly VehiclesService _vehiclesService;
        
        public LoadingState(GameManager gameManager, VehiclesService vehiclesService, Action onLoaded) : base(gameManager)
        {
            _vehiclesService = vehiclesService;
            _onLoaded = onLoaded;
        }

        public override void Start()
        {
            _operation = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        }

        public override void Update()
        {
            if (_operation.isDone && !_isLoadingDependencies)
            {
                _isLoadingDependencies = true;
                
                _loadingController = Object.FindObjectOfType<LoadingController>();

                _loadingController.Init(_vehiclesService, OnResourcesInitialized);
            }
        }

        public override void Stop()
        {
            _isLoadingDependencies = false;
        }
        
        private void OnResourcesInitialized()
        {
            _onLoaded?.Invoke();
        }
    }
}