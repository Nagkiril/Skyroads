using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Environment.Level;
using Zenject;

namespace Skyroad.Player.Controls
{
    //PlayerControl which is based on old Input System
    public class InputPlayerControls : PlayerControls
    {
        private bool _controlsActive;
        private ILevelEventProvider _levelEvents;


        [Inject]
        private void Inject(ILevelEventProvider levelEvents)
        {
            _levelEvents = levelEvents;
        }

        private void Awake()
        {
            _levelEvents.OnLevelStarted += EnableControls;
            _levelEvents.OnLevelEnded += DisableControls;
        }

        private void OnDestroy()
        {
            _levelEvents.OnLevelStarted -= EnableControls;
            _levelEvents.OnLevelEnded -= DisableControls;
        }

        private void EnableControls()
        {
            _controlsActive = true;
        }

        private void DisableControls()
        {
            _controlsActive = false;
        }


        private void Update()
        {
            if (_controlsActive)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    SetSway(-1);
                }
                else if
                (Input.GetKey(KeyCode.D))
                {
                    SetSway(1);
                }
                else
                {
                    SetSway(0);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SetBoost(true);
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    SetBoost(false);
                }
            }
        }
    }
}