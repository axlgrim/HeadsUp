using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{


    public TextMeshProUGUI TimerText;
    public bool IsPaused = false;
    private float _time;
    // Use this for initialization
    void Start()
    {
        _time = Time.timeSinceLevelLoad;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPaused)
        {

        }
        float t = Time.timeSinceLevelLoad - _time;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        TimerText.text = minutes + ":" + seconds;

    }
}
