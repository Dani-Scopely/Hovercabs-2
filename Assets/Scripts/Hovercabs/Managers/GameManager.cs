using System;
using Hovercabs.Events;
using Hovercabs.FSM;
using Hovercabs.FSM.States;
using Hovercabs.Models;
using Hovercabs.Services;
using UnityEngine;
using Utils;

namespace Hovercabs.Managers
{
    public class GameManager : StateMachine, IObserver
    {
        public ProfileService ProfileService { get; set; }
        public VehiclesService VehiclesService { get; set; }
        
        public void Init(ProfileService profileService, VehiclesService vehiclesService)
        {
            EventBus.GetBus().Register(this, typeof(OnStateChanged));
            
            ProfileService = profileService;
            VehiclesService = vehiclesService;
            
            SetState(new LoadingState(this, vehiclesService, OnLoaded));
        }

        private void OnLoaded()
        {
            SetState(new AuthenticationState(this, ProfileService, OnAuthenticated));
        }

        private void OnAuthenticated(bool isAuthenticated)
        {
            SetState(new MainMenuState(this, ProfileService, VehiclesService, "Loading"));
        }

        private void OnDestroy()
        {
            EventBus.GetBus().Unregister(this);
        }

        public void OnEvent(IEvent pEvent)
        {
            if (pEvent is OnStateChanged state)
            {
                SetState(state.State);
            }
        }
    }
}