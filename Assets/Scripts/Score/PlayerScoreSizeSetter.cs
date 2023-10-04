using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Skyroad.Player;

namespace Skyroad.Score
{
    public class PlayerScoreSizeSetter : MonoBehaviour, IScoreTickSizeProvider
    {
        [SerializeField] private int _scorePerTickRegular;
        [SerializeField] private int _scorePerTickBoosted;

        private IPlayer _player;

        [Inject]
        private void Inject(IPlayer player)
        {
            _player = player;
        }

        public int GetScoreTickSize()
        {
            return (_player.IsBoosted ? _scorePerTickBoosted : _scorePerTickRegular);
        }
    }
}