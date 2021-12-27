using Hovercabs.FSM.States.Base;
using Hovercabs.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hovercabs.FSM.States
{
    public class MainMenuState : State
    {
        private AsyncOperation _operation;

        public MainMenuState(GameManager gameManager) : base(gameManager)
        {
            
        }

        public override void Start()
        {
            _operation = SceneManager.UnloadSceneAsync("Loading");
            _operation.completed += onLoadingSceneUnloaded;
        }

        private void onLoadingSceneUnloaded(AsyncOperation obj)
        {
            _operation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        }

        public override void Update()
        {
            
        }
    }
}