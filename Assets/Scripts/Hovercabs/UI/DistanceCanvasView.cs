using TMPro;
using UnityEngine;

namespace Hovercabs.UI
{
    public class DistanceCanvasView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtDistance;
        public void Render(float distance)
        {
            txtDistance.text = $"{distance:F1} m";
        }
    }
}
