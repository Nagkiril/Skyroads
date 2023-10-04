using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Environment.Level
{
    public interface ILevelEventProvider
    {
        public event Action OnLevelStarted;
        public event Action OnLevelEnded;
    }
}