using Hovercabs.Controllers;
using Hovercabs.Events;
using Hovercabs.FSM.States.Base;
using Hovercabs.Managers;
using Hovercabs.Models;
using Hovercabs.Services;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Hovercabs.FSM.States
{
    public class GameplayState : State
    {
        private readonly TrackManager _trackManager;
        private readonly VehiclesService _vehiclesService;
        private readonly ProfileService _profileService;
        private AsyncOperation _operation;
        private GameplayController _gameplayController;
        
        public GameplayState(GameManager gameManager) : base(gameManager)
        {
            _trackManager = gameManager.TrackManager;
            _vehiclesService = gameManager.VehiclesService;
            _profileService = gameManager.ProfileService;
        }
        
        public override void Start()
        {
            _operation = SceneManager.UnloadSceneAsync("MainMenu");
            _operation.completed += OnMainMenuSceneUnloaded;
        }

        public override void Stop()
        {
            _trackManager.Reset();
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
            _gameplayController.Init(_trackManager, _vehiclesService, _profileService, OnQuitRace, OnGameOver);
        }

        private void OnQuitRace()
        {
            EventBus.GetBus().Send(new OnStateChanged
            {
                State = new MainMenuState(GameManager, GameManager.ProfileService, GameManager.VehiclesService, "Gameplay")
            });
        }

        private void OnGameOver(Result result)
        {
            Debug.Log("RESULT: "+JsonConvert.SerializeObject(result));
            
            EventBus.GetBus().Send(new OnStateChanged
            {
                State = new MainMenuState(GameManager, GameManager.ProfileService, GameManager.VehiclesService, "Gameplay")
            });
        }
    }
}