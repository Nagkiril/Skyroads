using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reusable.Save;
using Zenject;
using Skyroad.Environment.Level;

namespace Skyroad.Score
{
    public class ScoreManager : MonoBehaviour, IScoreProvider, IHighscoreProvider
    {
        //We're serializing those just so we can see them for debug purposes in inspector; this is not necessary for the final product
        [SerializeField] private int _currentScore;
        [SerializeField] private int _bestScore;
        [SerializeField] private float _scoreTickDuration = 1;
        private float _untilNextTick;

        private IScoreTickSizeProvider _scorePerTick;
        private ILevelEventProvider _levelEvents;

        public event Action<int> OnScoreChanged;

        [Inject]
        private void Inject(IScoreTickSizeProvider scoreTickSize, ILevelEventProvider levelEvents)
        {
            _scorePerTick = scoreTickSize;
            _levelEvents = levelEvents;
        }


        private void Awake()
        {
            //SaveManager could also be injected, however I try to keep reusables working with as few external packages as possible
            _bestScore = SaveManager.Load<int>(nameof(_bestScore));
            _levelEvents.OnLevelStarted += StartScoreCount;
            _levelEvents.OnLevelEnded += StopScoreCount;
        }

        private void OnDestroy()
        {
            _levelEvents.OnLevelStarted -= StartScoreCount;
            _levelEvents.OnLevelEnded -= StopScoreCount;
        }

        private void StartScoreCount()
        {
            _untilNextTick = _scoreTickDuration;
        }

        private void StopScoreCount()
        {
            _untilNextTick = 0f;
        }

        private void FixedUpdate()
        {
            if (_untilNextTick > 0)
            {
                _untilNextTick -= Time.deltaTime;
                if (_untilNextTick <= 0)
                {
                    AddScore(_scorePerTick.GetScoreTickSize());
                    StartScoreCount();
                }
            }
        }

        public void AddScore(int score)
        {
            _currentScore += score;
            if (_currentScore > _bestScore)
            {
                _bestScore = _currentScore;
                SaveManager.Save(nameof(_bestScore), _bestScore);
            }
            OnScoreChanged?.Invoke(_currentScore);
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        public int GetHighScore()
        {
            return _bestScore;
        }
    }
}