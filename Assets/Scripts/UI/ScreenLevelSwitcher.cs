using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Skyroad.Environment.Level;

namespace Skyroad.UI
{
    public class ScreenLevelSwitcher : MonoBehaviour
    {
        //Quite inelegant, but it does its job
        [SerializeField] bool defaultActivity;
        [SerializeField] bool levelEndActivity;
        [SerializeField] bool levelStartActivity;
        

        private ILevelEventProvider _levelEvents;

        [Inject]
        private void Inject(ILevelEventProvider levelEvents)
        {
            _levelEvents = levelEvents;
        }

        // Start is called before the first frame update
        void Awake()
        {
            //Ideally, I may want to make a method "OnLevelStart" and bind it here, rather than direct command
            _levelEvents.OnLevelStarted += SetStartActivity;
            _levelEvents.OnLevelEnded += SetEndActivity;
            SetInitialActivity();
        }

        private void OnDestroy()
        {
            _levelEvents.OnLevelStarted -= SetStartActivity;
            _levelEvents.OnLevelEnded -= SetEndActivity;
        }

        void SetInitialActivity()
        {
            gameObject.SetActive(defaultActivity);
        }

        void SetStartActivity()
        {
            gameObject.SetActive(levelStartActivity);
        }

        void SetEndActivity()
        {
            gameObject.SetActive(levelEndActivity);
        }
    }
}