using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Skyroad.Environment.Level
{
    public class LevelPlaytimeTimer : MonoBehaviour, IPlaytimeProvider
    {
        private bool _isCounting;
        private float _playtime;

        public event Action<float> OnPlaytimeChanged;

        ILevelEventProvider _levelEvents;

        [Inject]
        private void Inject(ILevelEventProvider levelEvents)
        {
            _levelEvents = levelEvents;
        }

        private void Awake()
        {
            _levelEvents.OnLevelStarted += StartCount;
            _levelEvents.OnLevelEnded += StopCount;
        }

        void OnDestroy()
        {
            _levelEvents.OnLevelStarted -= StartCount;
            _levelEvents.OnLevelEnded -= StopCount;
        }

        private void StartCount()
        {
            _isCounting = true;
        }

        private void StopCount()
        {
            _isCounting = false;
        }

        private void FixedUpdate()
        {
            if (_isCounting)
            {
                _playtime += Time.deltaTime;
                OnPlaytimeChanged?.Invoke(_playtime);
            }
        }

        public float GetPlaytime()
        {
            return _playtime;
        }
    }
}