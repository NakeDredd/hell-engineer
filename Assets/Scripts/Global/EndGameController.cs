using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : Singleton<EndGameController>
{
    [SerializeField] private GameObject losePanel;

    public void LoseTheGame ()
    {
        losePanel.SetActive(true);
    }

    public void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
