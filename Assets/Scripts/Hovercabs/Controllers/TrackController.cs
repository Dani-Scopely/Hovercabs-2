using System;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class TrackController : MonoBehaviour
    {
        private Action<GameObject> _onTrackExit;
        private GameObject _other;
        private float _destroyDistance;
        private bool _markToDestroy = false;

        public void Init(float destroyDistance, Action<GameObject> onTrackExit, out Vector3 size)
        {
            _onTrackExit = onTrackExit;
            _destroyDistance = destroyDistance;
            _markToDestroy = false;

            size = GetComponent<MeshRenderer>().bounds.size;
        }

        private void OnTriggerExit(Collider other)
        {
            _markToDestroy = true;
            _other = other.gameObject;
            NotifyTrackExit();
        }

        private void NotifyTrackExit()
        {
            if (_other == null) return;

            
            if (_markToDestroy && Vector3.Distance(_other.transform.position, transform.position) > _destroyDistance)
            {
                _onTrackExit?.Invoke(gameObject);
            }
        }
    }
}
