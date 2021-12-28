using UnityEngine;
using UnityEngine.UI;

namespace Hovercabs.Controllers
{
    public class VehicleEmblemController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer emblem;
        [SerializeField] private Camera _camera;
        
        public void SetEmblem(Sprite emblemSprite)
        {
            emblem.sprite = emblemSprite;
            gameObject.SetActive(true);
            
            transform.LookAt(_camera.transform.position, Vector3.up);
        }
    }
}
