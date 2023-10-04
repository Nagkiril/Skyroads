using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Score;
using Skyroad.Environment.Level;
using Zenject;

namespace Skyroad.Obstacles.Timers
{
    public sealed class ScoreObstacleTimer : MonoBehaviour, IObstacleTimer
    {
        //Those settings could be put into some form of a scriptable object, however, they're expected to be used strictly in this class
        [SerializeField] private float _initialTimer;
        [SerializeField] private float _minimalTimer;
        [SerializeField] private float _timePerStep;
        [SerializeField] private int _scoreStep;

        private float _untilNextObstacle;
        private ILevelEventProvider _levelEvents;
        private IScoreProvider _score;

        public event Action OnTimerExpiration;

        [Inject]
        private void Inject(ILevelEventProvider level, IScoreProvider score)
        {
            _levelEvents = level;
            _score = score;
        }

        private void Awake()
        {
            _levelEvents.OnLevelStarted += RestartTimer;
            _levelEvents.OnLevelEnded += StopTimer;
        }

        private void OnDestroy()
        {
            _levelEvents.OnLevelStarted -= RestartTimer;
            _levelEvents.OnLevelEnded -= StopTimer;
        }

        private void FixedUpdate()
        {
            if (_untilNextObstacle > 0)
            {
                _untilNextObstacle -= Time.deltaTime;
                if (_untilNextObstacle <= 0)
                {
                    OnTimerExpiration?.Invoke();
                    RestartTimer();
                }
            }
        }

        private void RestartTimer()
        {
            _untilNextObstacle = GetTimerDuration();
        }

        private void StopTimer()
        {
            _untilNextObstacle = 0;
        }

        private float GetTimerDuration()
        {
            var scoreTimer = _initialTimer - (_score.GetCurrentScore() / _scoreStep) * _timePerStep;
            return Mathf.Max(_minimalTimer, scoreTimer);
        }

    }
}