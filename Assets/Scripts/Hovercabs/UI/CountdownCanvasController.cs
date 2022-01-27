using System;
using UnityEngine;

namespace Hovercabs.UI
{
    [RequireComponent(typeof(CountdownCanvasView))]
    public class CountdownCanvasController : MonoBehaviour
    {
        private CountdownCanvasView _view;
        private float _initTime;
        private int _from;
        private Action _onCountdownEnded;
        private bool _isEnded;

        public void Init(Action onCountdownEnded)
        {
            _view = GetComponent<CountdownCanvasView>();
            _initTime = Time.realtimeSinceStartup;
            _onCountdownEnded = onCountdownEnded;
            _isEnded = false;
        }
        
        public void SetData(int from)
        {
            _from = from;
        }

        private void Update()
        {
            if (_isEnded) return;
            
            if (_from == -1)
            {
                _onCountdownEnded.Invoke();
                gameObject.SetActive(false);
                return;
            }

            if (!(Time.realtimeSinceStartup > _initTime + 1)) return;
            
            _initTime = Time.realtimeSinceStartup;
            _from--;
            _view.SetData(_from);
        }
    }
}
