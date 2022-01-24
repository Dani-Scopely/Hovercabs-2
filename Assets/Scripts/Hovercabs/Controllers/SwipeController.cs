using UnityEngine;

namespace Hovercabs.Controllers
{
    public class SwipeController : MonoBehaviour
    {
        private bool _tap;
        private bool _isDragging = false;
        
        void Update()
        {
            _tap = SwipeLeft = SwipeRight = SwipeUp = SwipeDown = false;
            ProcessInput();
        }

        private void Reset()
        {
            StartTouch = SwipeDelta = Vector2.zero;
            _isDragging = false;
        }

        public Vector2 SwipeDelta { get; private set; }
        public Vector2 StartTouch { get; private set; }
        public bool SwipeLeft { get; private set; }
        public bool SwipeRight { get; private set; }
        public bool SwipeUp { get; private set; }
        public bool SwipeDown { get; private set; }

        private void ProcessInput()
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            ProcessStandaloneInput();
            #endif
            #if UNITY_ANDROID && !UNITY_EDITOR
            ProcessMobileInput();
            #endif
            
            CalculateDistance();
            
            CalculateDeadZone();
        }

        private void ProcessStandaloneInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _tap = true;
                _isDragging = true;
                StartTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                _isDragging = false;
                Reset();
            }
        }

        private void ProcessMobileInput()
        {
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _tap = true;
                    _isDragging = true;
                    StartTouch = Input.touches[0].position;
                }else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    _isDragging = false;
                    Reset();
                }
            }
        }

        private void CalculateDeadZone()
        {
            if (!(SwipeDelta.magnitude > 125)) return;
            
            var x = SwipeDelta.x;
            var y = SwipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    SwipeLeft = true;
                }
                else
                {
                    SwipeRight = true;
                }     
                
            }
            else
            {
                if (y < 0)
                {
                    SwipeDown = true;
                }
                else
                {
                    SwipeUp = true;
                }
            }
            
            Reset();
        }
        
        private void CalculateDistance()
        {
            SwipeDelta = Vector2.zero;

            if (_isDragging)
            {
                #if UNITY_EDITOR || UNITY_STANDALONE
                CalculateDistanceStandalone();
                #endif
                
                #if UNITY_ANDROID && !UNITY_EDITOR
                CalculateDistanceMobile();
                #endif
            }
        }

        private void CalculateDistanceStandalone()
        {
            if (Input.GetMouseButton(0))
            {
                SwipeDelta = (Vector2) Input.mousePosition - StartTouch;
            }  
        }
        
        private void CalculateDistanceMobile()
        {
            if (Input.touches.Length > 0)
            {
                SwipeDelta = Input.touches[0].position - StartTouch;
            }
        }

        
    }
}
