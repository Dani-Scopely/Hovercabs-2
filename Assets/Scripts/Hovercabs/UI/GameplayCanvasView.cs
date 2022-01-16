using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hovercabs.UI
{
    public class GameplayCanvasView : MonoBehaviour
    {
        public Action OnPauseClick { get; set; }

        private GameplayCanvasController _controller;

        [SerializeField] private Button btnPause;
        
        public void Init(GameplayCanvasController controller)
        {
            _controller = controller;
            btnPause.onClick.AddListener( () => OnPauseClick());    
        }

        private void OnDestroy()
        {
            btnPause.onClick.RemoveAllListeners();
        }
    }
}
