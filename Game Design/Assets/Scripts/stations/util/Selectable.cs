using System;
using stations;
using UnityEngine;
namespace UI
{
    public class Selectable : MonoBehaviour
    {
        public GameObject selectionIndicator;

        private RevealStationName _revealStationName;

        private void Awake()
        {
            _revealStationName = GetComponent<RevealStationName>();
        }

        public void Select()
        {
            selectionIndicator.SetActive(true);
            if (_revealStationName)
            {
                _revealStationName.ShowMachineName(true);
            }
        }
        public void Deselect()
        {
            selectionIndicator.SetActive(false);
            if (_revealStationName)
            {
                _revealStationName.ShowMachineName(false);
            }
        }
    }
}
