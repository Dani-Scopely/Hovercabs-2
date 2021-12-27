using System;
using Hovercabs.Models;
using Hovercabs.Models.DTO;
using Newtonsoft.Json;
using UnityEngine;

namespace Hovercabs.Services
{
    public class ProfileService
    {
        private Action<Profile> _onLoginResult;
        
        public ProfileService()
        {
            
        }

        public void Login(string username, string password, Action<Profile> onLoginResult)
        {
            _onLoginResult = onLoginResult;
            var txt = Resources.Load<TextAsset>("demo-profile").text;
            var profileDto = JsonConvert.DeserializeObject<ProfileDto>(txt);
            _onLoginResult?.Invoke(new Profile(profileDto));
        }
    }
}