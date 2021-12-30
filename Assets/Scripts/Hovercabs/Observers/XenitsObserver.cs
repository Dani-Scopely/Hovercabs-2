using System;
using Hovercabs.Enums;
using Hovercabs.Models;
using Hovercabs.Services;
using TMPro;
using UnityEngine;
using Utils;

namespace Hovercabs.Observers
{
    public class XenitsObserver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtXenits;
        private ProfileService _profileService;
        
        public void Init(ProfileService profileService)
        {
            _profileService = profileService;
            _profileService.OnProfileUpdated += OnProfileUpdated;
            OnProfileUpdated(_profileService.Profile);
        }

        private void OnProfileUpdated(Profile profile)
        {
            txtXenits.text = $"{profile.Xenits}";
        }

        private void OnDestroy()
        {
            _profileService.OnProfileUpdated -= OnProfileUpdated;
        }
    }
}