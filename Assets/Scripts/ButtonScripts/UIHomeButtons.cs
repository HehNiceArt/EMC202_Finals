using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIHomeButtons : MonoBehaviour
{
    private int nextSceneLoad;
    UILoading load;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        #region Buttons
        #region Exploration
        Button explore = root.Q<Button>("Explore");
        Button formation = root.Q<Button>("Formation");
        #endregion
        #region BottmButtons
        Button recruits = root.Q<Button>("Recruits");
        Button armory = root.Q<Button>("Armory");
        Button dorm = root.Q<Button>("Dorm");
        Button gacha = root.Q<Button>("Gacha");
        Button mail = root.Q<Button>("Mail");
        #endregion
        #region Settings
        Button settings = root.Q<Button>("Settings");
        Button notice = root.Q<Button>("Notice");
        Button friends = root.Q<Button>("Friends");
        Button ranks = root.Q<Button>("Ranks");
        #endregion
        #endregion

        #region Button Triggers
        explore.clicked += () => StartCoroutine(UnLoad()); 
        #endregion
    }
    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
       // Debug.Log("Home" + nextSceneLoad);
    }
    IEnumerator UnLoad()
    {
      //  load.UnLoad(nextSceneLoad);
       // Debug.Log(load);
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(nextSceneLoad);
        while (!asyncload.isDone) { yield return null; }

    }

}
