using System;
using Hovercabs.Enums;
using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class TaxiStopController : MonoBehaviour
    {
        [SerializeField] private TaxiStopType type;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Vehicle"))
            {
                if (type == TaxiStopType.DropOn)
                {
                    other.GetComponent<VehicleController>().DropOnPassenger(new Passenger
                    {
                        Name = "My Passenger",
                        Xenits = 100
                    });       
                    
                    return;
                }
                
                other.GetComponent<VehicleController>().DropOffPassenger();
            }
        }
    }
}
