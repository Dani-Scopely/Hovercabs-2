using System.Collections.Generic;
using UnityEngine;

namespace Hovercabs.Configurations.Tracks
{
    [CreateAssetMenu(fileName = "TracksConfig", menuName = "Hovercabs/Tracks/TracksConfiguration", order = 1)]
    public class TracksConfig : ScriptableObject
    {
        public string tracksPrefabPath = "Hovercabs/3D/Roads/Prefabs/Roads";
        public List<TrackConfig> tracksConfig = new List<TrackConfig>();
    }
}