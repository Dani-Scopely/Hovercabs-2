using Hovercabs.Events;
using Hovercabs.FSM.States.Base;
using UnityEngine;
using Utils;

namespace Hovercabs.FSM.States
{
    public class QuitRaceState : State
    {
        public QuitRaceState() : base()
        {
            
        }

        public override void Start()
        {
            EventBus.GetBus().Send(new OnStateChanged
            {
                State = new MainMenuState(GameManager, GameManager.ProfileService, GameManager.VehiclesService, "Gameplay")
            });
        }
    }
}
