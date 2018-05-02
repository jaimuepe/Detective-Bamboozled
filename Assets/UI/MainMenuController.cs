using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject btNewGame;
    public GameObject btContinue;
    public GameObject btExit;

    public GameObject startNewGamePopup;
    public GameObject startNewGameCancelButton;

    private void Start()
    {
        startNewGamePopup.gameObject.SetActive(false);
        if (Database.instance.CheckEvent("evt_museum_owner_intro_done"))
        {
            EventSystem.current.SetSelectedGameObject(btContinue);
        }
        else
        {
            btContinue.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(btNewGame);
        }
    }

    public void StartNewGame()
    {
        if (Database.instance.CheckEvent("evt_museum_owner_intro_done"))
        {
            ShowMessageStartNewGame();
        }
        else
        {
            SceneManager.LoadScene("Scenes/" + "office");
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Scenes/" + "office");
    }

    public void Exit()
    {
        Application.Quit();
    }

    void ShowMessageStartNewGame()
    {
        startNewGamePopup.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(startNewGameCancelButton);
    }

    public void HideMessageStartNewGame()
    {
        startNewGamePopup.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(btContinue);
    }

    public void ForceNewGame()
    {
        PlayerPrefs.DeleteAll();
        startNewGamePopup.gameObject.SetActive(false);

        SceneManager.LoadScene("Scenes/" + "office");
    }
}
