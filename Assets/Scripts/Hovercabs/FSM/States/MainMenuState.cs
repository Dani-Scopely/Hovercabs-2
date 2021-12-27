using Hovercabs.Controllers;
using Hovercabs.FSM.States.Base;
using Hovercabs.Managers;
using Hovercabs.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hovercabs.FSM.States
{
    public class MainMenuState : State
    {
        private AsyncOperation _operation;
        private MainMenuController _mainMenuController;
        private readonly VehiclesService _vehiclesService;

        public MainMenuState(GameManager gameManager, VehiclesService vehiclesService) : base(gameManager)
        {
            _vehiclesService = vehiclesService;
        }

        public override void Start()
        {
            _operation = SceneManager.UnloadSceneAsync("Loading");
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
            _mainMenuController.Init(_vehiclesService);
        }
    }
}