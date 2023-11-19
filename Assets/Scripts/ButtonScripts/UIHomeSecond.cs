using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHomeSecond : MonoBehaviour
{
    [SerializeField] Sprite[] characterSprites;
    [SerializeField] Image characterImage;
    [SerializeField] GameObject[] characterContainer;
    [SerializeField] Sprite[] backgroundImage;
    [SerializeField] Image backgroundContainer;
    [SerializeField] GameObject[] buttonContainers;
    [SerializeField] GameObject[] loreContainers;

    private int nextSceneLoad;
    private void Start()
    {
        characterContainer[0].SetActive(true);
        characterContainer[1].SetActive(false);
        characterContainer[2].SetActive(false);
        buttonContainers[1].SetActive(false);
        buttonContainers[2].SetActive(false) ;
        buttonContainers[3].SetActive(false);
        loreContainers[0].SetActive(false);
        loreContainers[1].SetActive(false);
        loreContainers[2].SetActive(false);
        BackgroundImage();
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }
    void BackgroundImage()
    {
        int rnd = Random.Range(0, backgroundImage.Length);
        backgroundContainer.sprite = backgroundImage[rnd];
    }
    public void StartGame()
    {
        StartCoroutine(UnLoad());
    }
    IEnumerator UnLoad()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(nextSceneLoad);
        while (!asyncload.isDone) { yield return null; }
    }
    public void Settings()
    {
        buttonContainers[0].SetActive(false);
        buttonContainers[4].SetActive(false);
        buttonContainers[2].SetActive(true);
    }
    public void CharacterSelection()
    {
        buttonContainers[0].SetActive(false) ;
        buttonContainers[1].SetActive(true) ;   
    }
    public void YumiLore()
    {
        loreContainers[0].SetActive(true);
    }
    public void MaikoLore()
    {
        loreContainers[1].SetActive(true);
    }
    public void UtaLore()
    {
        loreContainers[2].SetActive(true);
    }
    public void GoBackToMain()
    {
        buttonContainers[0].SetActive(true);
        buttonContainers[1].SetActive(false);
        buttonContainers[2].SetActive(false);
        buttonContainers[3].SetActive(false);
        buttonContainers[4].SetActive(true);

        loreContainers[0].SetActive(false);
        loreContainers[1].SetActive(false);
        loreContainers[2].SetActive(false);
    }
    public void ExitConfirmation()
    {
        buttonContainers[0].SetActive(false);
        buttonContainers[3].SetActive(true);
    }
    public void SystemExit()
    {
      Application.Quit();
    }

    public void KohanaYumi()
    {
        characterContainer[0].SetActive(true);
        characterContainer[1].SetActive(false);
        characterContainer[2].SetActive(false);
    }
    public void NanamiMaiko()
    {
        characterContainer[0].SetActive(false);
        characterContainer[1].SetActive(true);
        characterContainer[2].SetActive(false);
    }
    public void IdeUta()
    {
        characterContainer[0].SetActive(false);
        characterContainer[1].SetActive(false);
        characterContainer[2].SetActive(true);
    }
}
