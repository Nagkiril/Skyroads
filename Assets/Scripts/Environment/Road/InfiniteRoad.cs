using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Skyroad.Obstacles;

namespace Skyroad.Environment.Road
{
    public class InfiniteRoad : MonoBehaviour, IObstacleSpray, IRoadWidthProvider
    {
        [SerializeField] private int _initialRoadLength = 4;
        private List<RoadSegment> _activeSegments;
        private IRoadSegmentSpawner _segmentSpawner;
        private Vector3 _nextSegmentPosition;

        private void Awake()
        {
            _activeSegments = new List<RoadSegment>();
            _nextSegmentPosition = transform.position;
            for (var i = 0; i < _initialRoadLength; i++)
                PlaceNewSegment();
        }

        //Zenject recommends usage of method injection over field injection;
        //I think public method injection would work great if we called it in a runtime-instantiated object
        [Inject]
        private void Inject(IRoadSegmentSpawner spawnProvider)
        {
            _segmentSpawner = spawnProvider;
        }

        private void SetupSegment(RoadSegment newSegment)
        {
            newSegment.OnSegmentPassed += OnSegmentPass;
            _nextSegmentPosition += transform.forward * newSegment.SegmentLength;
            _activeSegments.Add(newSegment);
        }

        private void OnSegmentPass(RoadSegment segment)
        {
            segment.OnSegmentPassed -= OnSegmentPass;
            segment.Dispose();
            _activeSegments.Remove(segment);
            PlaceNewSegment();
        }

        private void PlaceNewSegment()
        {
            var newSegment = _segmentSpawner.GetNewSegment();
            newSegment.PlaceInWorld(_nextSegmentPosition);
            SetupSegment(newSegment);
        }

        //Please note that obstacle spray range (how much offset obstacles can take when spawning) and road width are NOT necessarily same\similar value, despite current implementation
        //Hence we have different interfaces for them
        public float GetSprayRange()
        {
            return GetRoadWidth() / 2f;
        }

        public float GetRoadWidth()
        {
            return _segmentSpawner.GetSegmentWidth();
        }
    }
}