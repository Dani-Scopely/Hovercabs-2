using Hovercabs.FSM;
using Hovercabs.FSM.States;
using Hovercabs.Models;
using Hovercabs.Services;

namespace Hovercabs.Managers
{
    public class GameManager : StateMachine
    {
        private ProfileService _profileService;
        private VehiclesService _vehiclesService;
        
        public void Init(ProfileService profileService, VehiclesService vehiclesService)
        {
            _profileService = profileService;
            _vehiclesService = vehiclesService;
            SetState(new LoadingState(this, vehiclesService, OnLoaded));
        }

        private void OnLoaded()
        {
            SetState(new AuthenticationState(this, _profileService, OnAuthenticated));
        }

        private void OnAuthenticated(bool isAuthenticated)
        {
            SetState(new MainMenuState(this));
        }
    }
}