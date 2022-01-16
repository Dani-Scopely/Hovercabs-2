using System;
using Hovercabs.Controllers.Popups;
using Hovercabs.Enums;
using Hovercabs.Events;
using Hovercabs.Managers;
using Hovercabs.Models;
using Hovercabs.Observers;
using Hovercabs.Services;
using Hovercabs.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace Hovercabs.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private VehiclesService _vehiclesService;
        private ProfileService _profileService;
        private Action _onPlaySingle;
        private Action _onPlayMultiplayer;
        private int _currentVehicleIndex = 0;
        private int _maxVehicles = 0;

        [SerializeField] private MainMenuPopupsController mainMenuPopupsController;
        [SerializeField] private ShowcaseController showcaseController;
        [SerializeField] private PlayButtonCanvasController playButtonCanvasController;
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

        public void Init(ProfileService profileService, VehiclesService vehiclesService, Action onPlaySingle, Action onPlayMultiplayer)
        {
            _onPlaySingle = onPlaySingle;
            _onPlayMultiplayer = onPlayMultiplayer;
            
            audioBackground.Play();

            _profileService = profileService;
            _vehiclesService = vehiclesService;
            _maxVehicles = _vehiclesService.VehiclesCount;
            _currentVehicleIndex = _vehiclesService.GetCurrentVehicleIndex();
            
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
            _onPlaySingle?.Invoke();
        }
        
        private void OnPlayMultiplayer()
        {
            _onPlayMultiplayer?.Invoke();
        }

        private void OnSettings()
        {
            mainMenuPopupsController.Show(PopupType.Settings);
        }

        private void SetVehicle()
        {
            var vehicle = _vehiclesService.GetVehicleByIndex(_currentVehicleIndex);
            vehicle.IsAvailable = _profileService.Profile.Level >= vehicle.Level;
            showcaseController.SetVehicle(vehicle);
            playButtonCanvasController.Init(vehicle);
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
