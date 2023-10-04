using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Score
{
    public interface IScoreProvider
    {
        public event Action<int> OnScoreChanged;

        public int GetCurrentScore();
    }
}