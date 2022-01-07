using Hovercabs.Enums.UI;
using TMPro;
using UnityEngine;

namespace Hovercabs.UI
{
    public class PlayButtonCanvasView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtCaption;
        
        public void Init(PlayButtonMode mode)
        {
            switch (mode)
            {
                case PlayButtonMode.Play:
                    txtCaption.text = "PLAY";
                    break;
                case PlayButtonMode.Purchase:
                    txtCaption.text = $"UNLOCK";
                    break;
                case PlayButtonMode.ComingSoon:
                    txtCaption.text = "Coming Soon";
                    break;
            }
        }
    }
}
