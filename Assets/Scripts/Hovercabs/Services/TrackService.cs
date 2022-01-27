using System.Collections.Generic;
using Hovercabs.Models;

namespace Hovercabs.Services
{
    public class TrackService
    {
        private Queue<string> _preloadedTrackIndexes;
        
        public TrackService()
        {
            
        }

        public void InitLevelTracks(Level level)
        {
            _preloadedTrackIndexes?.Clear();
            _preloadedTrackIndexes = new Queue<string>();

            PreloadInitialTrackArea(level);
            
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

        private void PreloadInitialTrackArea(Level level)
        {
            foreach (var entry in level.initialTracks)
            {
                _preloadedTrackIndexes.Enqueue(entry);
            }
        }
        
        public string GetTrackId()
        {
            return _preloadedTrackIndexes.Dequeue();
        }
    }
}