using System;
using System.Collections.Generic;
using Hovercabs.Controllers;
using Hovercabs.Models;
using Hovercabs.Pools;
using Hovercabs.Services;
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

        private TrackService _trackService;
        
        public void Init(TrackService trackService, Track trackData)
        {
            _trackService = trackService;
            
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
            
            _trackService.InitLevelTracks();
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
                    Id = _trackService.GetTrackId(),
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
