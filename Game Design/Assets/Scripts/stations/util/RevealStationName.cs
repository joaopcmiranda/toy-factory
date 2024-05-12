using TMPro;
using UnityEngine;

namespace stations
{
    public class RevealStationName : MonoBehaviour
    {
        public Canvas stationNameCanvas;
        public TextMeshProUGUI stationNameText;
        public string stationName = "";

        private void Start()
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
            stationNameCanvas.enabled = show;
        }

    }
}
