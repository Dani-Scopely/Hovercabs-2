using UnityEngine;

namespace Hovercabs.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        // Update is called once per frame
        void Update()
        {
            var tpos = target.transform.position;
            var fpos = transform.position;
            fpos.z = tpos.z;
            fpos.y = tpos.y;
            transform.position = fpos;
            transform.position += new Vector3(0, 4, -3.5f);
        }
    }
}
