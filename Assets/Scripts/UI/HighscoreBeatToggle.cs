using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Skyroad.Score;

namespace Skyroad.UI
{
    public class HighscoreBeatToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _activeOnBeat;

        private IScoreProvider _score;
        private IHighscoreProvider _highscore;

        [Inject]
        private void Inject(IScoreProvider scoreProvider, IHighscoreProvider highscoreProvider)
        {
            _score = scoreProvider;
            _highscore = highscoreProvider;
        }


        // Start is called before the first frame update
        void Start()
        {
            var isHighscoreBeat = _score.GetCurrentScore() >= _highscore.GetHighScore();
            _target.SetActive((_activeOnBeat && isHighscoreBeat) || (!_activeOnBeat && !isHighscoreBeat));
        }
    }
}