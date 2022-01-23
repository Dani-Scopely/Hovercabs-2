using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class PassengerController : MonoBehaviour
    {
        private Passenger _passenger;
        
        [SerializeField] private Transform portrait;

        public void Init(Passenger passenger)
        {
            _passenger = passenger;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        
        void Update()
        {
            portrait.LookAt(Camera.main.transform);
        }

        public int Collect()
        {
            return _passenger.Xenits;
        }
    }
}
