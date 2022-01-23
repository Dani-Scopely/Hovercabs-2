using TMPro;
using UnityEngine;

namespace Hovercabs.UI
{
    public class XenitsCanvasView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void SetData(string xenits)
        {
            text.text = xenits;
        }
    }
}
