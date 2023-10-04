using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Pooling;

namespace Skyroad.Environment.Road.Pooling
{
    public class RoadSegmentPool : MonoScenePool<RoadSegment>, IRoadSegmentSpawner
    {
        public RoadSegment GetNewSegment()
        {
            return Get();
        }

        public float GetSegmentWidth()
        {
            return ((RoadSegment)pooledPrefab).SegmentWidth;
        }
    }
}