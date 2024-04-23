using TMPro;
using UnityEngine;

namespace machines
{
    public class RevealStationName : MonoBehaviour
    {
        public Canvas stationNameCanvas;
        public TextMeshProUGUI stationNameText;
        public string stationName = "";

        void Start()
        {
            //string highlightedText = "<mark=#FFFFFF>" + stationName + "</mark>";
            stationNameText.SetText(stationName);

            ShowMachineName(false);
        }

        // onMouseSomething requires a collider to detect the mouse
        public void OnMouseOver()
        {
            //Debug.Log("over " + stationName);
            ShowMachineName(true);
        }

        public void OnMouseExit()
        {
            //Debug.Log("exit " + stationName);
            ShowMachineName(false);
        }

        public void ShowMachineName(bool show)
        {
            stationNameCanvas.enabled = show;
        }

    }
}
