using System;
using Hovercabs.Controllers.Popups.Base;
using Hovercabs.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Hovercabs.Controllers.Popups
{
    public class MainMenuPopupsController : MonoBehaviour
    {
        [SerializeField] private PopupController[] popupControllers;
        [SerializeField] private Image background;

        private void Awake()
        {
            background.enabled = false;
        }

        public void Show(PopupType popupType)
        {
            foreach (var t in popupControllers)
            {
                t.Show(popupType, OnBack);
                background.enabled = true;
            }
        }

        private void OnBack()
        {
            background.enabled = false;
        }
    }
}
