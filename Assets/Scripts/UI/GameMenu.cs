using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        InputRegister.Instance.InputPause += AutoPause;
        ContinueGame();
    }
    private void OnDisable()
    {
        InputRegister.Instance.InputPause -= AutoPause;
    }

    private void AutoPause ()
    {
        if (Time.timeScale == 0)
        {
            ContinueGame();
        }
        else
        {
            StopGame();
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ContinueGame ()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void ExitInMenu ()
    {
        SceneManager.LoadScene(0);
    }
}
