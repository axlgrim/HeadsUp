using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManger : MonoBehaviour {

    private Gyroscope _gyro;
    private int _scroeGuessed;
    public bool gyro_enabled = false;
    public TextMeshProUGUI ScoreText;
    private Quaternion _rotGuess = new Quaternion(0.0f, -0.9f, -0.5f, 0.0f);
    private Quaternion _rotMissed = new Quaternion(0.0f, -0.5f, -0.9f,-0.1f);

	// Use this for initialization
	void Start () 
    {
        EnableGyro();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.Log(_gyro.attitude);
        CheckGuess();
        ScoreText.text = _scroeGuessed.ToString();
		
	}

    private void EnableGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            _gyro = Input.gyro;
            _gyro.enabled = true;
            gyro_enabled = true;
        }
    }

    private void CheckGuess()
    {
        if(_gyro.attitude == _rotGuess)
        {
            _scroeGuessed += 1;
        }

    }
}
