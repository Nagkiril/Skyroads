using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Obstacles
{
    public interface IObstacleTimer
    {
        public event Action OnTimerExpiration;
    }
}