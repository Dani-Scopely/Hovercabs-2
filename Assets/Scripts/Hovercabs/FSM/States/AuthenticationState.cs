using System;
using Hovercabs.FSM.States.Base;
using Hovercabs.Managers;
using Hovercabs.Models;
using Hovercabs.Services;
using UnityEngine;

namespace Hovercabs.FSM.States
{
    public class AuthenticationState : State
    {
        private readonly ProfileService _profileService;
        private readonly Action<bool> _onAuthenticated;
        
        public AuthenticationState(GameManager gameManager, ProfileService profileService, Action<bool> onAuthenticated) : base(gameManager)
        {
            _profileService = profileService;
            _onAuthenticated = onAuthenticated;
        }

        public override void Start()
        {
            _profileService.Login("username","password", OnLoginResult);
        }

        private void OnLoginResult(Profile profile)
        {
            Debug.Log("Welcome: "+profile.Username);
            
            _onAuthenticated?.Invoke(true);
        }
    }
}