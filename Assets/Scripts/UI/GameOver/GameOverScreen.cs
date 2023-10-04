using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skyroad.Environment.Level;


namespace Skyroad.UI.GameOver
{
    public class GameOverScreen : MonoBehaviour, ILevelRestartCommand
    {
        [SerializeField] private Button _btnRestart;

        public event Action OnRestartCommand;

        // Start is called before the first frame update
        void Awake()
        {
            _btnRestart.onClick.RemoveAllListeners();
            _btnRestart.onClick.AddListener(OnRestartClick);
        }

        void OnRestartClick()
        {
            OnRestartCommand?.Invoke();
        }
    }
}