using System;
using Hovercabs.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Hovercabs.Controllers
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private Button btnBack;
        [SerializeField] private TrackManager trackManager;
        
        private Action _onBack;
        
        private void Awake()
        {
            btnBack.onClick.AddListener(OnBackClick);
        }

        public void Init(Action onBack)
        {
            _onBack = onBack;
            trackManager.Init();
        }
        
        private void OnDestroy()
        {
            btnBack.onClick.RemoveAllListeners();
        }

        private void OnBackClick()
        {
            _onBack?.Invoke();
        }
    }
}