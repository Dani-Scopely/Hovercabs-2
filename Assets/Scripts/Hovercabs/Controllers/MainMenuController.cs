using System;
using Hovercabs.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Hovercabs.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private VehiclesService _vehiclesService;
        private int _currentVehicleIndex = 0;
        private int _maxVehicles = 0;

        [SerializeField] private ShowcaseController showcaseController;
        [SerializeField] private Button btnLeftArrow;
        [SerializeField] private Button btnRightArrow;
        [SerializeField] private Button btnPlayMultiplayer;
        [SerializeField] private Button btnPlay;
        [SerializeField] private AudioSource audioArrows;
        [SerializeField] private AudioSource audioBackground;
        
        private void Awake()
        {
            btnLeftArrow.onClick.AddListener(OnLeftArrowClick);
            btnRightArrow.onClick.AddListener(OnRightArrowClick);
            btnPlay.onClick.AddListener(OnPlay);
            btnPlayMultiplayer.onClick.AddListener(OnPlayMultiplayer);
        }

        public void Init(VehiclesService vehiclesService)
        {
            audioBackground.Play();
            
            _vehiclesService = vehiclesService;
            _maxVehicles = _vehiclesService.VehiclesCount;
     
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

        private void SetVehicle()
        {
            showcaseController.SetVehicle(_vehiclesService.GetVehicleByIndex(_currentVehicleIndex));
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
    }
}
