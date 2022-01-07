using System;
using Hovercabs.FSM.States.Base;
using UnityEngine;

namespace Hovercabs.FSM
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State CurrentState;

        public void SetState(State state)
        {
            CurrentState?.Stop();
            CurrentState = state;
            CurrentState?.Start();
        }

        private void Update()
        {
            CurrentState?.Update();
        }
    }
}
