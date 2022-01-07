using Hovercabs.Controllers;
using Hovercabs.Events;
using Hovercabs.FSM.States.Base;
using Hovercabs.Managers;
using Hovercabs.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Hovercabs.FSM.States
{
    public class GameplayState : State
    {
        private readonly TrackManager _trackManager;
        private readonly VehiclesService _vehiclesService;
        private AsyncOperation _operation;
        private GameplayController _gameplayController;
        
        public GameplayState(GameManager gameManager) : base(gameManager)
        {
            _trackManager = gameManager.TrackManager;
            _vehiclesService = gameManager.VehiclesService;
        }
        
        public override void Start()
        {
            _operation = SceneManager.UnloadSceneAsync("MainMenu");
            _operation.completed += OnMainMenuSceneUnloaded;
        }

        private void OnMainMenuSceneUnloaded(AsyncOperation obj)
        {
            _operation = SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Additive);
            _operation.completed -= OnMainMenuSceneUnloaded;
            _operation.completed += OnGameplaySceneLoaded;
        }
        
        private void OnGameplaySceneLoaded(AsyncOperation obj)
        {
            _operation.completed -= OnGameplaySceneLoaded;
            _gameplayController = Object.FindObjectOfType<GameplayController>();
            _gameplayController.Init(_trackManager, _vehiclesService, OnQuitRace);
        }

        private void OnQuitRace()
        {
            EventBus.GetBus().Send(new OnStateChanged
            {
                State = new MainMenuState(GameManager, GameManager.ProfileService, GameManager.VehiclesService, "Gameplay")
            });
        }
    }
}