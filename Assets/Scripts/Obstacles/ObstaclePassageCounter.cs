using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Collision;

namespace Skyroad.Obstacles
{
    public class ObstaclePassageCounter : MonoBehaviour, IObstaclePassProvider
    {
        [SerializeField] private ObstacleCollisionZone _obstacleCollision;
        private int _obstaclesPassed;

        public int PassedObstaclesCounter => _obstaclesPassed;

        public event Action<int> OnObstaclePassed;


        void Awake()
        {
            //We're not going to usubscribe because obstacleCollision is a tight component of a despawner; no abstraction here
            _obstacleCollision.OnEnter += OnObstacleEntered;
        }

        void OnObstacleEntered(Obstacle target)
        {
            _obstaclesPassed++;
            OnObstaclePassed?.Invoke(PassedObstaclesCounter);
        }
    }
}