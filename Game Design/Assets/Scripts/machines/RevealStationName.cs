using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace machines
{
    public class RevealStationName : MonoBehaviour
    {
        public Canvas stationNameCanvas;
        public TextMeshProUGUI stationNameText;
        public string stationName = "";

        void Start()
        {
            string highlightedText = "<mark=#FFFFFF>" + stationName + "</mark>";
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
            stationNameCanvas.enabled = show;
        }

    }
}
