using System;
using System.Collections.Generic;
using Hovercabs.Controllers;
using Hovercabs.Models;
using Hovercabs.Pools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hovercabs.Managers
{
    public class TrackManager : MonoBehaviour
    {
        private const int TrackSize = 5;
        private int _currentTrackNum = 0;
        private Queue<GameObject> _currentTracks;
        private VehicleController _vehicleController;
        private float _accumulatedTrackSize = 0f;

        [SerializeField] private TrackPool trackPool;
        [SerializeField] private int bufferTrackSize = 10;
        [SerializeField] private float destroyDistanceFactor = 1.5f;

        private Track _initialTrackData;
        private Queue<string> _preloadedTrackIndexes;
        
        public void Init(Track trackData)
        {
            _initialTrackData = trackData;
            
            trackPool.Init();
            
            Reset();

            _currentTrackNum = 0;
            _accumulatedTrackSize = 0f;
            _currentTracks = new Queue<GameObject>();

            for (var i = 0; i < bufferTrackSize; i++)
            {
                SpawnTrack();
            }
        }

        public void Reset()
        {
            DestroyAllTracks();
            
            PreloadTrackConfig();
        }
        
        public void SetVehicleController(VehicleController vehicleController)
        {
            _vehicleController = vehicleController;
        }
        
        private void RemoveOldestTrack()
        {
            var entry = _currentTracks.Dequeue();
            trackPool.Recycle(entry.GetComponent<TrackController>());
        }

        private void OnTrackExit(GameObject track)
        {
            RemoveOldestTrack();
            SpawnTrack();
        }

        private void SpawnTrack(Track track = null)
        {
            var tData = new Track();
            
            if (track == null)
            {
                tData = new Track
                {
                    Id = _preloadedTrackIndexes.Dequeue(),
                    IsStartTrack = false
                };
            }
            else
            {
                tData = _initialTrackData;
            }

            InitializeTrack(tData);
            
            _currentTrackNum++;
        }

        private void PreloadTrackConfig()
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

        private void InitializeTrack(Track trackData)
        {
            var go = trackPool.Get(trackData);
            go.SetActive(true);
            
            go.transform.SetParent(transform);
            
            
            var trackController = go.AddComponent<TrackController>();
            trackController.Init(_vehicleController, OnTrackExit, out var trackSize);
            trackController.TrackData = trackData;
            trackController.DestroyDistance = trackSize.z*destroyDistanceFactor;
            
            go.transform.position = new Vector3(6, 0, trackData.IsStartTrack ? -trackSize.z : _accumulatedTrackSize);
            
            _accumulatedTrackSize += trackSize.z;
            
            _currentTracks.Enqueue(go);

        }
        private void DestroyAllTracks()
        {
            if (_currentTracks == null) return;

            var enqueuedItems = _currentTracks.Count;

            for (var i = 0; i < enqueuedItems; i++)
            {
                var t = _currentTracks.Dequeue();
                trackPool.Recycle(t.GetComponent<TrackController>());
            }
        }
    }
}
