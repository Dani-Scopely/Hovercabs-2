using System.Collections.Generic;
using Hovercabs.Models;

namespace Hovercabs.Services
{
    public class LevelService
    {
        public Level GetCurrentLevel()
        {
            return new Level
            {
                Id = 1,
                
                initialTracks = new List<string>
                {
                    "tr1k3",
                    "tr1k3",
                    "tr1k3",
                }
            };
        }
    }
}