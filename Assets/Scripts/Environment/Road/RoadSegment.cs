using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Player;
using Skyroad.Collision;
using Skyroad.Pooling;

namespace Skyroad.Environment.Road
{
    public sealed class RoadSegment : PoolableBehaviour
    {
        [SerializeField] PlayerCollisionZone ownCollision;
        [SerializeField] float _segmentLength;
        [SerializeField] float _segmentWidth;
        public event Action<RoadSegment> OnSegmentPassed;

        //Lossy scale might be a little slow, so perhaps this should be exposed as a method GetSegmntLength
        //Still, it is probably insignificant enough to even warrant caching
        public float SegmentLength => _segmentLength;
        public float SegmentWidth => _segmentWidth;


        private void Awake()
        {
            ownCollision.OnEnter += OnPlayerShipPass;
        }

        public void OnPlayerShipPass(IPlayer player)
        {
            OnSegmentPassed?.Invoke(this);
        }
    }
}