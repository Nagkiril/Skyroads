using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Player.Controls;
using Skyroad.Player.Movement;
using Skyroad.Player.Visuals;

namespace Skyroad.Player
{
    public class PlayerShip : MonoBehaviour, IPlayer
    {
        //Notice how here we're using *abstract* classes, hence we're not truly relying on implementation, but rather on abstraction
        //We're also using abstract classes over interfaces so that Inspector referencing would not break (and also different implementations would very likely share some code.
        [SerializeField] private PlayerControls _controls;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerVisuals _visuals;


        public bool IsBoosted { get; private set; }

        public event Action OnPlayerKilled;
        public event Action OnPlayerBoostChanged;


        // Start is called before the first frame update
        void Start()
        {
            _controls.OnSwayChanged += OnSwayInput;
            _controls.OnBoostChanged += OnBoostInput;
        }

        private void OnSwayInput(float sway)
        {
            _movement.ApplySway(sway);
            _visuals.ApplySway(sway);
        }

        private void OnBoostInput(bool boost)
        {
            _movement.SetBoost(boost);
            IsBoosted = boost;
            OnPlayerBoostChanged?.Invoke();
        }


        public void Kill()
        {
            gameObject.SetActive(false);
            OnPlayerKilled?.Invoke();
        }
    }
}