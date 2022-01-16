using System.Collections.Generic;
using UnityEngine;

namespace Hovercabs.Configurations.Tracks
{
    
    [CreateAssetMenu(fileName = "TrackConfig", menuName = "Hovercabs/Tracks/TrackConfiguration", order = 2)]
    public class TrackConfig : ScriptableObject
    {
        public string id;
        public GameObject prefab;
    }
}
