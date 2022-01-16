using UnityEngine;

namespace Hovercabs.Controllers
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }
        
        void Update()
        {
            var tpos = _target.transform.position;
            var fpos = transform.position;
            fpos.z = tpos.z;
            fpos.x = tpos.x;
            fpos.y = tpos.y;
            transform.position = fpos;
            transform.position += new Vector3(0, 4, -3.5f);
        }
    }
}
