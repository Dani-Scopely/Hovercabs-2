using System;
using UnityEngine;

namespace Hovercabs.UI
{
    [RequireComponent(typeof(FuelCanvasView))]
    public class FuelCanvasController : MonoBehaviour
    {
        private FuelCanvasView _view;
        
        private void Awake()
        {
            _view = GetComponent<FuelCanvasView>();
        }

        public void Init()
        {
            _view.SetData(100f);
        }

        public void SetData(float fuel)
        {
            _view.SetData(fuel);
        }
    }
}
