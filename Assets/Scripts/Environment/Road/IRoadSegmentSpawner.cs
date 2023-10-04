using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Environment.Road
{
    public interface IRoadSegmentSpawner
    {
        public RoadSegment GetNewSegment();

        public float GetSegmentWidth();
    }
}