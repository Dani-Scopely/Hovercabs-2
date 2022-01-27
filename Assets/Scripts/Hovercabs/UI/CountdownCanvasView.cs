using TMPro;
using UnityEngine;

namespace Hovercabs.UI
{
    public class CountdownCanvasView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        public void SetData(int num)
        {
            text.text = num != 0 ? $"{num}" : "GO!";
        }
    }
}
