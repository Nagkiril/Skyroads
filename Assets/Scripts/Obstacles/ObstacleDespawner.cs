using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Collision;

namespace Skyroad.Obstacles
{
    public class ObstacleDespawner : MonoBehaviour
    {
        [SerializeField] private ObstacleCollisionZone _obstacleCollision;

        void Awake()
        {
            //We're not going to usubscribe because obstacleCollision is a tight component of a despawner; no abstraction here
            _obstacleCollision.OnEnter += OnObstacleEntered;
        }

        void OnObstacleEntered(Obstacle target)
        {
            target.Dispose();
        }
    }
}