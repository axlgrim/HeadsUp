using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManger : MonoBehaviour {

    private Gyroscope _gyro;
    public bool IsPaused;
    public GameObject PausePanel;

    private Animator animator;
    public int _scroreGuessed = 0;
    public bool _guessed = false;
    public bool _skipped = false;
    public bool gyro_enabled = false;
    public TextMeshProUGUI ScoreText;
    public Image CorrectImage;
    public Image WrongImage;

	// Use this for initialization
	void Start () 
    {
        
        //CorrectImage.enabled = false;
        PausePanel.SetActive(false);
        EnableGyro();
        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!IsPaused)
        {
            Resume();

        }
        if (IsPaused)
        {

            Pause();

        }

        if(_gyro!= null)
        {
            CheckGuess();
            CheckSkipped();
            CheckDefault();
        }
        Debug.Log(_gyro.attitude.y);


		
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
        if(Math.Abs(_gyro.attitude.y) > 0.8f && !_guessed)
        {
            CorrectImage.enabled = true;
            _scroreGuessed += 1;
            _guessed = true;

            ScoreText.text = _scroreGuessed.ToString();
            if(CorrectImage.enabled)
            {
                animator.SetTrigger("Correct");
            }

        }

    }

    private void CheckSkipped()
    {
        if (Math.Abs(_gyro.attitude.y) < 0.5f && !_skipped)
        {
            _skipped = true;
            animator.SetTrigger("Wrong");

        }


    }
    private void CheckDefault()
    {
        Debug.Log(_gyro.attitude);
        if (Math.Abs(_gyro.attitude.y) <= 0.8f && Math.Abs(_gyro.attitude.y) >= 0.5f)
        {
            Debug.Log("Cleared");
            _guessed = false;
            _skipped = false;

        }
    }

    void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;

    }

    void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnPauseBtnClicked()
    {
        IsPaused = true;

    }

    public void OnResumeBtnClicked()
    {
        IsPaused = false;

    }

    public void OnRestartBtnClicked()
    {
        SceneManager.LoadScene("HeadsUpScene");
    }

    public void OnMenuBtnClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
