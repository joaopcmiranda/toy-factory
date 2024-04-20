using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace machines
{
    public class RevealStationName : MonoBehaviour
    {
        public TextMeshProUGUI stationNameText;
        public string stationName = "";

        void Start()
        {
            stationNameText.SetText(stationName);
            ShowMachineName(false);
        }

        public void OnMouseOver()
        {
            ShowMachineName(true);
        }

        public void OnMouseExit()
        {
            ShowMachineName(false);
        }

        public void ShowMachineName(bool show)
        {
            stationNameText.enabled = show;
        }

    }
}
