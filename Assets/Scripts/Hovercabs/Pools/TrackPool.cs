using System;
using System.Collections.Generic;
using System.Linq;
using Hovercabs.Configurations.Tracks;
using Hovercabs.Controllers;
using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.Pools
{
    public class TrackPool : MonoBehaviour
    {
        [SerializeField] private TracksConfig tracksConfig;
        [SerializeField] private bool isPreInitialized = false;
        [SerializeField] private int sizePrefabsChunk = 1;
        [SerializeField] private int bufferPrefabsSize = 2;
        
        private Dictionary<string, Queue<GameObject>> _tracks;

        //private Queue<GameObject> _usedTracks = new Queue<GameObject>();
        
        private void Awake()
        {
            if (isPreInitialized) Init();
        }

        public void Init()
        {
            _tracks = new Dictionary<string, Queue<GameObject>>();

            for (var i = 0; i < tracksConfig.tracksConfig.Count; i++)
            {
                var p = $"{tracksConfig.tracksPrefabPath}/pf_{tracksConfig.tracksConfig[i].id}";
                 
                _tracks.Add(tracksConfig.tracksConfig[i].id, new Queue<GameObject>());
                
                InstantiatePrefabs(tracksConfig.tracksConfig[i]);
            }
        }

        private void InstantiatePrefabs(TrackConfig trackConfig)
        {
            var tcfg = tracksConfig.tracksConfig.First(p => p.id == trackConfig.id);

            for (var j = 0; j < sizePrefabsChunk; j++)
            {
                var ob = Instantiate(trackConfig.prefab, transform, false);
                ob.name = trackConfig.id;
                ob.SetActive(false);
                _tracks[tcfg.id].Enqueue(ob);
            }
        }
        
        public GameObject Get(Track track)
        {
            var listOfTracks = _tracks[track.Id];
            
            if (listOfTracks.Count < bufferPrefabsSize)
            {
                var tcfg = tracksConfig.tracksConfig.First(p => p.id == track.Id);
                InstantiatePrefabs(tcfg);
            }

            return _tracks[track.Id].Dequeue();
        }

        public void Recycle(TrackController trackController)
        {
            var trackData = trackController.TrackData;
            trackController.gameObject.SetActive(false);
            trackController.transform.position = Vector3.zero;
            var go = trackController.gameObject;
            _tracks[trackData.Id].Enqueue(go);
            go.transform.SetParent(transform);
            Destroy(trackController);
        }
        
        private void Update()
        {
            /*
            if (Input.GetKeyUp(KeyCode.A))
            {
                var g = Get(new Track {Id = "tr2k3"});
                g.name = "tr2k3";
                g.SetActive(true);
                _usedTracks.Enqueue(g);
            }

            if (Input.GetKeyUp(KeyCode.B))
            {
                Recycle(new Track { Id = "tr2k3"});
            }
            */
        }
    }
}