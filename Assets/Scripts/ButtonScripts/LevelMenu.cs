using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] GameObject[] uiContainers;

    bool isPaused = false;

    int homeSceneLoad;
    private void Start()
    {
        homeSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        uiContainers[0].SetActive(false);
        uiContainers[1].SetActive(false);
        uiContainers[2].SetActive(false);
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        EscMenu();

        }
    }

    void EscMenu()
    {
        if(isPaused)
        {
            uiContainers[0].SetActive(false);
            uiContainers[1].SetActive(false);
            uiContainers[2].SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            uiContainers[0].SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        isPaused = !isPaused;
    }
    public void ResumeGame()
    {
        uiContainers[0].SetActive(false);
        Cursor.lockState= CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
    public void ExitConfirm()
    {
        uiContainers[0].SetActive(false);
        uiContainers[1].SetActive(true);
    }
    public void GoBack()
    {
        uiContainers[0].SetActive(true);
        uiContainers[1].SetActive(false);
        uiContainers[2].SetActive(false);
    }
    public void Settings()
    {
        uiContainers[0].SetActive(false);
        uiContainers[2].SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void GoBackHome()
    {
        StartCoroutine(GoHome());
    }
    IEnumerator GoHome()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(homeSceneLoad);
        while(!asyncload.isDone) { yield return null; }
    }
}
