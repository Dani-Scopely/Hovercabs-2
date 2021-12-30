using System;
using Hovercabs.Controllers.Popups;
using Hovercabs.Enums;
using Hovercabs.Events;
using Hovercabs.Managers;
using Hovercabs.Models;
using Hovercabs.Observers;
using Hovercabs.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Hovercabs.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private VehiclesService _vehiclesService;
        private ProfileService _profileService;
        private int _currentVehicleIndex = 0;
        private int _maxVehicles = 0;

        [SerializeField] private MainMenuPopupsController _mainMenuPopupsController;
        [SerializeField] private ShowcaseController showcaseController;
        [SerializeField] private Button btnLeftArrow;
        [SerializeField] private Button btnRightArrow;
        [SerializeField] private Button btnPlayMultiplayer;
        [SerializeField] private Button btnPlay;
        [SerializeField] private Button btnSettings;
        [SerializeField] private AudioSource audioArrows;
        [SerializeField] private AudioSource audioBackground;
        [SerializeField] private XenitsObserver xenitsObserver;

        private void Awake()
        {
            btnLeftArrow.onClick.AddListener(OnLeftArrowClick);
            btnRightArrow.onClick.AddListener(OnRightArrowClick);
            btnPlay.onClick.AddListener(OnPlay);
            btnPlayMultiplayer.onClick.AddListener(OnPlayMultiplayer);
            btnSettings.onClick.AddListener(OnSettings);
        }

        public void Init(ProfileService profileService, VehiclesService vehiclesService)
        {
            audioBackground.Play();

            _profileService = profileService;
            _vehiclesService = vehiclesService;
            _maxVehicles = _vehiclesService.VehiclesCount;
            
            SetObservers();
            SetVehicle();
        }

        private void OnLeftArrowClick()
        {
            audioArrows.Play();
            
            _currentVehicleIndex--;
            if (_currentVehicleIndex < 0) _currentVehicleIndex = _maxVehicles - 1;
            SetVehicle();
        }

        private void OnRightArrowClick()
        {
            audioArrows.Play();
            
            _currentVehicleIndex++;
            _currentVehicleIndex %= _maxVehicles;
            SetVehicle();
        }

        private void OnPlay()
        {
            Debug.Log("Lets go to play solo!");
        }
        
        private void OnPlayMultiplayer()
        {
            Debug.Log("Lets go to play multiplayer!");   
        }

        private void OnSettings()
        {
            _mainMenuPopupsController.Show(PopupType.Settings);
        }

        private void SetVehicle()
        {
            showcaseController.SetVehicle(_vehiclesService.GetVehicleByIndex(_currentVehicleIndex));
        }

        private void SetObservers()
        {
            xenitsObserver.Init(_profileService);
        }

        private void OnDestroy()
        {
            btnLeftArrow.onClick.RemoveAllListeners();
            btnRightArrow.onClick.RemoveAllListeners();
            btnPlay.onClick.RemoveAllListeners();
            btnPlayMultiplayer.onClick.RemoveAllListeners();
            audioArrows.Stop();
            audioBackground.Stop();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _profileService.Profile.Level += 1;
                _profileService.Profile.Xenits += 400;
                _profileService.SaveProfile();
            }
        }
    }
}
