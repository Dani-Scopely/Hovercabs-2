using System;
using Hovercabs.Events;
using Hovercabs.FSM;
using Hovercabs.FSM.States;
using Hovercabs.Managers;
using Hovercabs.UI;
using UnityEngine;
using Utils;

namespace Hovercabs.Controllers
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private GameplayCanvasController gameplayCanvasController;

        public void Init(TrackManager trackManager, Action onQuit)
        {
            gameplayCanvasController.Init(onQuit);
            
            trackManager.Init();
            
        }
    }
}