using System;
using UnityEngine;

namespace Hovercabs.UI
{
    [RequireComponent(typeof(DistanceCanvasView))]
    public class DistanceCanvasController : MonoBehaviour
    {
        private DistanceCanvasView _view;
        private float _defaultDistance = 0f;
        
        private void Awake()
        {
            _view = GetComponent<DistanceCanvasView>();
        }

        public void Init()
        {
            _view.Render(_defaultDistance);
        }
        
        public void SetDistance(float distance)
        {
            _view.Render(distance);
        }
    }
}
