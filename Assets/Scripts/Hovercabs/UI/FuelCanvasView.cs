using UnityEngine;
using UnityEngine.UI;

namespace Hovercabs.UI
{
    public class FuelCanvasView : MonoBehaviour
    {
        [SerializeField] private Image fillIndicator;
        
        public void SetData(float fuel)
        {
            fillIndicator.fillAmount = (fuel / 100f);
        }
    }
}
