using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Skyroad.Obstacles
{
    public class TimedObstacleSpawner : MonoBehaviour
    {

        //We're injecting the spawner because we expect it'll be a separate service that can be coupled to external elements (pools on the scene in current implementation)
        private IObstacleSpawner _obstacleSpawner;
        //Same thing with the timer (coupled to score in current implementation)
        private IObstacleTimer _obstacleTimer;
        //Same with the IObstacleSpray (coupled to road width in current implementation)
        private IObstacleSpray _obstacleSpray;


        //Please refer to Scripts\Environment\Road\InfiniteRoad.cs for commentary around inject method here 
        [Inject]
        private void Inject(IObstacleSpawner spawnProvider, IObstacleTimer timerProvider, IObstacleSpray sprayProvider)
        {
            _obstacleSpawner = spawnProvider;
            _obstacleTimer = timerProvider;
            _obstacleSpray = sprayProvider;
        }

        private void Awake()
        {
            _obstacleTimer.OnTimerExpiration += OnTimerExpired;
        }

        private void OnDestroy()
        {
            _obstacleTimer.OnTimerExpiration -= OnTimerExpired;
        }


        private void OnTimerExpired()
        {
            var newObstacle = _obstacleSpawner.GetObstacle();
            var spawnRange = _obstacleSpray.GetSprayRange();

            newObstacle.PlaceInWorld(new Vector3(Random.Range(-1, 1f) * spawnRange, transform.position.y, transform.position.z));
        }
    }
}