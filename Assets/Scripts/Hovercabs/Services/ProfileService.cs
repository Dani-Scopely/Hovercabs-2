using System;
using Hovercabs.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Hovercabs.Services
{
    public delegate void OnProfileUpdatedDelegate(Profile profile);
    
    public class ProfileService
    {
        private const string ProfileKey = "profile";
        
        public Profile Profile { get; private set; }
        public OnProfileUpdatedDelegate OnProfileUpdated;
        
        private Action<Profile> _onLoginResult;
        
        public void Login(string username, string password, Action<Profile> onLoginResult)
        {
            _onLoginResult = onLoginResult;
    
            var data = PlayerPrefs.GetString(ProfileKey, Resources.Load<TextAsset>("demo-profile").text);
            Profile = new Profile(data);
            SaveProfile();
            
            _onLoginResult?.Invoke(Profile);
        }

        public void SaveProfile()
        {
            PlayerPrefs.SetString(ProfileKey, JsonConvert.SerializeObject(Profile.ToDto()));
            OnProfileUpdated?.Invoke(Profile);
        }
    }
}