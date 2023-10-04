using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Skyroad.Player;
using DG.Tweening;

namespace Skyroad.Environment
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _targetCamera;
        [SerializeField] private float _regularFoV;
        [SerializeField] private float _boostedFoV;
        [SerializeField] private float _boostDuration;
        [SerializeField] private float _revertDuration;

        private IPlayer _player;

        private Tween _fovTween;

        [Inject]
        private void Inject(IPlayer player)
        {
            _player = player;
        }

        // Start is called before the first frame update
        void Awake()
        {
            _player.OnPlayerBoostChanged += SetBoostFov;
        }

        private void OnDestroy()
        {
            _player.OnPlayerBoostChanged -= SetBoostFov;
        }


        //I know the task was asking us to move the camera around the character smoothly, but I think changing FoV is significantly better to make players feel faster
        //I hope my improvisation wouldn't be too inconvenient
        void SetBoostFov()
        {
            if (_fovTween != null && _fovTween.active)
                _fovTween.Kill();
            _fovTween = _targetCamera.DOFieldOfView((_player.IsBoosted ? _boostedFoV : _regularFoV), (_player.IsBoosted ? _boostDuration : _revertDuration ));
            //If we REALLY wanted to move camera instead\together with FoV, we could tween a float and on each update call some kind of method on SmoothFollow script (like mentioned there AdjustFollowOffset)
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}