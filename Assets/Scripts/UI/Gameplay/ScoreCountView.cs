using UnityEngine;
using Skyroad.Score;
using Zenject;
using TMPro;

namespace Skyroad.UI.Gameplay
{
    public class ScoreCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreView;
        [SerializeField] private TextMeshProUGUI _highscoreView;

        private IHighscoreProvider _highscore;
        private IScoreProvider _score;

        [Inject]
        private void Inject(IHighscoreProvider highscore, IScoreProvider score)
        {
            _score = score;
            _highscore = highscore;
        }

        void Start()
        {
            _score.OnScoreChanged += ViewData;
            ViewData(_score.GetCurrentScore());
        }

        private void OnDestroy()
        {
            _score.OnScoreChanged -= ViewData;
        }

        void ViewData(int score)
        {
            _scoreView.text = score.ToString();
            _highscoreView.text = _highscore.GetHighScore().ToString();
        }
    }
}