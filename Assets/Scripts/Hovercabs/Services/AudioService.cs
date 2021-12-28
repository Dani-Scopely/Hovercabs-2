using Hovercabs.Enums;
using Hovercabs.Events;
using UnityEngine;
using Utils;

namespace Hovercabs.Services
{
    public class AudioService
    {
        public void Set(AudioMode audioMode, float value)
        {
            PlayerPrefs.SetFloat($"{audioMode}",value/10f);
            PlayerPrefs.Save();
        }

        public float Get(AudioMode audioMode)
        {
            return PlayerPrefs.GetFloat($"{audioMode}", 1f);
        }
    }
}