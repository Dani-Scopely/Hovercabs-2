using Hovercabs.FSM;
using Hovercabs.FSM.States;
using Hovercabs.Models;
using Hovercabs.Services;

namespace Hovercabs.Managers
{
    public class GameManager : StateMachine
    {
        private ProfileService _profileService;
        
        public void Init(ProfileService profileService)
        {
            _profileService = profileService;
            SetState(new LoadingState(this, OnLoaded));
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