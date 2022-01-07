using Hovercabs.Controllers;
using Hovercabs.FSM.States.Base;
using Hovercabs.Managers;
using UnityEngine;

namespace Hovercabs.FSM.States
{
    public class InitRaceState : State
    {
        private readonly GameplayController _gameplayController;
        private readonly TrackManager _trackManager;
        
        public InitRaceState(GameplayController gameplayController, TrackManager trackManager) : base()
        {
            _gameplayController = gameplayController;
            _trackManager = trackManager;
        }

        public override void Start()
        {
            _trackManager.Init();
        }
    }
}
