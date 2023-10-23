using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    [Header("Progress Bar")]
    [SerializeField] Image progressBar;
    [Header("Image Change")]
    [SerializeField] Image backgroundImage;
    [SerializeField] Sprite[] randomImage;
    private void Start()
    {
        ImageChange();
        StartCoroutine(UnLoad());
    }
    IEnumerator UnLoad()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("UI_Home");
        while (gameLevel.progress < 1) 
        {
               progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }

    void ImageChange()
    {
        int rnd = Random.Range(0, randomImage.Length);
        backgroundImage.sprite = randomImage[rnd];
        //Random rnd = new Random();
        //int num = rnd.next(Sprite.count);

    }
}

