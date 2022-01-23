using System.Collections.Generic;

namespace Hovercabs.Services
{
    public class TrackService
    {
        private Queue<string> _preloadedTrackIndexes;
        
        public TrackService()
        {
            
        }

        public void InitLevelTracks()
        {
            _preloadedTrackIndexes?.Clear();
            _preloadedTrackIndexes = new Queue<string>();

            var lastTaxiStop = "tr_taxi_off1kright";
            
            for (var i = 0; i < 100; i++)
            {
                if (i % 10 == 0)
                {
                    lastTaxiStop = lastTaxiStop.Contains("off") ? "tr_taxi_on1kleft" : "tr_taxi_off1kleft";
                    _preloadedTrackIndexes.Enqueue(lastTaxiStop);
                    continue;
                }
                
                _preloadedTrackIndexes.Enqueue("tr1k3");
            }
        }

        public string GetTrackId()
        {
            return _preloadedTrackIndexes.Dequeue();
        }
    }
}