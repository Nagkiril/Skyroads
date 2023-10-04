using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Player
{
    public interface IPlayer
    {
        public event Action OnPlayerKilled;
        public event Action OnPlayerBoostChanged;

        public bool IsBoosted { get; }

        public void Kill();
    }
}