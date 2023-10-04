using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Skyroad.Player.Visuals
{
    //Player visuals that tilt a transform as a reaction to ship's sway
    public class PlayerTiltVisuals : PlayerVisuals
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float maxTiltAngle;
        [SerializeField] private float swayDuration;

        private Tween _tiltTween;
        private Vector3 _defaultEulers;

        private void Awake()
        {
            _defaultEulers = _target.localRotation.eulerAngles;
        }

        public override void ApplySway(float sway)
        {
            if (_tiltTween != null && _tiltTween.active)
                _tiltTween.Kill();
            var newEulers = _defaultEulers;
            newEulers.z += maxTiltAngle * sway;
            _tiltTween = _target.DOLocalRotateQuaternion(Quaternion.Euler(newEulers), swayDuration);
        }
    }
}