using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Skyroad.Player;

namespace Skyroad.Environment.Level
{
    public class LevelManager : MonoBehaviour, ILevelEventProvider
    {
        public event Action OnLevelStarted;
        public event Action OnLevelEnded;

        private IPlayer _player;
        private ILevelBeginCommand _beginCommand;
        private ILevelRestartCommand _restartCommand;

        private bool _isLevelInProgress;

        [Inject]
        private void Inject(IPlayer player, ILevelBeginCommand beginCommandSource, ILevelRestartCommand restartCommandSource)
        {
            _player = player;
            _beginCommand = beginCommandSource;
            _restartCommand = restartCommandSource;
        }

        private void Awake()
        {
            _player.OnPlayerKilled += EndLevel;
            _beginCommand.OnBeginCommand += StartLevel;
            _restartCommand.OnRestartCommand += RestartLevel;
        }

        private void OnDestroy()
        {
            _player.OnPlayerKilled -= EndLevel;
            _beginCommand.OnBeginCommand -= StartLevel;
            _restartCommand.OnRestartCommand -= RestartLevel;
        }

        private void StartLevel()
        {
            if (!_isLevelInProgress)
            {
                _isLevelInProgress = true;
                OnLevelStarted?.Invoke();
            }
        }

        private void EndLevel()
        {
            if (_isLevelInProgress)
            {
                _isLevelInProgress = false;
                OnLevelEnded?.Invoke();
            }
        }

        public void RestartLevel()
        {
            //We're simply reloading the scene on restart, this can easily be adjusted as per Game Designer's wishes
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}