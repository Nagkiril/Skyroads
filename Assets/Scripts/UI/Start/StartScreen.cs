using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skyroad.Environment.Level;
using Zenject;

namespace Skyroad.UI.Start
{
    public class StartScreen : MonoBehaviour, ILevelBeginCommand
    {
        public event Action OnBeginCommand;


        private void Update()
        {
            if (Input.anyKey)
            {
                OnBeginCommand?.Invoke();
            }
        }
    }
}