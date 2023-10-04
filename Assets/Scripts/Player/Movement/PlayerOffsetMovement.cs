using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Environment.Level;
using Skyroad.Environment.Road;
using Zenject;

namespace Skyroad.Player.Movement
{
    //Player movement based on road coordinate constraints and offsetting own transform
    public class PlayerOffsetMovement : PlayerMovement
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _regularSpeed;
        [SerializeField] private float _boostedSpeed;
        [SerializeField] private float _swaySensitivity;

        private float _appliedForwardSpeed;
        private float _lastSway;
        private float _maxSwayWidth;

        private ILevelEventProvider _levelEvents;
        private IRoadWidthProvider _roadWidth;

        [Inject]
        private void Inject(ILevelEventProvider levelEvents, IRoadWidthProvider roadWidth)
        {
            _levelEvents = levelEvents;
            _roadWidth = roadWidth;
        }

        private void Awake()
        {
            _maxSwayWidth = _roadWidth.GetRoadWidth() * 0.4f;
            _levelEvents.OnLevelStarted += StartMoving;
        }

        private void OnDestroy()
        {
            _levelEvents.OnLevelStarted -= StartMoving;
        }

        private void Update()
        {
            if (IsMoving())
            {
                Vector3 newPosition = _target.position;
                newPosition += _target.forward * _appliedForwardSpeed * Time.deltaTime;
                if (_lastSway != 0)
                {
                    newPosition += _target.right * _lastSway * _swaySensitivity * Time.deltaTime;
                    //A very crude check so that we stay on the road
                    newPosition.x = Mathf.Clamp(newPosition.x, -_maxSwayWidth, _maxSwayWidth);
                }
                _target.position = newPosition;
            }
        }

        private bool IsMoving() => _appliedForwardSpeed > 0;

        private void StartMoving()
        {
            _appliedForwardSpeed = _regularSpeed;
        }

        public override void ApplySway(float sway)
        {
            _lastSway = sway;
        }
        public override void SetBoost(bool isBoosted)
        {
            _appliedForwardSpeed = (isBoosted ? _boostedSpeed : _regularSpeed); 
        }
    }
}