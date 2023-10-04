using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Pooling;

namespace Skyroad.Obstacles.Pooling
{
    public class ObstaclesPool : MonoScenePool<Obstacle>, IObstacleSpawner
    {
        Obstacle IObstacleSpawner.GetObstacle()
        {
            return Get();
        }
    }
}