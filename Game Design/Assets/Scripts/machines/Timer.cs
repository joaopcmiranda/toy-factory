using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float time;
    public TextMeshProUGUI timerText;
    public Image fill;
    public float max;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timerText.text = "" + (int)time;
        fill.fillAmount = time / max;

        if (time < 0)
        {
            time = 0;
        }
    }
}
