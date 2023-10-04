using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Score
{
    public interface IHighscoreProvider
    {
        public int GetHighScore();
    }
}