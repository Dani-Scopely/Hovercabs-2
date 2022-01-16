using System;
using Hovercabs.Enums;
using UnityEngine;

namespace Hovercabs.Controllers.Popups.Base
{
    public abstract class PopupController : MonoBehaviour
    {
        public abstract void Show(PopupType popupType, Action onBack);
    }
}