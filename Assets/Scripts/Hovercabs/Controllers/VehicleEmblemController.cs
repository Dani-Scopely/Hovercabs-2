using Hovercabs.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Hovercabs.Controllers
{
    public class VehicleEmblemController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer emblem;
        [SerializeField] private Camera _camera;
        
        public void SetEmblem(Vehicle vehicle)
        {
            emblem.sprite = vehicle.Emblem;
            emblem.color = vehicle.IsAvailable ? Color.white : Color.black;

            transform.LookAt(_camera.transform.position, Vector3.up);
        }
    }
}
