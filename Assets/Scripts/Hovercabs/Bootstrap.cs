using System;
using Hovercabs.Managers;
using Hovercabs.Services;
using UnityEngine;

namespace Hovercabs
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        private ProfileService _profileService;
        
        private void Awake()
        {
            _profileService = new ProfileService();
        }

        private void Start()
        {
            gameManager.Init(_profileService);    
        }
    }
}
