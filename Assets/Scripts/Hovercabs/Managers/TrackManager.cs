using System;
using System.Collections.Generic;
using Hovercabs.Controllers;
using UnityEngine;

namespace Hovercabs.Managers
{
    public class TrackManager : MonoBehaviour
    {
        private const int TrackSize = 5;
        private int _currentTrackNum = 0;
        private Queue<GameObject> _currentTracks;

        public void Init()
        {
            DestroyAllTracks();

            _currentTrackNum = 0;
            _currentTracks = new Queue<GameObject>();

            for (var i = 0; i < 5; i++)
            {
                SpawnTrack();
            }
        }

        private void RemoveOldestTrack()
        {
            var entry = _currentTracks.Dequeue();
            Destroy(entry);
        }

        private void OnTrackExit(GameObject track)
        {
            RemoveOldestTrack();
            SpawnTrack();
        }

        private void SpawnTrack()
        {
            var go = Instantiate(Resources.Load<GameObject>($"Hovercabs/3D/Roads/Prefabs/Roads/Road/pf_tr3k3"), transform, true);
            go.AddComponent<TrackController>().Init(3f, OnTrackExit, out var trackSize);

            go.transform.position = new Vector3(6, 0, _currentTrackNum * trackSize.z);
            go.transform.name = $"Track_{_currentTrackNum}";
            _currentTracks.Enqueue(go);
            _currentTrackNum++;
        }

        private void DestroyAllTracks()
        {
            if (_currentTracks == null) return;

            var enqueuedItems = _currentTracks.Count;

            for (var i = 0; i < enqueuedItems; i++)
            {
                var t = _currentTracks.Dequeue();
                GameObject.Destroy(t);
            }
        }
    }
}
