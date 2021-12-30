using System;
using Hovercabs.Controllers;
using Hovercabs.Events;
using Hovercabs.FSM.States.Base;
using Hovercabs.Managers;
using Hovercabs.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Object = UnityEngine.Object;

namespace Hovercabs.FSM.States
{
    public class MainMenuState : State
    {
        private AsyncOperation _operation;
        private MainMenuController _mainMenuController;
        private readonly VehiclesService _vehiclesService;
        private readonly ProfileService _profileService;

        public MainMenuState(GameManager gameManager, ProfileService profileService, VehiclesService vehiclesService, string lastSceneId) : base(gameManager, lastSceneId)
        {
            _vehiclesService = vehiclesService;
            _profileService = profileService;
        }

        public override void Start()
        {
            _operation = SceneManager.UnloadSceneAsync(LastSceneId);
            _operation.completed += OnLoadingSceneUnloaded;
        }

        private void OnLoadingSceneUnloaded(AsyncOperation obj)
        {
            _operation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
            _operation.completed -= OnLoadingSceneUnloaded;
            _operation.completed += OnMainMenuSceneLoaded;
        }

        private void OnMainMenuSceneLoaded(AsyncOperation obj)
        {
            _operation.completed -= OnMainMenuSceneLoaded;
            _mainMenuController = Object.FindObjectOfType<MainMenuController>();
            _mainMenuController.Init(_profileService, _vehiclesService, OnPlaySingle, OnPlayMultiplayer);
        }

        private void OnPlaySingle()
        {
            EventBus.GetBus().Send(new OnStateChanged
            {
                State = new GameplayState(GameManager)
            });
        }

        private void OnPlayMultiplayer()
        {
            EventBus.GetBus().Send(new OnStateChanged
            {
                State = new GameplayState(GameManager)
            });
        }
    }
}