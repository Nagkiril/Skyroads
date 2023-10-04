using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using Skyroad.Environment.Level;

namespace Skyroad.UI.Gameplay
{
    public class PlaytimeCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textView;

        private IPlaytimeProvider _playtimeProvider;

        [Inject]
        private void Inject(IPlaytimeProvider playtimeProvider)
        {
            _playtimeProvider = playtimeProvider;
        }

        private void Start()
        {
            _playtimeProvider.OnPlaytimeChanged += ViewData;
            ViewData(_playtimeProvider.GetPlaytime());
        }

        private void OnDestroy()
        {
            _playtimeProvider.OnPlaytimeChanged -= ViewData;
        }


        void ViewData(float playtime)
        {
            _textView.text = Mathf.FloorToInt(playtime).ToString();
        }
    }
}