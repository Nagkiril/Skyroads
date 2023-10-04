using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Obstacles
{
    public interface IObstacleSpawner
    {
        public Obstacle GetObstacle();
    }
}