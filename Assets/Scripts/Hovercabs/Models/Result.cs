using System;

namespace Hovercabs.Models
{
    [Serializable]
    public class Result
    {
        public int Xenits;
        public float Distance;
        public float RemainingFuel;
        public int PassengersDelivered;
        public int PassengersFailed;

        public Result()
        {
            Xenits = 0;
            Distance = 0f;
            RemainingFuel = 0f;
            PassengersDelivered = 0;
            PassengersFailed = 0;
        }
    }
}