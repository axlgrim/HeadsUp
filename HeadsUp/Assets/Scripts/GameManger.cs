using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;

public class GameManger : MonoBehaviour {

    private Gyroscope _gyro;
    public bool IsPaused;
    public GameObject PausePanel;
    public GameObject EndPanel;


    private Animator animator;
    public int _scroreGuessed = 0;
    public int _scroreSkipped = 0;
    public bool _guessed = false;
    public bool _skipped = false;
    public bool gyro_enabled = false;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI SkippededText;
    public TextMeshProUGUI WordToGuess;
    public GameObject CorrectImage;
    public GameObject WrongImage;
    public GameObject Canvas;


    private string _jsonfile;
    private string path;
    private float _countdown;
    private int _wordCounter = 0;
    private Timer timer;
    private bool _endGame = false;

    private WordData Words;
    public GameObject img;


	// Use this for initialization
	void Start () 
    {
        
        timer = FindObjectOfType<Timer>();
        Subcribe(timer);
        path = Application.streamingAssetsPath + "/Config.json";
        _jsonfile = File.ReadAllText(path);

        WrongImage.SetActive(false);
        CorrectImage.SetActive(false);
        Words = JsonUtility.FromJson<WordData>(_jsonfile);
        WordToGuess.text = Words.words[_wordCounter];
        PausePanel.SetActive(false);
        EndPanel.SetActive(false);
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
            if(!_endGame)
            {
                CheckGuess();
                CheckSkipped();
                CheckDefault();
            }

        }



		
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
            
            _scroreGuessed += 1;
            _guessed = true;

            ScoreText.text = _scroreGuessed.ToString();

            timer.N = Time.timeSinceLevelLoad;
            if(img == null)
            {
                
                img = Instantiate(CorrectImage, CorrectImage.transform) as GameObject;
                img.transform.SetParent(Canvas.transform);

                img.SetActive(true);
            }
            animator.SetTrigger("Correct");
            Destroy(img, 1f);
            ChangeWord();

        }

    }

    private void CheckSkipped()
    {
        if (Math.Abs(_gyro.attitude.y) < 0.5f && !_skipped)
        {
            _scroreSkipped += 1;
            SkippededText.text = _scroreSkipped.ToString();
            _skipped = true;

            if (img == null)
            {

                img = Instantiate(WrongImage, WrongImage.transform) as GameObject;
                img.transform.SetParent(Canvas.transform);

                img.SetActive(true);
            }
            animator.SetTrigger("Wrong");
            Destroy(img, 1f);


            timer.N = Time.timeSinceLevelLoad;
            ChangeWord();
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

    private void Subcribe(Timer t)
    {

        t.OnTimeElapsed += HandleOnTimeElapsed;

    }
    private void Unsubcribe(Timer t)
    {

        t.OnTimeElapsed -= HandleOnTimeElapsed;


    }

    private void HandleOnTimeElapsed(Timer t)
    {
        _scroreSkipped += 1;
        animator.SetTrigger("Wrong");
        SkippededText.text = _scroreSkipped.ToString();
        ChangeWord();
    }

    private void ChangeWord()
    {
        _wordCounter += 1;
        if (_wordCounter < Words.words.Length)
        {
            WordToGuess.text = Words.words[_wordCounter];
        }
        else
        {
            EndGame();
        }

        
    }

    private void EndGame()
    {
        Destroy(timer);
        _endGame = true;
        EndPanel.SetActive(true);


    }
}


[System.Serializable]
public class WordData
{
    public string[] words;
}

public class Correct
{
    public GameObject img;
}
