using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Player.Controls
{
    public abstract class PlayerControls : MonoBehaviour
    {
        public event Action<float> OnSwayChanged;
        public event Action<bool> OnBoostChanged;

        //These can be exposed with a getter later on, if need be
        protected float _currentSway;
        protected bool _currentBoost;

        protected void SetBoost(bool newBoost)
        {
            if (_currentBoost != newBoost)
            {
                _currentBoost = newBoost;
                OnBoostChanged?.Invoke(_currentBoost);
            }
        }

        protected void SetSway(float newSway)
        {
            if (_currentSway != newSway)
            {
                _currentSway = newSway;
                OnSwayChanged?.Invoke(_currentSway);
            }
        }
    }
}