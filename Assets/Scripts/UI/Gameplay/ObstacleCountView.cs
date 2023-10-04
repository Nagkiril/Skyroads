using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Skyroad.Obstacles;
using Zenject;

namespace Skyroad.UI.Gameplay
{
    public class ObstacleCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textView;

        private IObstaclePassProvider _passProvider;

        [Inject]
        private void Inject(IObstaclePassProvider passProvider)
        {
            _passProvider = passProvider;
        }

        private void Start()
        {
            _passProvider.OnObstaclePassed += ViewData;
            ViewData(_passProvider.PassedObstaclesCounter);
        }

        private void OnDestroy()
        {
            _passProvider.OnObstaclePassed -= ViewData;
        }


        void ViewData(int passedObstacles)
        {
            _textView.text = passedObstacles.ToString();
        }
    }
}