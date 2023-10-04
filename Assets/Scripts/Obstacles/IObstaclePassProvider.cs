using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Obstacles
{
    public interface IObstaclePassProvider
    {
        public event Action<int> OnObstaclePassed;

        public int PassedObstaclesCounter { get; }
    }
}