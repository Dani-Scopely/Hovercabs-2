using System;
using UnityEngine;

namespace Hovercabs.UI
{
    [RequireComponent(typeof(XenitsCanvasView))]
    public class XenitsCanvasController : MonoBehaviour
    {
        private XenitsCanvasView _view;

        private void Awake()
        {
            _view = GetComponent<XenitsCanvasView>();
        }

        public void Init(int xenits)
        {
            _view.SetData(xenits.ToString());            
        }

        public void SetData(int xenits)
        {
            _view.SetData(xenits.ToString());
        }
    }
}
