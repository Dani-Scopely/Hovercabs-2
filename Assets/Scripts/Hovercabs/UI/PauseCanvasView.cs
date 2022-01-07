using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hovercabs.UI
{
    public class PauseCanvasView : MonoBehaviour
    {
        public Action OnQuitClicked { get; set; }
        public Action OnContinueClicked { get; set; }
        
        private PauseCanvasController _controller;
        
        [SerializeField] private Button btnQuit;
        [SerializeField] private Button btnContinue;

        private void Awake()
        {
            btnQuit.onClick.AddListener(() => OnQuitClicked());
            btnContinue.onClick.AddListener(() => OnContinueClicked());
        }

        public void Init(PauseCanvasController controller)
        {
            _controller = controller;
        }

        private void OnDestroy()
        {
            btnQuit.onClick.RemoveAllListeners();
            btnContinue.onClick.RemoveAllListeners();
        }
    }
}
