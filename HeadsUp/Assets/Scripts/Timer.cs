using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{


    public TextMeshProUGUI TimerText;
    public bool IsPaused = false;
    public float _time;
    public float N = 0.0f;
    public event Action<Timer> OnTimeElapsed;

    private float _countdown;
    public float reset_t = 0.0f;

    void Awake()
    {
        var textAsset = Resources.Load<TextAsset>("Config");
        _countdown = Convert.ToInt32(textAsset.text);
    }
    // Use this for initialization
    void Start()
    {
        _time = Time.timeSinceLevelLoad;

    }

    // Update is called once per frame
    void Update()
    {

        float t = Time.timeSinceLevelLoad - _time - reset_t - N;
        if ((t % 60) > _countdown)
        {
            if (OnTimeElapsed != null)
            {
                OnTimeElapsed(this);
            }
            reset_t += 30;
        }


        string seconds = (t % 60).ToString("f2");

        TimerText.text = seconds;

    }
}
