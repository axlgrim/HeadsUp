using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator animator;
    private AnimatorClipInfo[] _currentClipInfo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("HeadsUpScene");
    }

    public void OnQuitClicked()
    {

        Application.Quit();
    }
}
