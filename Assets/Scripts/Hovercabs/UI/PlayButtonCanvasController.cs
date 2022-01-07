using System;
using Hovercabs.Enums.UI;
using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.UI
{
    [RequireComponent(typeof(PlayButtonCanvasView))]
    public class PlayButtonCanvasController : MonoBehaviour
    {
        private PlayButtonCanvasView _view;
        
        private void Awake()
        {
            _view = GetComponent<PlayButtonCanvasView>();
        }

        public void Init(Vehicle vehicle)
        {
            _view.Init(vehicle.IsAvailable ? PlayButtonMode.Play : PlayButtonMode.Purchase);
        }
    }
}
