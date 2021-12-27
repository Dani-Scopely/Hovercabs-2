using UnityEngine;
using UnityEngine.UI;

namespace Hovercabs.Controllers
{
    public class VehicleEmblemController : MonoBehaviour
    {
        [SerializeField] private Image emblem;
        
        public void SetEmblem(Sprite emblemSprite)
        {
            emblem.sprite = emblemSprite;
            gameObject.SetActive(true);
        }
    }
}
